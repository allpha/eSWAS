namespace Swas.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WasteType_Coeficinet_Add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WasteTypes", "Coeficient", c => c.Decimal(nullable: false, precision: 16, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WasteTypes", "Coeficient");
        }
    }
}
