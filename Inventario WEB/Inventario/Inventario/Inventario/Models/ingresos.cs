using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Ingresos
    {
        public int IngresosID { get; set; }
        [Display(Name = "Producto")]
        public int productosID{ get; set; }
        [Display(Name = "Cantidad")]
        public double cantidad { get; set; }
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        internal static double obtenerCantidad(int ID)
        {
            return new Context().Ingresos.Find(ID).cantidad;
        }

        public static bool existe(int ID)
        {
            Context db = new Context();
            if (db.Ingresos.Find(ID) != null) return true;
            return false;
        }
    }
}