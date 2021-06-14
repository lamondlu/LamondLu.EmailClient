using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorTask
    {
        private EmailConnectorConfigViewModel _emailConnector = null;

        public EmailConnectorTask(EmailConnectorConfigViewModel emailConnector)
        {
            _emailConnector = emailConnector;
        }

        public void Start()
        {
            Console.WriteLine($"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Start");

            try
            {
                //var emailConnector = new EmailConnector(_emailConnector.Name, _emailConnector)
            }
            catch
            {

            }
        }
    }
}
