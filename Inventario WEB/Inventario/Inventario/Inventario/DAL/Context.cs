using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Inventario.DAL
{
    public class Context: DbContext
    {

        public Context() : base("Context")
        {

        }

        public System.Data.Entity.DbSet<Inventario.Models.productos> productos { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.Ingresos> Ingresos { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.registrosInventario> RegistrosInventario { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.Bodega> Bodegas { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.sectores> Sectores { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.Salidas> Salidas { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.productoBodega> ProductoBodega { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.Despacho> Despachos { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.DetalleDespacho> DetalleDespacho { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<Inventario.Models.Usuario> Usuarios { get; set; }
    }
}