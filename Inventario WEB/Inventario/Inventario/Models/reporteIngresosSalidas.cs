using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class reporteIngresosSalidas
    {
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Fecha { get; set; }
        public DateTime FechaDatetime { get; set; }
        public string Producto { get; set; }
        public string Tipo { get; set; }
        public double Cantidad { get; set; }


        public static List<reporteIngresosSalidas> convertirDatos(DateTime Inicio, DateTime Termino) 
        {
            List<reporteIngresosSalidas> retorno = new List<reporteIngresosSalidas>();
            Context db = new Context();
            Context db2 = new Context();

            foreach (Ingresos ingreso in db.Ingresos.Where(s => s.fecha >= Inicio && s.fecha <= Termino)) 
            {
                reporteIngresosSalidas dato = new reporteIngresosSalidas();

                dato.FechaDatetime = ingreso.fecha;
                dato.Fecha = Helpers.mostrarFecha(ingreso.fecha);
                dato.Cantidad = ingreso.cantidad;
                dato.FechaInicio = Helpers.mostrarFecha(Inicio);
                dato.FechaTermino = Helpers.mostrarFecha(Termino);
                dato.Tipo = "INGRESO";
                dato.Producto = db2.productos.Find(ingreso.productosID).descripcion;

                retorno.Add(dato);
            }

            foreach (Salidas salida in db.Salidas.Where(s => s.fecha >= Inicio && s.fecha <= Termino))
            {
                reporteIngresosSalidas dato = new reporteIngresosSalidas();

                dato.FechaDatetime = salida.fecha;
                dato.Fecha = Helpers.mostrarFecha(salida.fecha);
                dato.Cantidad = -salida.cantidad;
                dato.FechaInicio = Helpers.mostrarFecha(Inicio);
                dato.FechaTermino = Helpers.mostrarFecha(Termino);
                dato.Tipo = "SALIDA";
                dato.Producto = db2.productos.Find(salida.productosID).descripcion;

                retorno.Add(dato);
            }

            return retorno.OrderBy(s => s.FechaDatetime).OrderBy(s => s.Producto).ToList();
        }
    }
}