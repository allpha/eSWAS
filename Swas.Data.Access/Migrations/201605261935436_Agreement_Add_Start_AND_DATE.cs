namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agreement_Add_Start_AND_DATE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agreements", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Agreements", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agreements", "EndDate");
            DropColumn("dbo.Agreements", "StartDate");
        }
    }
}
