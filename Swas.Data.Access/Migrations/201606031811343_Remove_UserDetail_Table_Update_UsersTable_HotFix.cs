namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_UserDetail_Table_Update_UsersTable_HotFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Users", "LastLockedDate");
            DropColumn("dbo.Users", "LastDisibledDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastDisibledDate", c => c.DateTime());
            AddColumn("dbo.Users", "LastLockedDate", c => c.DateTime());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
        }
    }
}
