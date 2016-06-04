namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_UserDetail_Table_Update_UsersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDetails", "UserId", "dbo.Users");
            DropIndex("dbo.UserDetails", new[] { "UserId" });
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "PrivateNumber", c => c.String());
            AddColumn("dbo.Users", "BirthDate", c => c.DateTime());
            AddColumn("dbo.Users", "JobPosition", c => c.String());
            DropTable("dbo.UserDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(nullable: false, maxLength: 200),
                        PrivateNumber = c.String(maxLength: 100),
                        BirthDate = c.DateTime(),
                        JobPosition = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Users", "JobPosition");
            DropColumn("dbo.Users", "BirthDate");
            DropColumn("dbo.Users", "PrivateNumber");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
            CreateIndex("dbo.UserDetails", "UserId");
            AddForeignKey("dbo.UserDetails", "UserId", "dbo.Users", "Id");
        }
    }
}
