using LamondLu.EmailX.Domain.Interface;
using System;

namespace LamondLu.EmailX.App
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine($"[Log]{message}");
        }
    }
}