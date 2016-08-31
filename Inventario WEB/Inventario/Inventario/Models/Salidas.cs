using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Salidas
    {
        public int SalidasID { get; set; }
        [Display(Name = "Producto")]
        public int productosID { get; set; }
        [Display(Name = "Cantidad")]
        public double cantidad { get; set; }
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        internal static double obtenerCantidad(int IDSalida)
        {
            return new Context().Salidas.Find(IDSalida).cantidad;
        }

        public static bool existe(int ID)
        {
            Context db = new Context();
            if (db.Salidas.Find(ID) != null) return true;
            return false;
        }
    }
}