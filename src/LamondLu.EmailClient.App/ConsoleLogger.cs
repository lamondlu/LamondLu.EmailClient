using LamondLu.EmailClient.Domain.Interface;
using System;

namespace LamondLu.EmailClient.App
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine($"[Log]{message}");
        }
    }
}