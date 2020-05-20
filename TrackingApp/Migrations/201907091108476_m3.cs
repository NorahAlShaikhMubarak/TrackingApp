namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        SupportId = c.Int(nullable: false),
                        system_Form_Form_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Form", t => t.system_Form_Form_id)
                .Index(t => t.system_Form_Form_id);
            
            DropColumn("dbo.System_Form", "Filename");
        }
        
        public override void Down()
        {
            AddColumn("dbo.System_Form", "Filename", c => c.String(nullable: false));
            DropForeignKey("dbo.FileDetails", "system_Form_Form_id", "dbo.System_Form");
            DropIndex("dbo.FileDetails", new[] { "system_Form_Form_id" });
            DropTable("dbo.FileDetails");
        }
    }
}
