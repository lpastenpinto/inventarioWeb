namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class despachosyclientes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        codigoCliente = c.String(),
                        nombreCliente = c.String(),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.Despachoes",
                c => new
                    {
                        DespachoID = c.Int(nullable: false, identity: true),
                        NombreDocumento = c.String(),
                        NumeroDocumento = c.Int(nullable: false),
                        ClienteID = c.Int(nullable: false),
                        DireccionDespacho = c.String(),
                        BodegaID = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        BodegaOrigen = c.String(),
                        Linea = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DespachoID)
                .ForeignKey("dbo.Bodegas", t => t.BodegaID, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .Index(t => t.ClienteID)
                .Index(t => t.BodegaID);
            
            CreateTable(
                "dbo.DetalleDespachoes",
                c => new
                    {
                        DetalleDespachoID = c.Int(nullable: false, identity: true),
                        DespachoID = c.Int(nullable: false),
                        productosID = c.Int(nullable: false),
                        Cantidad = c.Double(nullable: false),
                        PrecioUnitario = c.Double(nullable: false),
                        TotalNetoLinea = c.Double(nullable: false),
                        CostoVigente = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleDespachoID)
                .ForeignKey("dbo.Despachoes", t => t.DespachoID, cascadeDelete: true)
                .ForeignKey("dbo.productos", t => t.productosID, cascadeDelete: true)
                .Index(t => t.DespachoID)
                .Index(t => t.productosID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleDespachoes", "productosID", "dbo.productos");
            DropForeignKey("dbo.DetalleDespachoes", "DespachoID", "dbo.Despachoes");
            DropForeignKey("dbo.Despachoes", "ClienteID", "dbo.Clientes");
            DropForeignKey("dbo.Despachoes", "BodegaID", "dbo.Bodegas");
            DropIndex("dbo.DetalleDespachoes", new[] { "productosID" });
            DropIndex("dbo.DetalleDespachoes", new[] { "DespachoID" });
            DropIndex("dbo.Despachoes", new[] { "BodegaID" });
            DropIndex("dbo.Despachoes", new[] { "ClienteID" });
            DropTable("dbo.DetalleDespachoes");
            DropTable("dbo.Despachoes");
            DropTable("dbo.Clientes");
        }
    }
}
