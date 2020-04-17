using FluentMigrator;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations
{
    [Migration(20200417081007)]
    public class UpdateEmailConnectorTable : Migration
    {
        public override void Up()
        {
            Delete.Column("CreatedTime").FromTable("EmailConnector");
            Delete.Column("CreatedBy").FromTable("EmailConnector");
            Delete.Column("UpdatedTime").FromTable("EmailConnector");
            Delete.Column("UpdatedBy").FromTable("EmailConnector");

            Alter.Table("EmailConnector").AddColumn("Type").AsInt16().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("Type").FromTable("EmailConnector");

            Alter.Table("EmailConnector").AddColumn("CreatedTime").AsDateTime().NotNullable();
            Alter.Table("EmailConnector").AddColumn("CreatedBy").AsString().NotNullable();
            Alter.Table("EmailConnector").AddColumn("UpdatedTime").AsDateTime().Nullable();
            Alter.Table("EmailConnector").AddColumn("UpdatedBy").AsString().Nullable();
        }
    }
}
