using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario.Models;
using Inventario.DAL;
using OfficeOpenXml;

namespace Inventario.Controllers
{
    public class productosController : Controller
    {
        private Context db = new Context();

        // GET: productos
        public ActionResult Index()
        {
            return View(db.productos.ToList());
        }

        // GET: productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: productos/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ActualizarExcel()         
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarExcel(FormCollection formCollection)
        {
            List<FormatoExcelMaestro> datosIngresados = new List<FormatoExcelMaestro>();

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
                            FormatoExcelMaestro datos = new FormatoExcelMaestro();
                            datos.codigo = workSheet.Cells[rowIterator, 1].Value.ToString();
                            datos.codigoBarra = workSheet.Cells[rowIterator, 2].Value.ToString();
                            datos.codigoBarraInterno = workSheet.Cells[rowIterator, 3].Value.ToString();
                            datos.descripcion = workSheet.Cells[rowIterator, 4].Value.ToString();
                            datos.stockMinimo = double.Parse(workSheet.Cells[rowIterator, 5].Value.ToString());
                            datos.stockMaximo = double.Parse(workSheet.Cells[rowIterator, 6].Value.ToString());
                            datos.bodega = workSheet.Cells[rowIterator, 7].Value.ToString();

                            datosIngresados.Add(datos);
                        }
                    }
                    ViewBag.Users = datosIngresados;
                }
            }
            FormatoExcelMaestro.agregar(datosIngresados);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public string verificarExcel()
        {
            try
            {
                List<FormatoExcelMaestro> datosIngresados = new List<FormatoExcelMaestro>();
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
                                FormatoExcelMaestro datos = new FormatoExcelMaestro();
                                datos.codigo = workSheet.Cells[rowIterator, 1].Value.ToString();
                                datos.codigoBarra = workSheet.Cells[rowIterator, 2].Value.ToString();
                                datos.codigoBarraInterno = workSheet.Cells[rowIterator, 3].Value.ToString();
                                datos.descripcion = workSheet.Cells[rowIterator, 4].Value.ToString();
                                datos.stockMinimo = double.Parse(workSheet.Cells[rowIterator, 5].Value.ToString());
                                datos.stockMaximo = double.Parse(workSheet.Cells[rowIterator, 6].Value.ToString());
                                datos.bodega = workSheet.Cells[rowIterator, 7].Value.ToString();

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

        // POST: productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productosID,codigo,codigoBarra,codigoBarraInterno,descripcion")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productos);
        }

        // GET: productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productosID,codigo,codigoBarra,codigoBarraInterno,descripcion")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }

            List<DetalleDespacho> detalleDespacho = db.DetalleDespacho.ToList();

            if (detalleDespacho.Count == 0) 
            {
                Console.Write("hola");
            }

            if(db.DetalleDespacho.Any(s=>s.productosID==productos.productosID)){
                return RedirectToAction("Index");
            }

            return View(productos);
        }

        // POST: productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos productos = db.productos.Find(id);
            db.productos.Remove(productos);
            db.SaveChanges();
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
        public string verificarCodigo(string valor_codigo)
        {
  
                if (productos.existeProducto(valor_codigo)) return "true";
                else return "false";
         }
        public string verificarCodigoProveedor(string valor_proveedor)
        {

            if (productos.existeCodigoProveedor(valor_proveedor)) return "true";
            else return "false";
        }
        public string verificarCodigoInterno(string valor_interno)
        {

            if (productos.existeCodigoInterno(valor_interno)) return "true";
            else return "false";
        }
    }
}
