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

namespace Inventario.Controllers
{
    public class productoBodegasController : Controller
    {
        private Context db = new Context();

        // GET: productoBodegas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productoBodega productoBodega = await db.ProductoBodega.FindAsync(id);
            if (productoBodega == null)
            {
                return HttpNotFound();
            }
            return View(productoBodega);
        }

        public string existe(string productoID, string bodegaID)
        {
            int idBodega = int.Parse(bodegaID);
            int idProducto = int.Parse(productoID);

            if (db.ProductoBodega.Any(s => s.bodegaID == idBodega && s.productosID == idProducto))
            {
                return "true";
            }
            else 
            {
                return "false";
            }
        }

        // GET: productoBodegas/Create
        public ActionResult Create(int? idBodega)
        {
            if (idBodega == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega =  db.Bodegas.Find(idBodega);
            if (bodega == null)
            {
                return HttpNotFound();
            }

            ViewBag.idBodega = idBodega;
            ViewBag.bodegaID = new SelectList(db.Bodegas, "BodegaID", "nombre");
            ViewBag.productosID = new SelectList(db.productos, "productosID", "codigo");
            return View();
        }

        // POST: productoBodegas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "productoBodegaID,productosID,bodegaID,SectorID,cantidadDisponible,cantidadMinima,cantidadMaxima,costoUnitario,alertarStockBajo")] productoBodega productoBodega)
        {
            if (ModelState.IsValid)
            {
                db.ProductoBodega.Add(productoBodega);
                await db.SaveChangesAsync();
                return RedirectToAction("Inventario", "Bodegas", new { bodegaID = productoBodega.bodegaID });
            }

            ViewBag.bodegaID = new SelectList(db.Bodegas, "BodegaID", "nombre", productoBodega.bodegaID);
            ViewBag.productosID = new SelectList(db.productos, "productosID", "codigo", productoBodega.productosID);
            return View(productoBodega);
        }

        // GET: productoBodegas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productoBodega productoBodega = await db.ProductoBodega.FindAsync(id);
            if (productoBodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.bodegaID = new SelectList(db.Bodegas, "BodegaID", "nombre", productoBodega.bodegaID);
            ViewBag.productosID = new SelectList(db.productos, "productosID", "codigo", productoBodega.productosID);
            return View(productoBodega);
        }

        // POST: productoBodegas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "productoBodegaID,productosID,bodegaID,SectorID,cantidadDisponible,cantidadMinima,cantidadMaxima,costoUnitario,alertarStockBajo")] productoBodega productoBodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productoBodega).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Inventario", "Bodegas", new { bodegaID = productoBodega.bodegaID });
            }
            ViewBag.bodegaID = new SelectList(db.Bodegas, "BodegaID", "nombre", productoBodega.bodegaID);
            ViewBag.productosID = new SelectList(db.productos, "productosID", "codigo", productoBodega.productosID);
            return View(productoBodega);
        }

        // GET: productoBodegas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productoBodega productoBodega = await db.ProductoBodega.FindAsync(id);
            if (productoBodega == null)
            {
                return HttpNotFound();
            }
            return View(productoBodega);
        }

        // POST: productoBodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            productoBodega productoBodega = await db.ProductoBodega.FindAsync(id);
            db.ProductoBodega.Remove(productoBodega);
            await db.SaveChangesAsync();
            return RedirectToAction("Inventario", "Bodegas", new { bodegaID = productoBodega.bodegaID });
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
