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
    public class productos
    {
        [Key]
        public int productosID { get; set; }
        [Display(Name = "Código")]
        public string codigo { get; set; }
        [Display(Name = "Codigo de Barra Proveedor")]
        public string codigoBarra { get; set; }
        [Display(Name = "Codigo de Barra Interno")]
        public string codigoBarraInterno { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        internal void guardarIngreso(Ingresos Ingreso)
        {
            Context db = new Context();
            db.Ingresos.Add(Ingreso);
            db.SaveChanges();

            /*registrosInventario Registro = new registrosInventario();
            
            Registro.productoID = this.productosID;
            Registro.descripcion = "INGRESO";
            Registro.fechaRegistrado = DateTime.Now;
            Registro.idOperacion = Ingreso.IngresosID;
            Registro.cantidadPrevia = this.cantidadDisponible;

            this.cantidadDisponible += Ingreso.cantidad;

            Registro.cantidadPosterior = this.cantidadDisponible;//*/
            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();

            //db.RegistrosInventario.Add(Registro);
            db.SaveChanges();
        }

        internal void modificarIngreso(Ingresos ingresos, double cantidadPreviaIngreso)
        {
            Context db = new Context();

            db.Entry(ingresos).State = EntityState.Modified;
            db.SaveChanges();

            /*
            registrosInventario Registro = new registrosInventario();
            Registro.descripcion = "MODIFICACIÓN INGRESO";
            Registro.fechaRegistrado = DateTime.Now;
            Registro.idOperacion = ingresos.IngresosID;
            Registro.productoID = ingresos.productosID;
            Registro.cantidadPrevia = this.cantidadDisponible;

            this.cantidadDisponible += (ingresos.cantidad - cantidadPreviaIngreso);

            Registro.cantidadPosterior = this.cantidadDisponible;//*/

            //db.RegistrosInventario.Add(Registro);
            db.SaveChanges();

            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void eliminarIngreso(Ingresos ingreso)
        {
            Context db = new Context();

            /*registrosInventario Registro = new registrosInventario();

            Registro.descripcion = "ELIMINACIÓN INGRESO";
            Registro.fechaRegistrado = DateTime.Now;
            Registro.idOperacion = ingreso.IngresosID;
            Registro.productoID = ingreso.productosID;
            Registro.cantidadPrevia = this.cantidadDisponible;

            this.cantidadDisponible -= ingreso.cantidad;

            Registro.cantidadPosterior = this.cantidadDisponible;

            db.RegistrosInventario.Add(Registro);//*/

            db.Ingresos.Remove(ingreso);

            db.Entry(this).State = EntityState.Modified;

            db.SaveChanges();
        }

        internal void guardarSalida(Salidas salida)
        {
            Context db = new Context();
            db.Salidas.Add(salida);
            db.SaveChanges();

            /*registrosInventario Registro = new registrosInventario();

            Registro.productoID = this.productosID;
            Registro.descripcion = "SALIDA";
            Registro.fechaRegistrado = DateTime.Now;
            Registro.idOperacion = salida.SalidasID;
            Registro.cantidadPrevia = this.cantidadDisponible;

            this.cantidadDisponible -= salida.cantidad;

            Registro.cantidadPosterior = this.cantidadDisponible;//*/
            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();

            //db.RegistrosInventario.Add(Registro);
            db.SaveChanges();
        }

        internal void modificarSalida(Salidas salida, double cantidadPrevia)
        {
            Context db = new Context();

            db.Entry(salida).State = EntityState.Modified;
            db.SaveChanges();

            /*registrosInventario Registro = new registrosInventario();
            Registro.descripcion = "MODIFICACIÓN SALIDA";
            Registro.fechaRegistrado = DateTime.Now;
            Registro.idOperacion = salida.SalidasID;
            Registro.productoID = salida.productosID;
            Registro.cantidadPrevia = this.cantidadDisponible;

            this.cantidadDisponible -= (salida.cantidad - cantidadPrevia);

            Registro.cantidadPosterior = this.cantidadDisponible;

            db.RegistrosInventario.Add(Registro);//*/
            db.SaveChanges();

            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void eliminarSalida(Salidas salida)
        {
            Context db = new Context();

            /*registrosInventario Registro = new registrosInventario();

            Registro.descripcion = "ELIMINACIÓN INGRESO";
            Registro.fechaRegistrado = DateTime.Now;
            Registro.idOperacion = salida.SalidasID;
            Registro.productoID = salida.productosID;
            Registro.cantidadPrevia = this.cantidadDisponible;

            this.cantidadDisponible += salida.cantidad;

            Registro.cantidadPosterior = this.cantidadDisponible;

            db.RegistrosInventario.Add(Registro);//*/

            db.Salidas.Remove(salida);

            db.Entry(this).State = EntityState.Modified;

            db.SaveChanges();
        }

        public static bool existenAlertas()
        {
            /*Context db = new Context();
            if (db.productos.Where(s => s.alertarStockBajo == true && s.cantidadDisponible < s.cantidadMinima).ToList().Count>0)            
            {
                return true;
            }//*/
            return false;
        }

        public static IEnumerable<productos> listaAlertas()
        {
            Context db = new Context();

            //return db.productos.Where(s => s.alertarStockBajo == true && s.cantidadDisponible < s.cantidadMinima);

            return new List<productos>();
        }

        public static int cantidadAlertas()
        {
            Context db = new Context();
            //return db.productos.Where(s => s.alertarStockBajo == true && s.cantidadDisponible < s.cantidadMinima).ToList().Count;
            return 0;
        }
        public static bool existeProducto(string nombre)
        {
            Context db = new Context();
            if (db.productos.Any(o => o.codigo == nombre))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        public static bool existeCodigoProveedor(string nombre)
        {
            Context db = new Context();
            if (db.productos.Any(o => o.codigoBarra == nombre))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        public static bool existeCodigoInterno(string nombre)
        {
            Context db = new Context();
            if (db.productos.Any(o => o.codigoBarraInterno == nombre))
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