namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment_FIX : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SolidWasteActHistories", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SolidWasteActHistories", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
