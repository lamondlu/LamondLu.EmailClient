using LamondLu.EmailClient.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorTask
    {
        private Guid _emailConnectorId;
        private string _emailConnectorName = string.Empty;

        public EmailConnectorTask(Guid emailConnectorId, string emailConnectorName)
        {
            _emailConnectorId = emailConnectorId;
            _emailConnectorName = emailConnectorName;
        }

        public void Start()
        {
            Console.WriteLine($"Email Connector (id:{_emailConnectorId},name: {_emailConnectorName}) Start");

            try
            {

            }
            catch
            {

            }
        }
    }
}
