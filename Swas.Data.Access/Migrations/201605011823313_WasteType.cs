namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WasteType : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Landfills",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 30),
            //            RegionId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Regions", t => t.RegionId)
            //    .Index(t => t.RegionId);
            
            //CreateTable(
            //    "dbo.Regions",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 30),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WasteTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LessQuantity = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LessQuantityPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        FromQuantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                        FromQuantityPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        EndQuantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                        EndQuantityPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        MoreQuantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                        MoreQuantityPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Landfills", "RegionId", "dbo.Regions");
            //DropIndex("dbo.Landfills", new[] { "RegionId" });
            DropTable("dbo.WasteTypes");
            //DropTable("dbo.Regions");
            //DropTable("dbo.Landfills");
        }
    }
}
