using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using Totem.BLL;
using Totem.Util;
using Totem.VO;

namespace Totem.UI.Paginas
{
    public partial class PanelAdministrador : System.Web.UI.Page
    {
        private UsuarioAdministrador usuario = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario = new UsuarioAdministrador();
                usuario = (UsuarioAdministrador)(Session["administrador"]);
            }
        }

        protected void btnCargaExcel_Click(object sender, EventArgs e)
        {

            List<ReporteFinanciero> list = null;
            List<string> listAlumnos = null;
            OleDbCommand command = null;
            OleDbDataReader reader = null;
            int cont = 0; //Comenzara a leer despues de la primera fila (nombre de columna)


            try
            {
                list = new List<ReporteFinanciero>();
                listAlumnos = new List<string>();

                using (OleDbConnection conn = new OleDbConnection(UtilString.obtenerRutaConexion(fileUp)))
                {
                    conn.Open();
                    command = new OleDbCommand("SELECT * FROM [hh$]", conn);
                    reader = command.ExecuteReader();



                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cont++;

                            if (cont > 1)
                            {
                                if (reader[6].ToString().Equals("") && reader[15].ToString().Equals("Arancel"))
                                {
                                    //crea el objeto de reporte
                                    ReporteFinanciero rep = new ReporteFinanciero();

                                    //guarda los valores

                                    rep.nroCuota = UtilNumero.parseInt(reader[1].ToString());
                                    rep.runAlumno = reader[4].ToString();
                                    rep.fechaVencimiento = DateTime.Parse(reader[13].ToString());
                                    rep.montoCuota = UtilNumero.parseDouble(reader[17].ToString());
                                    rep.fechaDocumento = DateTime.Now;

                                    list.Add(rep);
                                    listAlumnos.Add(rep.runAlumno);
                                }
                            }
                        }
                    }
                }

                var alumnos = ((from usu in listAlumnos select usu).Distinct()).ToList();
                ReporteFinancieroBLL.getInstance.loadExcelList(list, alumnos);
                UtilScript.executeJS("msgSuccessExcel('" + cont.ToString("N0") + " registros cargados');", this.Page, 500);

            }
            catch (Exception ex)
            {
                UtilScript.executeJS("msgRespuesta('" + ex.Message + "','error');", this.Page, 500);
            }


        }
    }
}