namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.System_Track", "Track_title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.System_Track", "Track_title", c => c.String());
        }
    }
}
