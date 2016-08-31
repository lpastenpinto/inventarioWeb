using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }
        [Display(Name = "Código de Cliente")]
        public string codigoCliente { get; set; }
        [Display(Name = "Nombre de Cliente")]
        public string nombreCliente { get; set; }


        public static bool existeCliente(string nombre)
        {
            //int idcliente = Int32.Parse(nombre);
            Context db = new Context();
            if (db.Clientes.Any(o => o.codigoCliente == nombre))
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