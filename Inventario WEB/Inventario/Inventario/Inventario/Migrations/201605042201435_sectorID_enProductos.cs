namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sectorID_enProductos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productos", "sectorID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productos", "sectorID");
        }
    }
}
