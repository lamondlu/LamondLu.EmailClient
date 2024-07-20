using System;

namespace LamondLu.EmailX.Infrastructure.DataPersistent.Models
{
    public class DbSetting
    {
        public string ConnectionString { get; set; }

        public int TimeOut { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ConnectionString);
            }
        }
    }
}
