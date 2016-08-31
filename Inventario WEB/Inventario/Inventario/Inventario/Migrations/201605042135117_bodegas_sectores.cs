namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bodegas_sectores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bodegas",
                c => new
                    {
                        BodegaID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        direccion = c.String(),
                        ciudad = c.String(),
                    })
                .PrimaryKey(t => t.BodegaID);
            
            CreateTable(
                "dbo.sectores",
                c => new
                    {
                        sectoresID = c.Int(nullable: false, identity: true),
                        BodegaID = c.Int(nullable: false),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.sectoresID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.sectores");
            DropTable("dbo.Bodegas");
        }
    }
}
