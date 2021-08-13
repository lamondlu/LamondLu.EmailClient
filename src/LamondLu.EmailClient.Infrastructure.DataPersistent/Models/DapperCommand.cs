using System.Data;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Models
{
    public class DapperCommand
    {
        public DapperCommand(string sql, object parameters, CommandType commandType)
        {
            Sql = sql;
            Parameters = parameters;
            CommandType = commandType;
        }

        public DapperCommand(string sql, object parameters) : this(sql, parameters, CommandType.Text)
        {

        }

        public string Sql { get; private set; }

        public object Parameters { get; private set; }

        public CommandType CommandType { get; private set; }
    }
}
