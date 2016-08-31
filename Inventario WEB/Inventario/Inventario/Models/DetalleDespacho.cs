using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class DetalleDespacho
    {
        [Key]
        public int DetalleDespachoID { get; set; }

        [ForeignKey("Despacho")]
        public int DespachoID { get; set; }

        [ForeignKey("productos")]
        public int productosID { get; set; }

        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double TotalNetoLinea { get; set; }
        public double CostoVigente { get; set; }

        public virtual Despacho Despacho { get; set; }
        public virtual productos productos{get;set;}
    }
}