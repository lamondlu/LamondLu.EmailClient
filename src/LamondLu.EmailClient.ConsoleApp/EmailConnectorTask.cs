﻿using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorTask
    {
        private EmailConnectorConfigViewModel _emailConnector = null;
        private IEmailConnectorWorkerFactory _factory = null;
        private IRuleProcessorFactory _ruleProcessorFactory = null;
        private IUnitOfWorkFactory _unitOfWorkFactory = null;

        public EmailConnectorTask(EmailConnectorConfigViewModel emailConnector, IEmailConnectorWorkerFactory factory, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _emailConnector = emailConnector;
            _factory = factory;
            _ruleProcessorFactory = ruleProcessorFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Start()
        {
            Console.WriteLine($"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Start");

            EmailConnector emailConnector = new EmailConnector(_emailConnector.EmailConnectorId, _emailConnector.Name, _emailConnector.EmailAddress, _emailConnector.UserName, _emailConnector.Password, new EmailServerConfig(_emailConnector.IP, _emailConnector.Port, _emailConnector.EnableSSL)
            , _emailConnector.Type, string.Empty);

            try
            {
                var worker = _factory.Build(emailConnector, _ruleProcessorFactory, _unitOfWorkFactory.Create());

                var isConnected = await worker.Connect();

                if (isConnected)
                {
                    Console.WriteLine("Email Connector connected.");
                    await worker.Listen();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
