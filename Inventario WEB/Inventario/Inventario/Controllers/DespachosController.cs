using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario.DAL;
using Inventario.Models;
using OfficeOpenXml;

namespace Inventario.Controllers
{
    public class DespachosController : Controller
    {
        private Context db = new Context();

        // GET: Despachos
        public async Task<ActionResult> Index()
        {
            var despachos = db.Despachos.Include(d => d.Bodega).Include(d => d.Cliente);
            return View(await despachos.OrderBy(s=>s.NumeroDocumento).ToListAsync());
        }

        public string verificarNumeroDespacho(string numero) 
        {
            int numeroInt = 0;
            if (!int.TryParse(numero, out numeroInt)) 
            {
                return "null";
            }

            if (db.Despachos.Any(s => s.NumeroDocumento == numeroInt)) 
            {
                return "true";
            }

            return "false";
        }

        public ActionResult ActualizarExcel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarExcel(FormCollection formCollection)
        {
            List<FormatoExcelBaseDespacho> datosIngresados = new List<FormatoExcelBaseDespacho>();

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            FormatoExcelBaseDespacho datos = new FormatoExcelBaseDespacho();
                            datos.nombreDocumento = Helpers.valorString(workSheet.Cells[rowIterator, 1].Value);
                            datos.numeroDocumento = int.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 2].Value));
                            datos.codigoCliente = Helpers.valorString(workSheet.Cells[rowIterator, 3].Value);
                            datos.nombreCliente = Helpers.valorString(workSheet.Cells[rowIterator, 4].Value);
                            datos.direccionDespacho = Helpers.valorString(workSheet.Cells[rowIterator, 5].Value);
                            datos.codigoLocal = Helpers.valorString(Helpers.valorString(workSheet.Cells[rowIterator, 6].Value));
                            datos.fecha = Helpers.convertirFechaMesPrimero(Helpers.valorString(workSheet.Cells[rowIterator, 7].Value));
                            datos.codigoProducto = Helpers.valorString(workSheet.Cells[rowIterator, 8].Value);
                            datos.descripcionProducto = Helpers.valorString(workSheet.Cells[rowIterator, 9].Value);
                            datos.cantidad = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 10].Value));
                            datos.precioUnitario = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 11].Value));
                            datos.totalNetoLinea = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 12].Value));
                            datos.costoVigente = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 13].Value));
                            datos.bodegaOrigen = Helpers.valorString(workSheet.Cells[rowIterator, 14].Value);
                            datos.lineaOrigen = int.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 15].Value));
                            datos.status = Helpers.valorString(workSheet.Cells[rowIterator, 16].Value);

                            datosIngresados.Add(datos);
                        }
                    }
                    ViewBag.Users = datosIngresados;
                }
            }
            FormatoExcelBaseDespacho.agregar(datosIngresados);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public string verificarExcel()
        {
            try
            {
                List<FormatoExcelBaseDespacho> datosIngresados = new List<FormatoExcelBaseDespacho>();

                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["file-0"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                FormatoExcelBaseDespacho datos = new FormatoExcelBaseDespacho();
                                datos.nombreDocumento = Helpers.valorString(workSheet.Cells[rowIterator, 1].Value);
                                datos.numeroDocumento = int.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 2].Value));
                                datos.codigoCliente = Helpers.valorString(workSheet.Cells[rowIterator, 3].Value);
                                datos.nombreCliente = Helpers.valorString(workSheet.Cells[rowIterator, 4].Value);
                                datos.direccionDespacho = Helpers.valorString(workSheet.Cells[rowIterator, 5].Value);
                                datos.codigoLocal = Helpers.valorString(Helpers.valorString(workSheet.Cells[rowIterator, 6].Value));
                                datos.fecha = Helpers.convertirFechaMesPrimero(Helpers.valorString(workSheet.Cells[rowIterator, 7].Value));
                                datos.codigoProducto = Helpers.valorString(workSheet.Cells[rowIterator, 8].Value);
                                datos.descripcionProducto = Helpers.valorString(workSheet.Cells[rowIterator, 9].Value);
                                datos.cantidad = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 10].Value));
                                datos.precioUnitario = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 11].Value));
                                datos.totalNetoLinea = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 12].Value));
                                datos.costoVigente = double.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 13].Value));
                                datos.bodegaOrigen = Helpers.valorString(workSheet.Cells[rowIterator, 14].Value);
                                datos.lineaOrigen = int.Parse(Helpers.valorString(workSheet.Cells[rowIterator, 15].Value));
                                datos.status = Helpers.valorString(workSheet.Cells[rowIterator, 16].Value);

                                datosIngresados.Add(datos);
                            }
                        }
                    }
                }
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        // GET: Despachos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despacho despacho = await db.Despachos.FindAsync(id);
            
            if (despacho == null)
            {
                return HttpNotFound();
            }

            ViewBag.DetalleDespacho = db.DetalleDespacho.Where(s => s.DespachoID == despacho.DespachoID);

            return View(despacho);
        }

        // GET: Despachos/Create
        public ActionResult Create()
        {
            ViewBag.BodegaID = new SelectList(db.Bodegas, "BodegaID", "nombre");
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "codigoCliente");
            return View();
        }

        // POST: Despachos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DespachoID,NombreDocumento,NumeroDocumento,ClienteID,DireccionDespacho,BodegaID,Fecha,BodegaOrigen,Linea,Status")] Despacho despacho, FormCollection post)
        {

            despacho.Fecha = Helpers.convertirFecha(post["Fecha"]);

            //Se obtiene el detalle:
            string[] productoID = Request.Form.GetValues("producto");
            string[] cantidad = Request.Form.GetValues("cantidad");
            string[] precioUnitario = Request.Form.GetValues("precioUnitario");
            string[] costoVigente = Request.Form.GetValues("costoVigente");
            string[] totalNetoLinea = Request.Form.GetValues("totalNetoLinea");

            List<DetalleDespacho> detalleDespacho = new List<DetalleDespacho>();

            for (int i = 0; i < productoID.Length; i++)
            {
                DetalleDespacho nuevo = new DetalleDespacho();

                nuevo.Despacho = despacho;

                nuevo.productos = db.productos.Find(int.Parse(productoID[i]));
                nuevo.Cantidad = double.Parse(cantidad[i]);
                nuevo.CostoVigente = double.Parse(costoVigente[i]);
                nuevo.PrecioUnitario = double.Parse(precioUnitario[i]);
                nuevo.TotalNetoLinea = double.Parse(totalNetoLinea[i]);

                detalleDespacho.Add(nuevo);
            }

            db.Despachos.Add(despacho);
            db.DetalleDespacho.AddRange(detalleDespacho);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Despachos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despacho despacho = await db.Despachos.FindAsync(id);
            if (despacho == null)
            {
                return HttpNotFound();
            }
            ViewBag.BodegaID = new SelectList(db.Bodegas, "BodegaID", "nombre", despacho.BodegaID);
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "codigoCliente", despacho.ClienteID);

            ViewBag.DetalleDespacho = db.DetalleDespacho.Where(s => s.DespachoID == despacho.DespachoID).ToList();

            return View(despacho);
        }

        // POST: Despachos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DespachoID,NombreDocumento,NumeroDocumento,ClienteID,DireccionDespacho,BodegaID,Fecha,BodegaOrigen,Linea,Status")] Despacho despacho, FormCollection post)
        {
            despacho.Fecha = Helpers.convertirFecha(post["Fecha"]);

            //Se obtiene el detalle:
            string[] detalleID = Request.Form.GetValues("ID");
            string[] productoID = Request.Form.GetValues("producto");
            string[] cantidad = Request.Form.GetValues("cantidad");
            string[] precioUnitario = Request.Form.GetValues("precioUnitario");
            string[] costoVigente = Request.Form.GetValues("costoVigente");
            string[] totalNetoLinea = Request.Form.GetValues("totalNetoLinea");

            List<DetalleDespacho> detalleDespachoNuevo = new List<DetalleDespacho>();
            List<DetalleDespacho> detalleExistente = new List<DetalleDespacho>();

            for (int i = 0; i < productoID.Length; i++)
            {

                if (detalleID[i].Equals("-1"))
                {
                    DetalleDespacho nuevo = new DetalleDespacho();

                    nuevo.Despacho = despacho;

                    nuevo.productos = db.productos.Find(int.Parse(productoID[i]));
                    nuevo.Cantidad = double.Parse(cantidad[i]);
                    nuevo.CostoVigente = double.Parse(costoVigente[i]);
                    nuevo.PrecioUnitario = double.Parse(precioUnitario[i]);
                    nuevo.TotalNetoLinea = double.Parse(totalNetoLinea[i]);

                    detalleDespachoNuevo.Add(nuevo);
                }
                else 
                {
                    DetalleDespacho nuevo = db.DetalleDespacho.Find(int.Parse(detalleID[i]));

                    nuevo.Despacho = despacho;

                    nuevo.productos = db.productos.Find(int.Parse(productoID[i]));
                    nuevo.Cantidad = double.Parse(cantidad[i]);
                    nuevo.CostoVigente = double.Parse(costoVigente[i]);
                    nuevo.PrecioUnitario = double.Parse(precioUnitario[i]);
                    nuevo.TotalNetoLinea = double.Parse(totalNetoLinea[i]);

                    db.Entry(nuevo).State = EntityState.Modified;
                    detalleExistente.Add(nuevo);
                }
            }

            foreach (DetalleDespacho detalle in db.DetalleDespacho.Where(s => s.DespachoID == despacho.DespachoID)) 
            {
                if (!detalleExistente.Any(s => s.DetalleDespachoID == detalle.DetalleDespachoID)) 
                {
                    db.DetalleDespacho.Remove(detalle);
                }
            }

            db.Despachos.Add(despacho);
            db.DetalleDespacho.AddRange(detalleDespachoNuevo);

            db.Entry(despacho).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Despachos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despacho despacho = await db.Despachos.FindAsync(id);
            if (despacho == null)
            {
                return HttpNotFound();
            }

            ViewBag.DetalleDespacho = db.DetalleDespacho.Where(s => s.DespachoID == despacho.DespachoID);

            return View(despacho);
        }

        // POST: Despachos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Despacho despacho = await db.Despachos.FindAsync(id);

            db.DetalleDespacho.RemoveRange(db.DetalleDespacho.Where(s => s.DespachoID == despacho.DespachoID));

            db.Despachos.Remove(despacho);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}
