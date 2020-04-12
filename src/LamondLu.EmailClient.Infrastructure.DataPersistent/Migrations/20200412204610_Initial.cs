using FluentMigrator;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations
{
    [Migration(20200412204610)]
    public class Initial : Migration
    {
        public override void Up()
        {
            Create.Table("EmailConnector")
                .WithColumn("EmailConnectorId").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("EmailAddress").AsString().NotNullable()
                .WithColumn("UserName").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Status").AsInt16().NotNullable()
                .WithColumn("IP").AsString()
                .WithColumn("Description").AsString()
                .WithColumn("CreatedTime").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("UpdatedTime").AsDateTime()
                .WithColumn("UpdatedBy").AsString()
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Table("EmailConnector");
        }
    }
}
