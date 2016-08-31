namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class costo_unitario_por_bodega : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productoBodegas", "costoUnitario", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productoBodegas", "costoUnitario");
        }
    }
}
