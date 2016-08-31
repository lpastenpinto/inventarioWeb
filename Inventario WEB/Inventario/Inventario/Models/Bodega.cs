using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Bodega
    {
        [Key]
        public int BodegaID { get; set; }
        [Display(Name="Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        [Display(Name = "Ciudad")]
        public string ciudad { get; set; }

        internal static Bodega agregar(Bodega estaBodega)
        {
            Context db = new Context();
            db.Bodegas.Add(estaBodega);
            db.SaveChanges();
            return estaBodega;
        }
        public static bool existeBodega(string nombre)
        {
            //int idcliente = Int32.Parse(nombre);
            Context db = new Context();
            if (db.Bodegas.Any(o => o.nombre == nombre))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}