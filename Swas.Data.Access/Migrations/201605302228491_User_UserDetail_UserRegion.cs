namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_UserDetail_UserRegion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRegions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 200),
                        Password = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        UseEmailAsUserName = c.Boolean(nullable: false),
                        MaxAttamptPassword = c.Int(nullable: false, defaultValue: 5, defaultValueSql: "5"),
                        IsLocked = c.Boolean(nullable: false, defaultValue: false, defaultValueSql: "0"),
                        IsDisabled = c.Boolean(nullable: false, defaultValue: false, defaultValueSql: "0"),
                        LastLockedDate = c.DateTime(),
                        LastDisibledDate = c.DateTime(),
                        RoleId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(nullable: false, maxLength: 200),
                        PrivateNumber = c.String(),
                        BirthDate = c.DateTime(),
                        JobPosition = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRegions", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserDetails", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRegions", "RegionId", "dbo.Regions");
            DropIndex("dbo.UserDetails", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.UserRegions", new[] { "RegionId" });
            DropIndex("dbo.UserRegions", new[] { "UserId" });
            DropTable("dbo.UserDetails");
            DropTable("dbo.Users");
            DropTable("dbo.UserRegions");
        }
    }
}
