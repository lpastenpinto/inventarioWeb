using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class FormatoExcelMaestro
    {
        public string codigo { get; set; }
        public string codigoBarra { get; set; }
        public string codigoBarraInterno { get; set; }
        public string descripcion { get; set; }
        public double stockMinimo { get; set; }
        public double stockMaximo { get; set; }
        public string bodega { get; set; }

        internal static void agregar(List<FormatoExcelMaestro> datos)
        {
            Context db = new Context();

            List<Bodega> bodegas = new List<Bodega>();

            foreach (FormatoExcelMaestro dato in datos)
            {
                //Si el producto no existe se agrega, sino se actualiza su descripción
                productos esteProducto;

                if (db.productos.Any(s => s.codigo == dato.codigo))
                {
                    esteProducto = db.productos.Where(s => s.codigo == dato.codigo).ToList()[0];
                    esteProducto.descripcion = dato.descripcion;
                    esteProducto.codigoBarra = dato.codigoBarra;
                    esteProducto.codigoBarraInterno = dato.codigoBarraInterno;
                    db.Entry(esteProducto).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    esteProducto = new productos();
                    esteProducto.codigo = dato.codigo;
                    esteProducto.descripcion = dato.descripcion;
                    esteProducto.codigoBarra = dato.codigoBarra;
                    esteProducto.codigoBarraInterno = dato.codigoBarraInterno;

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
                if (db.ProductoBodega.Any(s => s.productosID == esteProducto.productosID && s.bodegaID == estaBodega.BodegaID))
                {
                    productoBodega datoProductoBodega = db.ProductoBodega.Where(s => s.productosID == esteProducto.productosID && s.bodegaID == estaBodega.BodegaID).ToList()[0];

                    datoProductoBodega.cantidadMaxima = dato.stockMaximo;
                    datoProductoBodega.cantidadMinima = dato.stockMinimo;
                    
                    db.Entry(datoProductoBodega).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    productoBodega datoProductoBodega = new productoBodega();
                    datoProductoBodega.alertarStockBajo = true;
                    datoProductoBodega.Bodega = estaBodega;
                    datoProductoBodega.cantidadMaxima = dato.stockMaximo;
                    datoProductoBodega.cantidadMinima = dato.stockMinimo;
                    datoProductoBodega.productos = esteProducto;

                    db.ProductoBodega.Add(datoProductoBodega);
                }
            }
            db.SaveChanges();
        }

    }


}