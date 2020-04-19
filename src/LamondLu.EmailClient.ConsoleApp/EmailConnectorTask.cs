using LamondLu.EmailClient.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorTask
    {
        private Guid _emailConnectorId;

        public EmailConnectorTask(Guid emailConnectorId)
        {
            _emailConnectorId = emailConnectorId;
        }

        public void Start()
        {

        }
    }
}
