using System;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Linq;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class ReplyRuleProcessor : IRuleProcessor
    {
        private readonly IUnitOfWork _unitOfWork = null;

        public ReplyRuleProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Run(Email email, Rule rule)
        {
            try
            {
                Console.WriteLine($"System send out reply email 'RE: {email.Subject}'");

                var emailConnector = _unitOfWork.EmailConnectorRepository.GetEmailConnectorConfig(email.EmailFolder.EmailConnectorId).Result;

                if (emailConnector != null)
                {
                    var replyRule = rule as ReplyRule;

                    using (var client = new SmtpClient())
                    {
                        client.Connect(emailConnector.SMTPServer, emailConnector.SMTPPort.Value, emailConnector.EnableSSL ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(emailConnector.UserName, emailConnector.Password);

                        var mimeMessage = new MimeMessage
                        {
                            Subject = replyRule.EmailTemplate.Subject
                        };

                        mimeMessage.From.Add(new MailboxAddress("", emailConnector.EmailAddress));

                        if (email.ReplyTos != null && email.ReplyTos.Any())
                        {
                            foreach (var replyTo in email.ReplyTos)
                            {
                                mimeMessage.To.Add(new MailboxAddress(replyTo.DisplayName ?? string.Empty, replyTo.Address));
                            }
                        }

                        if (email.CCs != null && email.CCs.Any())
                        {
                            foreach (var cc in email.CCs)
                            {
                                mimeMessage.Cc.Add(new MailboxAddress(cc.DisplayName ?? string.Empty, cc.Address));
                            }
                        }

                        var builder = new BodyBuilder
                        {
                            HtmlBody = replyRule.EmailTemplate.Body
                        };

                        mimeMessage.Body = builder.ToMessageBody();
                        await client.SendAsync(mimeMessage);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Reply Rule Error: {ex.Message}");
            }
        }
    }
}