using Inventario.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Inventario.Models;

namespace Inventario.Controllers
{
    public class APIController : Controller
    {
        private Context db = new Context();

        // GET: API
        public ActionResult Index()
        {
            return View();
        }

        public void Bodegas()
        {
            string json = JsonConvert.SerializeObject(db.Bodegas);
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
            Response.End();
        }

        public JsonResult jsonDespacho()
        {
            var despachos = db.Despachos.Include(s  => s.Bodega).Include(d => d.Cliente);
            var result = despachos.OrderBy(s => s.NumeroDocumento).ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult jsonDetalleDespacho(int id)
        {
            Despacho despacho = db.Despachos.Find(id);
           
            var resultDetalle = db.DetalleDespacho.Where(s => s.DespachoID == despacho.DespachoID);
            return Json(resultDetalle, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonSectores(int id)
        {
            List<sectores> sectores = db.Sectores.Where(s=>s.BodegaID==id).ToList();

            return Json(sectores, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonDespachoPorSector(string id) {
            var despachos = db.Despachos.Where(s => s.BodegaOrigen == id);//.Include(s => s.Bodega).Include(d => d.Cliente);
            var resultDetSector = despachos.OrderBy(s => s.NumeroDocumento).ToListAsync();
            return Json(resultDetSector, JsonRequestBehavior.AllowGet);
        
        }

    }
}