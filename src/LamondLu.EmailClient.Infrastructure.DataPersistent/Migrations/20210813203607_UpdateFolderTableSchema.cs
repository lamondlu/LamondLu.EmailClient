using FluentMigrator;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations
{
    [Migration(20210813203607)]
    public class UpdateFolderTableSchema : Migration
    {
        public override void Up()
        {
            Alter.Table("EmailFolder")
                .AddColumn("EmailConnectorId").AsGuid()
                .AddColumn("LastEmailId").AsInt32()
                .AddColumn("LastValidityId").AsInt32();

            Create.ForeignKey("FK_EmailFolder_EmailConnectorId_EmailConnector_Id")
                .FromTable("EmailFolder")
                .ForeignColumn("EmailConnectorId")
                .ToTable("EmailConnector")
                .PrimaryColumn("EmailConnectorId");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_EmailFolder_EmailConnectorId_EmailConnector_Id");

            Delete.Column("EmailConnectorId").FromTable("EmailFolder");
            Delete.Column("EmailConnectorId").FromTable("LastEmailId");
            Delete.Column("EmailConnectorId").FromTable("LastValidityId");
        }
    }
}
