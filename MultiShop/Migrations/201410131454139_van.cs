namespace MultiShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class van : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebActionId = c.Int(nullable: false),
                        RoleId = c.String(maxLength: 128),
                        Allowable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebActions", t => t.WebActionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.WebActionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.WebActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Controller = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Permissions", "WebActionId", "dbo.WebActions");
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "WebActionId" });
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropTable("dbo.WebActions");
            DropTable("dbo.Permissions");
        }
    }
}
