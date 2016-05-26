namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        SolidWasteActId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4, defaultValueSql: "0"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SolidWasteActs", t => t.SolidWasteActId, cascadeDelete: true)
                .Index(t => t.SolidWasteActId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "SolidWasteActId", "dbo.SolidWasteActs");
            DropIndex("dbo.Payments", new[] { "SolidWasteActId" });
            DropTable("dbo.Payments");
        }
    }
}
