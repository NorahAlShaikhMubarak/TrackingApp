namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.System_Form", "File", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.System_Form", "File");
        }
    }
}
