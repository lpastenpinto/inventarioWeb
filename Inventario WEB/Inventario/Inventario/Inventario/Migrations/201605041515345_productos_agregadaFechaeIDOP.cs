namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productos_agregadaFechaeIDOP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.registrosInventarios",
                c => new
                    {
                        registrosInventarioID = c.Int(nullable: false, identity: true),
                        productoID = c.Int(nullable: false),
                        cantidadPrevia = c.Double(nullable: false),
                        cantidadPosterior = c.Double(nullable: false),
                        descripcion = c.String(),
                        fechaRegistrado = c.DateTime(nullable: false),
                        idOperacion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.registrosInventarioID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.registrosInventarios");
        }
    }
}
