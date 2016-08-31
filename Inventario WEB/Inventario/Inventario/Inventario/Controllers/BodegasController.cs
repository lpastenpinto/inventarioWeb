using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario.DAL;
using Inventario.Models;
using OfficeOpenXml;

namespace Inventario.Controllers
{
    public class BodegasController : Controller
    {
        private Context db = new Context();

        public JsonResult obtenerSectores(string id)
        {
            int bodegaID = int.Parse(id);
            List<sectores> lista = db.Sectores.Where(s => s.BodegaID == bodegaID).ToList();
            if (lista.Count == 0)
                return null;
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        // GET: Bodegas
        public ActionResult Index()
        {
            return View(db.Bodegas.ToList());
        }

        public ActionResult Inventario(int? bodegaID) 
        {
            if (bodegaID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodegas.Find(bodegaID);
            if (bodega == null)
            {
                return HttpNotFound();
            }

            List<productoBodega> datos = db.ProductoBodega.Where(s => s.bodegaID == bodegaID).ToList();

            ViewBag.BodegaID = bodegaID;

            return View(datos);
        }

        public ActionResult ActualizarExcel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarExcel(FormCollection formCollection) 
        {
            List<FormatoExcelStock> datosIngresados = new List<FormatoExcelStock>();

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
                            FormatoExcelStock datos = new FormatoExcelStock();
                            datos.codigo = workSheet.Cells[rowIterator, 1].Value.ToString();
                            datos.descripcion = workSheet.Cells[rowIterator, 2].Value.ToString();
                            datos.saldo = double.Parse(workSheet.Cells[rowIterator, 3].Value.ToString());
                            datos.costoUnitario = double.Parse(workSheet.Cells[rowIterator, 4].Value.ToString());
                            datos.bodega = workSheet.Cells[rowIterator, 5].Value.ToString();
                            datosIngresados.Add(datos);
                        }
                    }
                    ViewBag.Users = datosIngresados;
                }
            }
            FormatoExcelStock.agregar(datosIngresados);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public string verificarExcel() 
        {
            try
            {
                List<FormatoExcelStock> datosIngresados = new List<FormatoExcelStock>();
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
                                FormatoExcelStock datos = new FormatoExcelStock();
                                datos.codigo = workSheet.Cells[rowIterator, 1].Value.ToString();
                                datos.descripcion = workSheet.Cells[rowIterator, 2].Value.ToString();
                                datos.saldo = double.Parse(workSheet.Cells[rowIterator, 3].Value.ToString());
                                datos.costoUnitario = double.Parse(workSheet.Cells[rowIterator, 4].Value.ToString());
                                datos.bodega = workSheet.Cells[rowIterator, 5].Value.ToString();
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

        public ActionResult Sectores(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodegas.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // GET: Bodegas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodegas.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // GET: Bodegas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bodegas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BodegaID,nombre,direccion,ciudad")] Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.Bodegas.Add(bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bodega);
        }

        public ActionResult CreateSector(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodegas.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.BodegaID = bodega.BodegaID;
            ViewBag.NombreBodega = bodega.nombre;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSector([Bind(Include = "sectoresID,BodegaID,nombre")] sectores sector)
        {
            db.Sectores.Add(sector);
            db.SaveChanges();
            return RedirectToAction("Sectores", new { id = sector.BodegaID });
        }

        // GET: Bodegas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodegas.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // POST: Bodegas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BodegaID,nombre,direccion,ciudad")] Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bodega);
        }

        public ActionResult EditSector(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sectores sector = db.Sectores.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSector([Bind(Include = "sectoresID,BodegaID,nombre")] sectores sector)
        {
            db.Entry(sector).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Sectores", new { id = sector.BodegaID });
        }

        // GET: Bodegas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodegas.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }

            if (db.Despachos.Any(s => s.BodegaID == bodega.BodegaID)) 
            {
                return RedirectToAction("Index");
            }

            return View(bodega);
        }

        // POST: Bodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bodega bodega = db.Bodegas.Find(id);
            db.Bodegas.Remove(bodega);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSector(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sectores sector = db.Sectores.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        // POST: Bodegas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSector(int id)
        {
            sectores Sector = db.Sectores.Find(id);
            int BodegaID = Sector.BodegaID;

            db.Sectores.Remove(Sector);
            db.SaveChanges();
            return RedirectToAction("Sectores", new { id = BodegaID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public string verificarNameBodega(string name_bodega)
        {

            if (Bodega.existeBodega(name_bodega)) return "true";
            else return "false";
        }
    }
}
