namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alertarStockBajo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productos", "alertarStockBajo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productos", "alertarStockBajo");
        }
    }
}
