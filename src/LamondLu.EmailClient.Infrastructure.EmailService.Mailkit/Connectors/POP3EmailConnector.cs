using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using MailKit.Net.Pop3;
using System;
using System.Collections.Generic;
using System.Text;


namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class POP3EmailConnector : IEmailConnector
    {
        public POP3EmailConnector(List<Rule> rules)
        {
            Pipeline = new RulePipeline(rules);
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public void Connect()
        {
            using (var client = new Pop3Client())
            {

            }
        }
    }
}
