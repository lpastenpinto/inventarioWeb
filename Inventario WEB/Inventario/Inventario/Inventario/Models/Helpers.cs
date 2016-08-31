using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Helpers
    {

        public static DateTime convertirFecha(string FECHA) 
        {
            string dia = FECHA.Split('/')[0];
            string mes = FECHA.Split('/')[1];
            string año = FECHA.Split('/')[2];

            if (año.Length > 4) año = año.Substring(0, 4);

            return new DateTime(int.Parse(año), int.Parse(mes), int.Parse(dia));
        }

        public static DateTime convertirFechaMesPrimero(string FECHA)
        {
            string dia = FECHA.Split('/')[1];
            string mes = FECHA.Split('/')[0];
            string año = FECHA.Split('/')[2];

            if (año.Length > 4) año = año.Substring(0, 4);

            return new DateTime(int.Parse(año), int.Parse(mes), int.Parse(dia));
        }

        public static string mostrarFecha(DateTime fecha) {
            return mostrarDiaoMes(fecha.Day) + "/" + mostrarDiaoMes(fecha.Month) + "/" + fecha.Year;            
        }

        public static string mostrarDiaoMes(int diaoMes) 
        {
            if (diaoMes < 10) return "0" + diaoMes;
            else return diaoMes.ToString();
        }

        internal static string valorString(object p)
        {
            if(p==null)
            {
                return "";
            }
            else return p.ToString();
        }

        internal static double ParseDouble(string p)
        {
            if (p == "") return 0;
            double temp = 0;
            if (double.TryParse(p, out temp))
            {
                return double.Parse(p);
            }
            else return 0;
        }
    }
}