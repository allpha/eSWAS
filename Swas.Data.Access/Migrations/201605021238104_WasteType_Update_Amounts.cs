namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WasteType_Update_Amounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WasteTypes", "MunicipalityLessQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 3));
            AddColumn("dbo.WasteTypes", "MunicipalityIntervalQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 3));
            AddColumn("dbo.WasteTypes", "MunicipalityMoreQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            AddColumn("dbo.WasteTypes", "LegalPersonLessQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 3));
            AddColumn("dbo.WasteTypes", "LegalPersonIntervalQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 3));
            AddColumn("dbo.WasteTypes", "LegalPersonMoreQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            AddColumn("dbo.WasteTypes", "PhysicalPersonLessQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 3));
            AddColumn("dbo.WasteTypes", "PhysicalPersonIntervalQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 3));
            AddColumn("dbo.WasteTypes", "PhysicalPersonMoreQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            DropColumn("dbo.WasteTypes", "LessQuantityPrice");
            DropColumn("dbo.WasteTypes", "IntervalQuantityPrice");
            DropColumn("dbo.WasteTypes", "MoreQuantityPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WasteTypes", "MoreQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            AddColumn("dbo.WasteTypes", "IntervalQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            AddColumn("dbo.WasteTypes", "LessQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            DropColumn("dbo.WasteTypes", "PhysicalPersonMoreQuantityPrice");
            DropColumn("dbo.WasteTypes", "PhysicalPersonIntervalQuantityPrice");
            DropColumn("dbo.WasteTypes", "PhysicalPersonLessQuantityPrice");
            DropColumn("dbo.WasteTypes", "LegalPersonMoreQuantityPrice");
            DropColumn("dbo.WasteTypes", "LegalPersonIntervalQuantityPrice");
            DropColumn("dbo.WasteTypes", "LegalPersonLessQuantityPrice");
            DropColumn("dbo.WasteTypes", "MunicipalityMoreQuantityPrice");
            DropColumn("dbo.WasteTypes", "MunicipalityIntervalQuantityPrice");
            DropColumn("dbo.WasteTypes", "MunicipalityLessQuantityPrice");
        }
    }
}
