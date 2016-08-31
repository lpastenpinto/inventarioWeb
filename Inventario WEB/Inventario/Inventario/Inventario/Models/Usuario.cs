using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string nombreUsuario { get; set; }
        public string password { get; set; }
        public string nombreCompleto { get; set; }
        public string rut { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
    }
}