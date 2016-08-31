using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class productoBodega
    {
        public int productoBodegaID { get; set; }
        
        [ForeignKey("productos")]
        [Display(Name = "Producto")]
        public int productosID { get; set; }

        [ForeignKey("Bodega")]
        [Display(Name = "Bodega")]
        public int bodegaID { get; set; }

        [Display(Name = "Sector")]
        public int SectorID { get; set; }
        [Display(Name = "Cantidad Disponible")]
        public double cantidadDisponible { get; set; }
        [Display(Name = "Cantidad Mínima")]
        public double cantidadMinima { get; set; }
        [Display(Name = "Cantidad Máxima")]
        public double cantidadMaxima { get; set; }
        [Display(Name = "Costo Unitario")]
        public double costoUnitario { get; set; }
        [Display(Name = "Alertar por Stock Bajo")]
        public bool alertarStockBajo { get; set; }

        public virtual productos productos { get; set; }
        public virtual Bodega Bodega { get; set; }
    }
}