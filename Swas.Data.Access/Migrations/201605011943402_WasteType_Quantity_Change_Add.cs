namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WasteType_Quantity_Change_Add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WasteTypes", "IntervalQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            DropColumn("dbo.WasteTypes", "FromQuantityPrice");
            DropColumn("dbo.WasteTypes", "EndQuantityPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WasteTypes", "EndQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            AddColumn("dbo.WasteTypes", "FromQuantityPrice", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            DropColumn("dbo.WasteTypes", "IntervalQuantityPrice");
        }
    }
}
