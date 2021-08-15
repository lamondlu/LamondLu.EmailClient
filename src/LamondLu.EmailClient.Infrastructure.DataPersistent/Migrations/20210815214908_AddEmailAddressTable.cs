using FluentMigrator;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations
{
    [Migration(20210815214908)]
    public class AddEmailAddressTable : Migration
    {
        public override void Up()
        {
            Create.Table("EmailAddress")
                .WithColumn("EmailAddressId").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString()
                .WithColumn("Address").AsString();

            Create.Table("EmailContent")
                .WithColumn("EmailId").AsGuid().PrimaryKey()
                .WithColumn("TextBody").AsCustom("text")
                .WithColumn("Body").AsCustom("text");

            Create.ForeignKey("EmailContent_EmailId_Email_Id").FromTable("EmailContent").ForeignColumn("EmailId").ToTable("Email").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("EmailAddress");
            Delete.ForeignKey("EmailContent_EmailId_Email_Id");
            Delete.Table("EmailContent");
        }
    }
}
