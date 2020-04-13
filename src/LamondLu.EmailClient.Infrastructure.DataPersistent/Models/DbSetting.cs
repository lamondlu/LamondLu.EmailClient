using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Models
{
    public class DbSetting
    {
        public string ConnectionString { get; set; }

        public int Timeout { get; set; }
    }
}
