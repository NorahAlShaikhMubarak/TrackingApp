namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.System_Form", "Form_title", c => c.String(nullable: false));
            AlterColumn("dbo.System_User", "User_name", c => c.String(nullable: false));
            AlterColumn("dbo.System_User", "User_department", c => c.String(nullable: false));
            AlterColumn("dbo.System_Status_Track", "Status_track_name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.System_Status_Track", "Status_track_name", c => c.String());
            AlterColumn("dbo.System_User", "User_department", c => c.String());
            AlterColumn("dbo.System_User", "User_name", c => c.String());
            AlterColumn("dbo.System_Form", "Form_title", c => c.String());
        }
    }
}
