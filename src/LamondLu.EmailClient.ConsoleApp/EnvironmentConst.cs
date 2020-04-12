using System;
using Microsoft.Extensions.DependencyInjection;

namespace LamondLu.EmailClient.ConsoleApp
{
    public static class EnvironmentConst
    {
        public static IServiceProvider Services = null;

        public static Settings EmailSettings = null;
    }
}