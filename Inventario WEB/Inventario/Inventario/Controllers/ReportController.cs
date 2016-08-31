using Inventario.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        static string deviceInfo = "<DeviceInfo>" +
                 "  <OutputFormat>jpeg</OutputFormat>" +
                 "  <PageWidth>10in</PageWidth>" +
                 "  <PageHeight>13in</PageHeight>" +
                 "  <MarginTop>0.5in</MarginTop>" +
                 "  <MarginLeft>1in</MarginLeft>" +
                 "  <MarginRight>1in</MarginRight>" +
                 "  <MarginBottom>0.5in</MarginBottom>" +
                 "</DeviceInfo>";

        public FileContentResult ReporteIngresosSalidas(string inicio, string termino)
        {
            DateTime Inicio = Helpers.convertirFecha(inicio);
            DateTime Termino = Helpers.convertirFecha(termino);

            return generarReporte("IngresosSalidas", reporteIngresosSalidas.convertirDatos(Inicio, Termino));            
        }

        private FileContentResult generarReporte(string nombre, object datos)
        {
            LocalReport reporte_local = new LocalReport();
            Inventario.DAL.Context db = new Inventario.DAL.Context();
            reporte_local.ReportPath = Server.MapPath("~/Report/" + nombre + ".rdlc");
            ReportDataSource conjunto_datos = new ReportDataSource();
            conjunto_datos.Name = "DataSet1";

            conjunto_datos.Value = datos;

            reporte_local.DataSources.Add(conjunto_datos);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = reporte_local.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //return File(renderedBytes, mimeType, nombre + "_" + Helpers.mostrarFecha(DateTime.Now) + ".pdf");
            return File(renderedBytes, mimeType);
        }
    }
}