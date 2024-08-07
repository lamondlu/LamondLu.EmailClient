﻿using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using MailKit.Net.Pop3;
using System;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class POP3EmailConnectorWorker : IEmailConnectorWorker
    {
        private Pop3Client _emailClient = null;

        private EmailConnector _emailConnector = null;

        private IUnitOfWork _unitOfWork = null;

        private IEncrypt _encryptor = null;

        public POP3EmailConnectorWorker(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork, IEncrypt encryptor)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            _emailConnector = emailConnector;
            _unitOfWork = unitOfWork;
            _encryptor = encryptor;
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public async Task<bool> Connect()
        {
            try
            {
                _emailClient = new Pop3Client();

                await _emailClient.ConnectAsync(_emailConnector.Server.POP3Server, _emailConnector.Server.POP3Port.Value, true);
                _emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await _emailClient.AuthenticateAsync(_emailConnector.UserName, _encryptor.Decrypt(_emailConnector.Password));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Disconnect()
        {
            try
            {
                await _emailClient.DisconnectAsync(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task Listen()
        {
            Console.WriteLine("Start to pull email.");

            try
            {
                if (!_emailClient.IsConnected)
                {
                    for (var index = 0; index < _emailClient.Count; index++)
                    {
                        var message = await _emailClient.GetMessageAsync(index);

                        if (EmailReceived != null)
                        {


                        }
                    }
                }

            }
            catch (InvalidOperationException)
            {
                if (!_emailClient.IsConnected)
                {
                    Console.WriteLine("The email client is disconnected.");
                }
            }
        }
    }
}
