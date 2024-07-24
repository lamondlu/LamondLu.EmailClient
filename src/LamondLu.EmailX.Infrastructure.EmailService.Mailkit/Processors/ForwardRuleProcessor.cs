using System;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class ForwardRuleProcessor : IRuleProcessor
    {
        private readonly IUnitOfWork _unitOfWork = null;

        public ForwardRuleProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Run(Email email, Rule rule)
        {
            try
            {
                Console.WriteLine($"System send out reply email 'FW: {email.Subject}'");

                var emailConnector = _unitOfWork.EmailConnectorRepository.GetEmailConnectorConfig(email.EmailFolder.EmailConnectorId).Result;

                if (emailConnector != null)
                {
                    var forwardRule = rule as ForwardRule;

                    using (var client = new SmtpClient())
                    {
                        client.Connect(emailConnector.SMTPServer, emailConnector.SMTPPort.Value, emailConnector.EnableSSL ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(emailConnector.UserName, emailConnector.Password);

                        var mimeMessage = new MimeMessage
                        {
                            Subject = $"FW: {email.Subject}"
                        };

                        mimeMessage.From.Add(new MailboxAddress("", emailConnector.EmailAddress));


                        if (forwardRule.ForwardAddresses != null && forwardRule.ForwardAddresses.Any())
                        {
                            foreach (var forwardAddress in forwardRule.ForwardAddresses)
                            {
                                mimeMessage.To.Add(new MailboxAddress(forwardAddress.DisplayName ?? string.Empty, forwardAddress.Address));
                            }
                        }

                        var builder = new BodyBuilder
                        {
                            HtmlBody = email.Body
                        };

                        mimeMessage.Body = builder.ToMessageBody();
                        await client.SendAsync(mimeMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reply Rule Error: {ex.Message}");
            }

        }
    }
}