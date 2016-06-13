namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SodliWasteActFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SolidWasteActHistories", "SolidWasteActId", "dbo.SolidWasteActs");
            AlterColumn("dbo.SolidWasteActHistories", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.SolidWasteActHistories", "CreateDate", c => c.DateTime(nullable: false, defaultValueSql:"0"));
            AddForeignKey("dbo.SolidWasteActHistories", "SolidWasteActId", "dbo.SolidWasteActs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolidWasteActHistories", "SolidWasteActId", "dbo.SolidWasteActs");
            AlterColumn("dbo.SolidWasteActHistories", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SolidWasteActHistories", "Content", c => c.String());
            AddForeignKey("dbo.SolidWasteActHistories", "SolidWasteActId", "dbo.SolidWasteActs", "Id", cascadeDelete: true);
        }
    }
}
