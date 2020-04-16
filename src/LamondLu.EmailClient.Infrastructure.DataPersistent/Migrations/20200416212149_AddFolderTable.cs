using FluentMigrator;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent.Migrations
{
    [Migration(20200416212149)]
    public class AddFolderTable : Migration
    {
        public override void Up()
        {
            Create.Table("EmailFolder")
               .WithColumn("FolderId").AsGuid().PrimaryKey()
               .WithColumn("FolderName").AsString().NotNullable()
               .WithColumn("FolderFullPath").AsString().NotNullable()
               .WithColumn("ParentId").AsGuid().ForeignKey("FK_Folder_ParentId_Folder_FolderId", "EmailFolder", "FolderId")
               .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Table("EmailFolder");
        }
    }
}
