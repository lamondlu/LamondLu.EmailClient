using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class POP3EmailConnector : IEmailConnector
    {
        public List<Rule> Rules
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event EmailReceived EmailReceived;

        public void Connect()
        {

        }
    }
}
