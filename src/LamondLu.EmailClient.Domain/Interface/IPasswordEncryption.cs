﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IPasswordEncryption
    {
        string Encrypt(string password);
    }
}