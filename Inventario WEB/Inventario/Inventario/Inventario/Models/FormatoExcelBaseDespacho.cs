using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class FormatoExcelBaseDespacho
    {
        public string nombreDocumento { get; set; }
        public int numeroDocumento { get; set; }
        public string codigoCliente { get; set; }
        public string nombreCliente { get; set; }
        public string direccionDespacho { get; set; }
        public string codigoLocal { get; set; }
        public DateTime fecha { get; set; }
        public string codigoProducto { get; set; }
        public string descripcionProducto { get; set; }
        public double cantidad { get; set; }
        public double precioUnitario { get; set; }
        public double totalNetoLinea { get; set; }
        public double costoVigente { get; set; }
        public string bodegaOrigen { get; set; }
        public int lineaOrigen { get; set; }
        public string status { get; set; }
                
        internal static void agregar(List<FormatoExcelBaseDespacho> datos)
        {
            Context db = new Context();

            List<Bodega> bodegas = new List<Bodega>();
            List<Cliente> nuevosClientes = new List<Cliente>();
            List<productos> nuevosProductos = new List<productos>();
            List<Despacho> nuevosDespachos = new List<Despacho>();

            foreach (FormatoExcelBaseDespacho dato in datos)
            {
                //Si el cliente no existe se agrega, sino se actualiza su nombre
                Cliente esteCliente;

                if (db.Clientes.Any(s => s.codigoCliente == dato.codigoCliente))
                {
                    esteCliente = db.Clientes.Where(s => s.codigoCliente == dato.codigoCliente).ToList()[0];
                    esteCliente.nombreCliente = dato.nombreCliente;
                }
                else 
                {
                    if (!nuevosClientes.Any(s => s.codigoCliente == dato.codigoCliente)) 
                    {
                        esteCliente = new Cliente();
                        esteCliente.codigoCliente = dato.codigoCliente;
                        esteCliente.nombreCliente = dato.nombreCliente;
                        db.Clientes.Add(esteCliente);
                        nuevosClientes.Add(esteCliente);
                    }         
                    else
                    {
                        esteCliente = nuevosClientes.Where(s => s.codigoCliente == dato.codigoCliente).ToList()[0];
                    }
                }

                //Si el producto no existe se agrega, sino se actualiza su descripción
                productos esteProducto;

                if (db.productos.Any(s => s.codigo == dato.codigoProducto))
                {
                    esteProducto = db.productos.Where(s => s.codigo == dato.codigoProducto).ToList()[0];
                    esteProducto.descripcion = dato.descripcionProducto;
                    db.Entry(esteProducto).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    if (!nuevosProductos.Any(s => s.codigo == dato.codigoProducto)) 
                    {
                        esteProducto = new productos();
                        esteProducto.codigo = dato.codigoProducto;
                        esteProducto.descripcion = dato.descripcionProducto;

                        db.productos.Add(esteProducto);
                        nuevosProductos.Add(esteProducto);
                    }
                    else 
                    {
                        esteProducto = nuevosProductos.Where(s => s.codigo == dato.codigoProducto).ToList()[0];
                    }
                }

                Bodega estaBodega;

                //Si la bodega no existe se agrega
                if (!db.Bodegas.Any(s => s.nombre == dato.codigoLocal))
                {
                    if (!bodegas.Any(s => s.nombre == dato.codigoLocal))
                    {
                        estaBodega = new Bodega();

                        estaBodega.nombre = dato.codigoLocal;
                        estaBodega.ciudad = "-";
                        estaBodega.direccion = "-";
                        db.Bodegas.Add(estaBodega);
                        bodegas.Add(estaBodega);
                    }
                    else
                    {
                        estaBodega = bodegas.Where(s => s.nombre == dato.codigoLocal).ToList()[0];
                    }
                }
                else
                {
                    estaBodega = db.Bodegas.Where(s => s.nombre == dato.codigoLocal).ToList()[0];
                }

                //Se agregan o modifican los datos de despacho
                if (db.Despachos.Any(s => s.NumeroDocumento == dato.numeroDocumento))
                {
                    Despacho despacho = db.Despachos.Where(s => s.NumeroDocumento==dato.numeroDocumento).ToList()[0];

                    despacho.NombreDocumento = dato.nombreDocumento;
                    despacho.Cliente = esteCliente;
                    despacho.DireccionDespacho = dato.direccionDespacho;
                    despacho.Bodega = estaBodega;
                    despacho.Fecha = dato.fecha;
                    despacho.BodegaOrigen = dato.bodegaOrigen;
                    despacho.Linea = dato.lineaOrigen;
                    //despacho.Status = "NUEVO";

                    //detalle
                    if (db.DetalleDespacho.Any(s => s.DespachoID == despacho.DespachoID && s.productosID == esteProducto.productosID)) 
                    {
                        DetalleDespacho detalle = db.DetalleDespacho.Where(s => s.DespachoID == despacho.DespachoID && s.productosID == esteProducto.productosID).ToList()[0];
                        detalle.productos = esteProducto;
                        detalle.Cantidad = dato.cantidad;
                        detalle.PrecioUnitario = dato.precioUnitario;
                        detalle.TotalNetoLinea = dato.totalNetoLinea;
                        detalle.CostoVigente = dato.costoVigente;
                        db.Entry(detalle).State = System.Data.Entity.EntityState.Modified;
                    }
                    else 
                    {
                        DetalleDespacho detalle = new DetalleDespacho();
                        detalle.Despacho = despacho;
                        detalle.productos = esteProducto;
                        detalle.Cantidad = dato.cantidad;
                        detalle.PrecioUnitario = dato.precioUnitario;
                        detalle.TotalNetoLinea = dato.totalNetoLinea;
                        detalle.CostoVigente = dato.costoVigente;
                        db.DetalleDespacho.Add(detalle);
                    }

                    db.Entry(despacho).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    if (!nuevosDespachos.Any(s => s.NumeroDocumento == dato.numeroDocumento))
                    {
                        Despacho despacho = new Despacho();

                        despacho.NumeroDocumento = dato.numeroDocumento;
                        despacho.NombreDocumento = dato.nombreDocumento;
                        despacho.Cliente = esteCliente;
                        despacho.DireccionDespacho = dato.direccionDespacho;
                        despacho.Bodega = estaBodega;
                        despacho.Fecha = dato.fecha;
                        despacho.BodegaOrigen = dato.bodegaOrigen;
                        despacho.Linea = dato.lineaOrigen;
                        despacho.Status = "CARGADO";

                        //detalle
                        DetalleDespacho detalle = new DetalleDespacho();
                        detalle.Despacho = despacho;
                        detalle.productos = esteProducto;
                        detalle.Cantidad = dato.cantidad;
                        detalle.PrecioUnitario = dato.precioUnitario;
                        detalle.TotalNetoLinea = dato.totalNetoLinea;
                        detalle.CostoVigente = dato.costoVigente;
                        db.DetalleDespacho.Add(detalle);

                        db.Despachos.Add(despacho);
                        nuevosDespachos.Add(despacho);
                    }
                    else 
                    {
                        Despacho despacho = nuevosDespachos.Where(s => s.NumeroDocumento == dato.numeroDocumento).ToList()[0];

                        despacho.NombreDocumento = dato.nombreDocumento;
                        despacho.Cliente = esteCliente;
                        despacho.DireccionDespacho = dato.direccionDespacho;
                        despacho.Bodega = estaBodega;
                        despacho.Fecha = dato.fecha;
                        despacho.BodegaOrigen = dato.bodegaOrigen;
                        despacho.Linea = dato.lineaOrigen;
                        //despacho.Status = "NUEVO";

                        //detalle
                        DetalleDespacho detalle = new DetalleDespacho();
                        detalle.Despacho = despacho;
                        detalle.productos = esteProducto;
                        detalle.Cantidad = dato.cantidad;
                        detalle.PrecioUnitario = dato.precioUnitario;
                        detalle.TotalNetoLinea = dato.totalNetoLinea;
                        detalle.CostoVigente = dato.costoVigente;
                        db.DetalleDespacho.Add(detalle);                        
                    }
                }
            }
            db.SaveChanges();
        }
        //*/
    }
}