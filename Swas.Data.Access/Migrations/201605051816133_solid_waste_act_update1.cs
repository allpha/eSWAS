namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solid_waste_act_update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SolidWasteActs", "LandfillId", "dbo.Landfills");
            DropForeignKey("dbo.SolidWasteActs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SolidWasteActs", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.SolidWasteActs", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.SolidWasteActs", "RepresentativeId", "dbo.Representatives");
            DropForeignKey("dbo.SolidWasteActDetails", "SolidWasteActId", "dbo.SolidWasteActs");
            DropForeignKey("dbo.SolidWasteActs", "TransporterId", "dbo.Transporters");
            DropForeignKey("dbo.CustomerRepresentatives", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerRepresentatives", "RepresentativeId", "dbo.Representatives");
            DropForeignKey("dbo.CustomerRepresentatives", "TransporterId", "dbo.Transporters");
            DropForeignKey("dbo.ReceiverPositions", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.ReceiverPositions", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.SolidWasteActDetails", "WasteTypeId", "dbo.WasteTypes");
            AddColumn("dbo.Receivers", "LastName", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.SolidWasteActs", "Remark", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Customers", "Code", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customers", "ContactInfo", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Representatives", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Transporters", "CarNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Transporters", "CarModel", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Transporters", "DriverInfo", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Positions", "Name", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Receivers", "Name", c => c.String(nullable: false, maxLength: 500));
            AddForeignKey("dbo.SolidWasteActs", "LandfillId", "dbo.Landfills", "Id");
            AddForeignKey("dbo.SolidWasteActs", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.SolidWasteActs", "PositionId", "dbo.Positions", "Id");
            AddForeignKey("dbo.SolidWasteActs", "ReceiverId", "dbo.Receivers", "Id");
            AddForeignKey("dbo.SolidWasteActs", "RepresentativeId", "dbo.Representatives", "Id");
            AddForeignKey("dbo.SolidWasteActDetails", "SolidWasteActId", "dbo.SolidWasteActs", "Id");
            AddForeignKey("dbo.SolidWasteActs", "TransporterId", "dbo.Transporters", "Id");
            AddForeignKey("dbo.CustomerRepresentatives", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.CustomerRepresentatives", "RepresentativeId", "dbo.Representatives", "Id");
            AddForeignKey("dbo.CustomerRepresentatives", "TransporterId", "dbo.Transporters", "Id");
            AddForeignKey("dbo.ReceiverPositions", "PositionId", "dbo.Positions", "Id");
            AddForeignKey("dbo.ReceiverPositions", "ReceiverId", "dbo.Receivers", "Id");
            AddForeignKey("dbo.SolidWasteActDetails", "WasteTypeId", "dbo.WasteTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolidWasteActDetails", "WasteTypeId", "dbo.WasteTypes");
            DropForeignKey("dbo.ReceiverPositions", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverPositions", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.CustomerRepresentatives", "TransporterId", "dbo.Transporters");
            DropForeignKey("dbo.CustomerRepresentatives", "RepresentativeId", "dbo.Representatives");
            DropForeignKey("dbo.CustomerRepresentatives", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SolidWasteActs", "TransporterId", "dbo.Transporters");
            DropForeignKey("dbo.SolidWasteActDetails", "SolidWasteActId", "dbo.SolidWasteActs");
            DropForeignKey("dbo.SolidWasteActs", "RepresentativeId", "dbo.Representatives");
            DropForeignKey("dbo.SolidWasteActs", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.SolidWasteActs", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.SolidWasteActs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SolidWasteActs", "LandfillId", "dbo.Landfills");
            AlterColumn("dbo.Receivers", "Name", c => c.String());
            AlterColumn("dbo.Positions", "Name", c => c.String());
            AlterColumn("dbo.Transporters", "DriverInfo", c => c.String());
            AlterColumn("dbo.Transporters", "CarModel", c => c.String());
            AlterColumn("dbo.Transporters", "CarNumber", c => c.String());
            AlterColumn("dbo.Representatives", "Name", c => c.String());
            AlterColumn("dbo.Customers", "ContactInfo", c => c.String());
            AlterColumn("dbo.Customers", "Code", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.SolidWasteActs", "Remark", c => c.String());
            DropColumn("dbo.Receivers", "LastName");
            AddForeignKey("dbo.SolidWasteActDetails", "WasteTypeId", "dbo.WasteTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceiverPositions", "ReceiverId", "dbo.Receivers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceiverPositions", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerRepresentatives", "TransporterId", "dbo.Transporters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerRepresentatives", "RepresentativeId", "dbo.Representatives", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerRepresentatives", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SolidWasteActs", "TransporterId", "dbo.Transporters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SolidWasteActDetails", "SolidWasteActId", "dbo.SolidWasteActs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SolidWasteActs", "RepresentativeId", "dbo.Representatives", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SolidWasteActs", "ReceiverId", "dbo.Receivers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SolidWasteActs", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SolidWasteActs", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SolidWasteActs", "LandfillId", "dbo.Landfills", "Id", cascadeDelete: true);
        }
    }
}
