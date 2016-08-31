using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Despacho
    {
        [Key]
        public int DespachoID { get; set; }
        [Display(Name="Nombre de Documento")]
        public string NombreDocumento { get; set; }
        [Display(Name = "N° de Documento")]
        public int NumeroDocumento { get; set; }

        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }
        [Display(Name = "Dirección de Despacho")]
        public string DireccionDespacho { get; set; }

        [ForeignKey("Bodega")]
        [Display(Name = "Bodega")]
        public int BodegaID { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Bodega Origen")]
        public string BodegaOrigen { get; set; }
        [Display(Name = "Línea")]
        public int Linea { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Cliente")]
        public virtual Cliente Cliente { get; set; }

        [Display(Name="Bodega")]
        public virtual Bodega Bodega { get; set; }
    }
}