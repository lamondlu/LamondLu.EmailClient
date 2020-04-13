using LamondLu.EmailClient.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Concreate
{
    public class PlanTextPasswordEncryption : IPasswordEncryption
    {
        public string Encrypt(string password)
        {
            return password;
        }
    }
}
