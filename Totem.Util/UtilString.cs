using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Web.UI.WebControls;

namespace Totem.Util
{
    public class UtilString
    {
        public static string formatearRun(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }

        public static string quitarFormatoRun(string rut)
        {
            string runConFormato = rut;
            string runSinFormato = runConFormato.Replace(".", "");
            runSinFormato = runSinFormato.Replace("-", "");
            return runSinFormato;
        }

        public static string obtenerRutaConexion(FileUpload fileUp)
        {
            string path = "";
            string ruta = "";
            string strCon = "";

            path = System.Web.HttpContext.Current.Server.MapPath("~/");
            fileUp.SaveAs(path + fileUp.FileName);
            ruta = path + fileUp.FileName;

            string strExtension = System.IO.Path.GetExtension(fileUp.FileName);

            if (!(strExtension == ".xlsx"))
            {
                throw new Exception("El archivo seleccionado no es de tipo excel");
            }

            if (strExtension.Length == 0)
            {
                throw new Exception("Debe seleccionar un archivo");
            }

            return strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ruta + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO;\"";
        }
    }
}
