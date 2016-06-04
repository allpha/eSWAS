namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users_Update_CreateDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
