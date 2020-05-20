namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.System_Form", "Filename", c => c.String(nullable: false));
            DropColumn("dbo.System_Form", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.System_Form", "File", c => c.String(nullable: false));
            DropColumn("dbo.System_Form", "Filename");
        }
    }
}
