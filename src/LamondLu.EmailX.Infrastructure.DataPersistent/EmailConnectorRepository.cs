using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Business;
using LamondLu.EmailX.Domain.Enum;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class EmailConnectorRepository : IEmailConnectorRepository
    {
        private DapperDbContext _context = null;

        public EmailConnectorRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmailConnectorConfigViewModel>> GetEmailConnectorConfigs()
        {
            var sql = "SELECT * FROM EmailConnector WHERE IsDeleted = 0";
            var result = await _context.QueryAsync<EmailConnectorConfigViewModel>(sql);

            return result.ToList();
        }

        public async Task<EmailConnectorConfigViewModel> GetEmailConnectorConfig(Guid emailConnectorId)
        {
            var sql = "SELECT * FROM EmailConnector WHERE IsDeleted = 0 AND EmailConnectorId=@emailConnectorId";
            var result = await _context.QueryFirstOrDefaultAsync<EmailConnectorConfigViewModel>(sql, new { emailConnectorId });

            return result;
        }

        public async Task AddEmailConnector(EmailConnector emailConnector)
        {
            var sql = @"INSERT INTO EmailConnector(EmailConnectorId, 
                    Name, 
                    EmailAddress, 
                    UserName, 
                    Password, 
                    Status, 
                    SMTPServer,
                    SMTPPort,
                    IMAPServer,
                    IMAPPort,
                    POP3Server,
                    POP3Port,
                    EnableSSL, 
                    Description, Type) 
                    VALUES(@EmailConnectorId, @Name, @EmailAddress, @UserName, @Password, @Status, @SMTPServer, @SMTPPort, @IMAPServer, @IMAPPort, @POP3Server, @POP3Port, @EnableSSL, @Description, @Type)";

            await _context.Execute(sql, new
            {
                emailConnector.EmailConnectorId,
                emailConnector.Name,
                emailConnector,
                emailConnector.Password,
                emailConnector.Status,
                emailConnector.Server.SMTPServer,
                emailConnector.Server.SMTPPort,
                emailConnector.Server.IMAPServer,
                emailConnector.Server.IMAPPort,
                emailConnector.Server.POP3Server,
                emailConnector.Server.POP3Port,
                emailConnector.Server.EnableSSL,
                emailConnector.Description,
                emailConnector.UserName,
                emailConnector.Type
            });
        }

        public async Task<bool> CheckDuplicated(string emailAddress, string name, Guid emailConnectorId)
        {
            var sql = "SELECT COUNT(*) FROM EmailConnector WHERE IsDeleted=0 AND Name=@name AND EmailConnectorId<>@emailConnectorId";
            var count = await _context.ExecuteScalar<int>(sql, new { name, emailConnectorId });
            return count > 0;
        }

        public async Task<EmailConnector> GetEmailConnector(Guid emailConnectorId)
        {
            var sql = "SELECT * FROM EmailConnector WHERE IsDeleted = 0 AND EmailConnectorId=@emailConnectorId";
            var connectorRaw = await _context.QueryFirstOrDefaultAsync<EmailConnectorConfigViewModel>(sql, new { emailConnectorId });

            EmailConnector connector = null;
            if (connectorRaw == null)
            {
                return null;
            }

            connector = new EmailConnector(connectorRaw.EmailConnectorId, connectorRaw.Name, connectorRaw.EmailAddress, connectorRaw.UserName, connectorRaw.Password, new EmailServerConfig
                (
                    connectorRaw.SMTPServer,
                    connectorRaw.SMTPPort,
                    connectorRaw.IMAPServer,
                    connectorRaw.IMAPPort,
                    connectorRaw.POP3Server,
                    connectorRaw.POP3Port,
                    connectorRaw.EnableSSL
                ), connectorRaw.Type, connectorRaw.Description);

            List<Rule> mappingRules = new List<Rule>();

            string ruleSql = @"SELECT er.EmailRuleId, er.RuleName, er.RuleType FROM emailconnectorrule as ecr
                            INNER JOIN emailrule er ON ecr.EmailRuleId = er.EmailRuleId
                            WHERE ecr.EmailConnectorId = @EmailConnectorId AND er.IsDeleted = 0";

            IEnumerable<EmailRuleQueryViewModel> rules = await _context.QueryAsync<EmailRuleQueryViewModel>(ruleSql, new { connectorRaw.EmailConnectorId });

            foreach (EmailRuleQueryViewModel rule in rules)
            {
                if (rule.RuleType == RuleType.Reply)
                {
                    mappingRules.Add(await BuildReplyRule(rule.EmailRuleId, connectorRaw.EmailConnectorId));

                }
                // else if (rule.RuleType == RuleType.Classify)
                // {
                //     mappingRules.Add(await BuildClassifyRule(rule.EmailRuleId, id));
                // }
                // else if (rule.RuleType == RuleType.Move)
                // {
                //     mappingRules.Add(await BuildMoveRule(rule.EmailRuleId, id));
                // }
                // else if (rule.RuleType == RuleType.UpdateStatus)
                // {
                //     mappingRules.Add(await BuildUpdateStatusRule(rule.EmailRuleId, id));
                // }
                else
                {
                    continue;
                }
            }

            connector.Rules = mappingRules;

            return connector;
        }

         
        // private async Task<ClassifyRule> BuildClassifyRule(Guid ruleId, Guid emailConnectorId)
        // {
        //     string sql = @"SELECT  
        //                 er.TerminateIfMatch,
        //                 t.TagId,er.rulename,
        //                 t.TagName,e.`Order`,
        //                 er.IsAIOCRCriteria
        //                 FROM classifyemailrule cr 
        //                 INNER JOIN emailrule er on er.EmailRuleId = cr.EmailRuleId
        //                 INNER JOIN tag t ON t.TagId = cr.TagId
        //                 INNER JOIN emailconnectorrule e ON e.EmailRuleId =cr.EmailRuleId
        //                 WHERE cr.EmailRuleId = @ruleId and e.EmailConnectorId = @emailConnectorId";

        //     ClassifyRuleInternal ruleInternal = await _context.QueryFirstOrDefaultAsync<ClassifyRuleInternal>(sql, new { ruleId, emailConnectorId });

        //     ClassifyRule classify = new ClassifyRule
        //     {
        //         Tag = new EmailTag { TagId = ruleInternal.TagId, TagName = ruleInternal.TagName },
        //         StopProcessingMoreRule = ruleInternal.TerminateIfMatch,
        //         Expressions = new MatchExpressionCollection(await GetExpressions(ruleId)),
        //         Order = ruleInternal.Order,
        //         RuleName = ruleInternal.RuleName,
        //     };

        //     return classify;
        // }

        private async Task<List<MatchExpression>> GetExpressions(Guid emailRuleId)
        {
            string sql = @"SELECT ConditionType as Field, ConditionOperator as Operator, ConditionValue as Value, `Condition` FROM emailruledetails WHERE EmailRuleId=@emailRuleId ORDER BY `Order`";
            var expressions = await _context.QueryAsync<MatchExpression>(sql, new { emailRuleId });
            return expressions.ToList();
        }

        private async Task<ReplyRule> BuildReplyRule(Guid ruleId, Guid emailConnectorId)
        {
            string sql = @"SELECT et.EmailTemplateId, 
                        et.Name, 
                        et.Subject, 
                        et.Body, er.rulename,
                        er.TerminateIfMatch,
                        e.`Order`
                        FROM replyemailrule rr
                        INNER JOIN emailrule er on er.EmailRuleId = rr.EmailRuleId
                        INNER JOIN emailtemplate et ON et.EmailTemplateId = rr.EmailTemplateId
                        INNER JOIN emailconnectorrule e ON e.EmailRuleId =rr.EmailRuleId
                        WHERE rr.EmailRuleId = @ruleId and e.EmailConnectorId = @emailConnectorId";

            ReplyRuleInternal ruleInternal = await _context.QueryFirstOrDefaultAsync<ReplyRuleInternal>(sql, new { ruleId, emailConnectorId });

            ReplyRule replyRule = new ReplyRule
            {
                EmailTemplate = new EmailTemplate(ruleInternal.EmailTemplateId,
                ruleInternal.Name,
                ruleInternal.Subject,
                ruleInternal.Body),
                Order = ruleInternal.Order,
                StopProcessingMoreRule = ruleInternal.TerminateIfMatch,
                Expressions = new MatchExpressionCollection(await GetExpressions(ruleId))
            };

            return replyRule;
        }


        public async Task<List<EmailConnectorStatusViewModel>> GetEmailConnectorStatuses()
        {
            var sql = "SELECT Name, EmailAddress, Status FROM EmailConnector WHERE IsDeleted = 0";
            var result = await _context.QueryAsync<EmailConnectorStatusViewModel>(sql);

            return result.ToList();
        }

        public async Task UpdateEmailConnectorStatus(Guid emailConnectorId, EmailConnectorStatus status)
        {
            var sql = "UPDATE EmailConnector SET Status=@status WHERE EmailConnectorId=@emailConnectorId";
            await _context.Execute(sql, new { status, emailConnectorId });
        }

        public async Task<List<Guid>> GetAllRunningEmailConnectorIds()
        {
            var sql = "SELECT EmailConnectorId FROM EmailConnector WHERE IsDeleted = 0 AND Status = @status";
            var result = await _context.QueryAsync<Guid>(sql, new { status = EmailConnectorStatus.Running });

            return result.ToList();
        }
    }
}
