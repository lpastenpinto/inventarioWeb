namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioProductos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productos", "cantidadMinima", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productos", "cantidadMinima");
        }
    }
}
