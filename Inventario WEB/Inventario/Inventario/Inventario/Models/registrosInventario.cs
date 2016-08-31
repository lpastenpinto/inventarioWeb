using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class registrosInventario
    {
        public int registrosInventarioID { get; set; }
        [Display(Name = "Producto")]
        public int productoID { get; set; }
        [Display(Name = "Cantidad Previa")]
        public double cantidadPrevia { get; set; }
        [Display(Name = "Cantidad Posterior")]
        public double cantidadPosterior { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime fechaRegistrado { get; set; }
        [Display(Name = "ID de Operación")]
        public int idOperacion { get; set; }
    }
}