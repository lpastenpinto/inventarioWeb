namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ingresosB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingresos",
                c => new
                    {
                        IngresosID = c.Int(nullable: false, identity: true),
                        productosID = c.Int(nullable: false),
                        cantidad = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IngresosID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ingresos");
        }
    }
}
