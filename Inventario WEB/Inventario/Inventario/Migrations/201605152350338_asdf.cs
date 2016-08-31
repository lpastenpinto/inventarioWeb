namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productoBodegas", "productosID", c => c.Int(nullable: false));
            CreateIndex("dbo.productoBodegas", "productosID");
            DropColumn("dbo.productoBodegas", "productoID");
            AddForeignKey("dbo.productoBodegas", "productosID", "dbo.productos", "productosID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.productoBodegas", "productoID", c => c.Int(nullable: false));
            DropIndex("dbo.productoBodegas", new[] { "productosID" });
            DropColumn("dbo.productoBodegas", "productosID");
            DropForeignKey("dbo.productoBodegas", "productosID", "dbo.productos");
        }
    }
}
