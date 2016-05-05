namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class solid_waste_act : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolidWasteActs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ActDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    LandfillId = c.Int(nullable: false),
                    ReceiverId = c.Int(nullable: false),
                    PositionId = c.Int(nullable: false),
                    CustomerId = c.Int(nullable: false),
                    TransporterId = c.Int(nullable: false),
                    RepresentativeId = c.Int(nullable: false),
                    Remark = c.String(),
                    CreateDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Representatives", t => t.RepresentativeId, cascadeDelete: true)
                .ForeignKey("dbo.Transporters", t => t.TransporterId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Landfills", t => t.LandfillId, cascadeDelete: true)
                .ForeignKey("dbo.Receivers", t => t.ReceiverId, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.LandfillId)
                .Index(t => t.ReceiverId)
                .Index(t => t.PositionId)
                .Index(t => t.CustomerId)
                .Index(t => t.TransporterId)
                .Index(t => t.RepresentativeId);

            CreateTable(
                "dbo.Customers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.Int(nullable: false),
                    Name = c.String(),
                    Code = c.String(),
                    ContactInfo = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.CustomerRepresentatives",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CustomerId = c.Int(nullable: false),
                    RepresentativeId = c.Int(nullable: false),
                    TransporterId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Representatives", t => t.RepresentativeId, cascadeDelete: true)
                .ForeignKey("dbo.Transporters", t => t.TransporterId, cascadeDelete: true)
                .Index(t => new { t.CustomerId, t.RepresentativeId, t.TransporterId }, name: "IX_CustomerRepresentative_CustomerId_RepresentativeId_TransporterId");

            CreateTable(
                "dbo.Representatives",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Transporters",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CarNumber = c.String(),
                    CarModel = c.String(),
                    DriverInfo = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Positions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ReceiverPositions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ReceiverId = c.Int(nullable: false),
                    PositionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.Receivers", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => new { t.ReceiverId, t.PositionId }, name: "IX_ReceiverPosition_ReceiverId_PositionId");

            CreateTable(
                "dbo.Receivers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SolidWasteActDetails",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SolidWasteActId = c.Int(nullable: false),
                    WasteTypeId = c.Int(nullable: false),
                    Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SolidWasteActs", t => t.SolidWasteActId, cascadeDelete: true)
                .ForeignKey("dbo.WasteTypes", t => t.WasteTypeId, cascadeDelete: true)
                .Index(t => t.SolidWasteActId)
                .Index(t => t.WasteTypeId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.SolidWasteActDetails", "WasteTypeId", "dbo.WasteTypes");
            DropForeignKey("dbo.SolidWasteActDetails", "SolidWasteActId", "dbo.SolidWasteActs");
            DropForeignKey("dbo.SolidWasteActs", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.SolidWasteActs", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverPositions", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverPositions", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.SolidWasteActs", "LandfillId", "dbo.Landfills");
            DropForeignKey("dbo.SolidWasteActs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SolidWasteActs", "TransporterId", "dbo.Transporters");
            DropForeignKey("dbo.CustomerRepresentatives", "TransporterId", "dbo.Transporters");
            DropForeignKey("dbo.SolidWasteActs", "RepresentativeId", "dbo.Representatives");
            DropForeignKey("dbo.CustomerRepresentatives", "RepresentativeId", "dbo.Representatives");
            DropForeignKey("dbo.CustomerRepresentatives", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SolidWasteActDetails", new[] { "WasteTypeId" });
            DropIndex("dbo.SolidWasteActDetails", new[] { "SolidWasteActId" });
            DropIndex("dbo.ReceiverPositions", "IX_ReceiverPosition_ReceiverId_PositionId");
            DropIndex("dbo.CustomerRepresentatives", "IX_CustomerRepresentative_CustomerId_RepresentativeId_TransporterId");
            DropIndex("dbo.SolidWasteActs", new[] { "RepresentativeId" });
            DropIndex("dbo.SolidWasteActs", new[] { "TransporterId" });
            DropIndex("dbo.SolidWasteActs", new[] { "CustomerId" });
            DropIndex("dbo.SolidWasteActs", new[] { "PositionId" });
            DropIndex("dbo.SolidWasteActs", new[] { "ReceiverId" });
            DropIndex("dbo.SolidWasteActs", new[] { "LandfillId" });
            DropTable("dbo.SolidWasteActDetails");
            DropTable("dbo.Receivers");
            DropTable("dbo.ReceiverPositions");
            DropTable("dbo.Positions");
            DropTable("dbo.Transporters");
            DropTable("dbo.Representatives");
            DropTable("dbo.CustomerRepresentatives");
            DropTable("dbo.Customers");
            DropTable("dbo.SolidWasteActs");
        }
    }
}
