namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Add_Session_ActivityDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SeassionId", c => c.Guid());
            AddColumn("dbo.Users", "LastActivityDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastActivityDate");
            DropColumn("dbo.Users", "SeassionId");
        }
    }
}
