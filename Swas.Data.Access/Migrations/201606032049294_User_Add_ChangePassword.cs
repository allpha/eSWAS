namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Add_ChangePassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ChangePassword", c => c.Boolean(nullable: true, defaultValueSql: "1"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ChangePassword");
        }
    }
}
