using System;

namespace LamondLu.EmailClient.App
{
    public static class EnvironmentConst
    {
        public static IServiceProvider Services = null;

        public static Settings EmailSettings = null;

        public static T GetService<T>()
        {
            return (T)Services.GetService(typeof(T));
        }
    }
}