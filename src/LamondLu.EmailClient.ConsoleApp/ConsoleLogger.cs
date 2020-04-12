using System;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine($"[Log]{message}");
        }
    }
}