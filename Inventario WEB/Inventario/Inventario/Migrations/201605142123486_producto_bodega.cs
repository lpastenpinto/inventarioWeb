namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producto_bodega : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.productoBodegas",
                c => new
                    {
                        productoBodegaID = c.Int(nullable: false, identity: true),
                        productoID = c.Int(nullable: false),
                        sectorID = c.Int(nullable: false),
                        cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.productoBodegaID);
            
            DropColumn("dbo.productos", "sectorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.productos", "sectorID", c => c.Int(nullable: false));
            DropTable("dbo.productoBodegas");
        }
    }
}
