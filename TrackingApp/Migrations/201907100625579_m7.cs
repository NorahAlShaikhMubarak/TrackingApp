namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileDetails", "system_Form_Form_id", "dbo.System_Form");
            DropIndex("dbo.FileDetails", new[] { "system_Form_Form_id" });
            RenameColumn(table: "dbo.FileDetails", name: "system_Form_Form_id", newName: "Form_id");
            AlterColumn("dbo.FileDetails", "Form_id", c => c.Int(nullable: false));
            CreateIndex("dbo.FileDetails", "Form_id");
            AddForeignKey("dbo.FileDetails", "Form_id", "dbo.System_Form", "Form_id", cascadeDelete: true);
            DropColumn("dbo.FileDetails", "SupportId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileDetails", "SupportId", c => c.Int(nullable: false));
            DropForeignKey("dbo.FileDetails", "Form_id", "dbo.System_Form");
            DropIndex("dbo.FileDetails", new[] { "Form_id" });
            AlterColumn("dbo.FileDetails", "Form_id", c => c.Int());
            RenameColumn(table: "dbo.FileDetails", name: "Form_id", newName: "system_Form_Form_id");
            CreateIndex("dbo.FileDetails", "system_Form_Form_id");
            AddForeignKey("dbo.FileDetails", "system_Form_Form_id", "dbo.System_Form", "Form_id");
        }
    }
}
