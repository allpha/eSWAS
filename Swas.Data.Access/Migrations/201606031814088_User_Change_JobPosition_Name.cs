namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Change_JobPosition_Name : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "JobPosition", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "JobPosition", c => c.String());
        }
    }
}
