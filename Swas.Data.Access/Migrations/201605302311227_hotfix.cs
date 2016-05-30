namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserDetails", "PrivateNumber", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDetails", "PrivateNumber", c => c.String());
        }
    }
}
