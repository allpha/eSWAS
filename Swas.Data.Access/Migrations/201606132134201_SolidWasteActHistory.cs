namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolidWasteActHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolidWasteActHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SolidWasteActId = c.Int(nullable: false),
                        Content = c.String(),
                        CreateDate = c.DateTime(nullable: false,defaultValueSql:("getdate()")),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SolidWasteActs", t => t.SolidWasteActId, cascadeDelete: true)
                .Index(t => t.SolidWasteActId);
            
            AddColumn("dbo.SolidWasteActs", "HasHistory", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SolidWasteActs", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolidWasteActHistories", "SolidWasteActId", "dbo.SolidWasteActs");
            DropIndex("dbo.SolidWasteActHistories", new[] { "SolidWasteActId" });
            AlterColumn("dbo.SolidWasteActs", "CreateDate", c => c.DateTime(nullable: false, defaultValueSql: "0"));
            DropColumn("dbo.SolidWasteActs", "HasHistory");
            DropTable("dbo.SolidWasteActHistories");
        }
    }
}
