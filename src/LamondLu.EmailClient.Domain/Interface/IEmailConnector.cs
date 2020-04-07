using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Interface
{
    public delegate void EmailReceived();

    public interface IEmailConnector
    {
        List<Rule> Rules { get; }

        event EmailReceived EmailReceived;

        void Connect();
    }
}
