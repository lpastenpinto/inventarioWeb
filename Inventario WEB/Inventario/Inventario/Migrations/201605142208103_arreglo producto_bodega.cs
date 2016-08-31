namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class arregloproducto_bodega : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productoBodegas", "bodegaID", c => c.Int(nullable: false));
            AddColumn("dbo.productoBodegas", "cantidadDisponible", c => c.Double(nullable: false));
            AddColumn("dbo.productoBodegas", "cantidadMinima", c => c.Double(nullable: false));
            AddColumn("dbo.productoBodegas", "cantidadMaxima", c => c.Double(nullable: false));
            AddColumn("dbo.productoBodegas", "alertarStockBajo", c => c.Boolean(nullable: false));
            DropColumn("dbo.productoBodegas", "cantidad");
            DropColumn("dbo.productos", "cantidadDisponible");
            DropColumn("dbo.productos", "cantidadMinima");
            DropColumn("dbo.productos", "alertarStockBajo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.productos", "alertarStockBajo", c => c.Boolean(nullable: false));
            AddColumn("dbo.productos", "cantidadMinima", c => c.Double(nullable: false));
            AddColumn("dbo.productos", "cantidadDisponible", c => c.Double(nullable: false));
            AddColumn("dbo.productoBodegas", "cantidad", c => c.Int(nullable: false));
            DropColumn("dbo.productoBodegas", "alertarStockBajo");
            DropColumn("dbo.productoBodegas", "cantidadMaxima");
            DropColumn("dbo.productoBodegas", "cantidadMinima");
            DropColumn("dbo.productoBodegas", "cantidadDisponible");
            DropColumn("dbo.productoBodegas", "bodegaID");
        }
    }
}
