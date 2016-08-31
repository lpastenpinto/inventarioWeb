namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class arregloproductos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productos", "codigoBarra", c => c.String());
            AddColumn("dbo.productos", "codigoBarraInterno", c => c.String());
            DropColumn("dbo.productos", "precio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.productos", "precio", c => c.Int(nullable: false));
            DropColumn("dbo.productos", "codigoBarraInterno");
            DropColumn("dbo.productos", "codigoBarra");
        }
    }
}
