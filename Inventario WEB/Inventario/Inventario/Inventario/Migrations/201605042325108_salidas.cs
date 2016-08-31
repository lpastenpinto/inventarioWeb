namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salidas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Salidas",
                c => new
                    {
                        SalidasID = c.Int(nullable: false, identity: true),
                        productosID = c.Int(nullable: false),
                        cantidad = c.Double(nullable: false),
                        fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SalidasID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Salidas");
        }
    }
}
