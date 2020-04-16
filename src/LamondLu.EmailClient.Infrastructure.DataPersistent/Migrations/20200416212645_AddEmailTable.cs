using FluentMigrator;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations
{
    [Migration(20200416212645)]
    public class AddEmailTable : Migration
    {
        public override void Up()
        {
            Create.Table("Email")
                .WithColumn("EmailId").AsGuid().PrimaryKey()
                .WithColumn("MessageId").AsString().NotNullable()
                .WithColumn("MailkitId").AsInt32().Nullable()
                .WithColumn("MailkitValidityId").AsInt32().Nullable()
                .WithColumn("ReceivedDate").AsDateTime().Nullable()
                .WithColumn("SentDate").AsDateTime().Nullable()
                .WithColumn("ImportedDate").AsDateTime().NotNullable()
                .WithColumn("Subject").AsString().Nullable()
                .WithColumn("Body").AsCustom("longtext").Nullable()
                .WithColumn("TextBody").AsCustom("longtext").Nullable()
                .WithColumn("Notes").AsCustom("longtext").Nullable()
                .WithColumn("SenderAddress").AsString().NotNullable()
                .WithColumn("SenderDisplayName").AsString().Nullable()
                .WithColumn("ReceiptAddress").AsString().NotNullable()
                .WithColumn("ReceiptDisplayName").AsString().Nullable()
                .WithColumn("FolderId").AsGuid().ForeignKey("FK_Email_FolderId_EmailFolder_FolderId", "EmailFolder", "FolderId");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Email_FolderId_EmailFolder_FolderId").OnTable("Email");
            Delete.Table("Email");
        }
    }
}
