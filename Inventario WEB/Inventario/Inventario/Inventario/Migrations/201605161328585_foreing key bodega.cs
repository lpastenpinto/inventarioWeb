namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreingkeybodega : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.productoBodegas", "bodegaID");
            AddForeignKey("dbo.productoBodegas", "bodegaID", "dbo.Bodegas", "BodegaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.productoBodegas", "bodegaID", "dbo.Bodegas");
            DropIndex("dbo.productoBodegas", new[] { "bodegaID" });
        }
    }
}
