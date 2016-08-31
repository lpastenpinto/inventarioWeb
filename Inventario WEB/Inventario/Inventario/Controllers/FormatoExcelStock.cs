using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventario.Models;

namespace Inventario.Controllers
{
    class FormatoExcelStock
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public double saldo { get; set; }
        public double costoUnitario { get; set; }
        public string bodega { get; set; }

        internal static void agregar(List<FormatoExcelStock> datos)
        {
            Context db = new Context();

            List<Bodega> bodegas = new List<Bodega>();

            foreach (FormatoExcelStock dato in datos)             
            { 
                //Si el producto no existe se agrega, sino se actualiza su descripción
                productos esteProducto;

                if (db.productos.Any(s => s.codigo == dato.codigo))
                {
                    esteProducto = db.productos.Where(s => s.codigo == dato.codigo).ToList()[0];
                    esteProducto.descripcion = dato.descripcion;
                    db.Entry(esteProducto).State = System.Data.Entity.EntityState.Modified;
                }
                else 
                {
                    esteProducto = new productos();
                    esteProducto.codigo = dato.codigo;
                    esteProducto.descripcion = dato.descripcion;

                    db.productos.Add(esteProducto);
                }

                Bodega estaBodega;

                //Si la bodega no existe se agrega
                if (!db.Bodegas.Any(s => s.nombre == dato.bodega))
                {
                    if (!bodegas.Any(s => s.nombre == dato.bodega))
                    {
                        estaBodega = new Bodega();

                        estaBodega.nombre = dato.bodega;
                        estaBodega.ciudad = "-";
                        estaBodega.direccion = "-";
                        db.Bodegas.Add(estaBodega);
                        bodegas.Add(estaBodega);
                    }
                    else 
                    {
                        estaBodega = bodegas.Where(s => s.nombre == dato.bodega).ToList()[0];
                    }
                }
                else 
                {
                    estaBodega = db.Bodegas.Where(s => s.nombre == dato.bodega).ToList()[0];
                }

                //Si el producto nunca se ha ingresado a la tabla de productos_bodega se agrega, sino se actualizan sus datos
                if (db.ProductoBodega.Any(s => s.productosID == esteProducto.productosID && s.bodegaID==estaBodega.BodegaID))
                {
                    productoBodega datoProductoBodega = db.ProductoBodega.Where(s => s.productosID == esteProducto.productosID && s.bodegaID == estaBodega.BodegaID).ToList()[0];
                    datoProductoBodega.cantidadDisponible = dato.saldo;
                    datoProductoBodega.costoUnitario = dato.costoUnitario;
                    db.Entry(datoProductoBodega).State = System.Data.Entity.EntityState.Modified;
                }
                else 
                {
                    productoBodega datoProductoBodega = new productoBodega();
                    datoProductoBodega.alertarStockBajo = true;
                    datoProductoBodega.Bodega = estaBodega;
                    datoProductoBodega.cantidadDisponible = dato.saldo;
                    datoProductoBodega.cantidadMinima = dato.saldo;
                    datoProductoBodega.costoUnitario = dato.costoUnitario;
                    datoProductoBodega.productos = esteProducto;

                    db.ProductoBodega.Add(datoProductoBodega);
                }                
            }
            db.SaveChanges();
        }
    }
}
