namespace TrackingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.System_Form",
                c => new
                    {
                        Form_id = c.Int(nullable: false, identity: true),
                        User_id = c.Int(nullable: false),
                        Form_note = c.String(),
                        Form_remark = c.String(),
                        Form_title = c.String(),
                        Form_date = c.DateTime(nullable: false),
                        Form_notification = c.Boolean(nullable: false),
                        Form_status = c.String(),
                    })
                .PrimaryKey(t => t.Form_id)
                .ForeignKey("dbo.System_User", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.System_User",
                c => new
                    {
                        User_id = c.Int(nullable: false, identity: true),
                        User_name = c.String(),
                        User_password = c.Int(nullable: false),
                        User_department = c.String(),
                        User_email = c.String(nullable: false),
                        User_badge = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.User_id);
            
            CreateTable(
                "dbo.System_Status_Track",
                c => new
                    {
                        Status_track_id = c.Int(nullable: false, identity: true),
                        Status_track_name = c.String(),
                    })
                .PrimaryKey(t => t.Status_track_id);
            
            CreateTable(
                "dbo.System_Track",
                c => new
                    {
                        Track_id = c.Int(nullable: false, identity: true),
                        Form_id = c.Int(nullable: false),
                        Status_track_id = c.Int(nullable: false),
                        Track_remark = c.String(),
                        Track_title = c.String(),
                        Track_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Track_id)
                .ForeignKey("dbo.System_Form", t => t.Form_id, cascadeDelete: true)
                .ForeignKey("dbo.System_Status_Track", t => t.Status_track_id, cascadeDelete: true)
                .Index(t => t.Form_id)
                .Index(t => t.Status_track_id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.System_Track", "Status_track_id", "dbo.System_Status_Track");
            DropForeignKey("dbo.System_Track", "Form_id", "dbo.System_Form");
            DropForeignKey("dbo.System_Form", "User_id", "dbo.System_User");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.System_Track", new[] { "Status_track_id" });
            DropIndex("dbo.System_Track", new[] { "Form_id" });
            DropIndex("dbo.System_Form", new[] { "User_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.System_Track");
            DropTable("dbo.System_Status_Track");
            DropTable("dbo.System_User");
            DropTable("dbo.System_Form");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
