using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class sectores
    {
        public int sectoresID { get; set; }
        [Display(Name="Bodega")]
        public int BodegaID { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
    }
}