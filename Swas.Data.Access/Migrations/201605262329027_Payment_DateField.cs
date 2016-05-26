namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment_DateField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "SolidWasteActId", "dbo.SolidWasteActs");
            AddColumn("dbo.Payments", "PayDate", c => c.DateTime(nullable: false));
            AddForeignKey("dbo.Payments", "SolidWasteActId", "dbo.SolidWasteActs", "Id");
            DropColumn("dbo.Payments", "StartDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "StartDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Payments", "SolidWasteActId", "dbo.SolidWasteActs");
            DropColumn("dbo.Payments", "PayDate");
            AddForeignKey("dbo.Payments", "SolidWasteActId", "dbo.SolidWasteActs", "Id", cascadeDelete: true);
        }
    }
}
