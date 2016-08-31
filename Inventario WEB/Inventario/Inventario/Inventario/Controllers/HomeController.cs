using Inventario.DAL;
using Inventario.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Excel() 
        {
            return View();
        }

        public ActionResult Index()
        {
           
            return View();
        }

        private void borrarTodosLosDatos()
        {
            Context db = new Context();
            db.Bodegas.RemoveRange(db.Bodegas);
            db.productos.RemoveRange(db.productos);
            db.Ingresos.RemoveRange(db.Ingresos);
            db.Salidas.RemoveRange(db.Salidas);
            db.ProductoBodega.RemoveRange(db.ProductoBodega);
            db.Sectores.RemoveRange(db.Sectores);
            db.Despachos.RemoveRange(db.Despachos);
            db.DetalleDespacho.RemoveRange(db.DetalleDespacho);
            db.Clientes.RemoveRange(db.Clientes);
            db.SaveChanges();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}