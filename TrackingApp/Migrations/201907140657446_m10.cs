namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "User_id", "dbo.System_User");
            DropIndex("dbo.AspNetUsers", new[] { "User_id" });
            DropColumn("dbo.AspNetUsers", "User_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "User_id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "User_id");
            AddForeignKey("dbo.AspNetUsers", "User_id", "dbo.System_User", "User_id", cascadeDelete: true);
        }
    }
}
