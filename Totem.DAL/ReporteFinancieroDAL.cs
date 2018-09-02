using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totem.VO;
using Totem.Util;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace Totem.DAL
{
    public class ReporteFinancieroDAL
    {
        public static void loadReports(List<ReporteFinanciero> list)
        {
            OracleConnection con = null;
            OracleCommand command = null;

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                foreach (ReporteFinanciero reporte in list)
                {

                    command = new OracleCommand("PKG_REPORTE.SP_CARGA_DATA", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(":p_run", reporte.runAlumno);
                    command.Parameters.Add(":p_nro_cuota", reporte.nroCuota);
                    command.Parameters.Add(":p_monto_cuota", reporte.montoCuota);
                    command.Parameters.Add(":p_glosa_cuota", reporte.glosaCuota);
                    command.Parameters.Add(":p_interes", reporte.montoInteres);
                    command.Parameters.Add(":p_gasto_cobranza", reporte.gastoCobranza);
                    command.Parameters.Add(":p_total_cuota", reporte.totalCuota);
                    command.Parameters.Add(":p_fec_doc", reporte.fechaDocumento);
                    command.Parameters.Add(":p_fec_venc", reporte.fechaVencimiento);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw new DataAccessException("Error desconocido, contacte al administrador");
            }
            finally
            {
                con.Close();
            }
        }

        public static List<ReporteFinanciero> getReporteByRut(string run)
        {
            OracleConnection con = null;
            OracleCommand command = null;
            OracleDataAdapter adapter = null;
            DataSet ds = null;
            List<ReporteFinanciero> list = null;

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                command = new OracleCommand("PKG_REPORTE.SP_REPORTE_ALUMNO", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(":p_run", run);
                command.Parameters.Add(":p_list", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();

                adapter = new OracleDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds);

                list = new List<ReporteFinanciero>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ReporteFinanciero reporte = new ReporteFinanciero();
                    reporte.nroCuota = UtilNumero.parseInt(row["nro_cuota"].ToString());
                    reporte.montoCuota = UtilNumero.parseInt(row["monto_cuota"].ToString());
                    reporte.glosaCuota = row["glosa_cuota"].ToString();
                    reporte.montoInteres = UtilNumero.parseInt(row["interes"].ToString());
                    reporte.gastoCobranza = UtilNumero.parseInt(row["gasto_cobranza"].ToString());
                    reporte.totalCuota = UtilNumero.parseInt(row["total_cuota"].ToString());

                    list.Add(reporte);
                }
            }
            catch (Exception)
            {
                throw new DataAccessException("Error desconocido, contacte al administrador");
            }
            finally
            {
                con.Close();
            }

            return list;
        }


        //Elimia todos los registros de la tabla "Reporte"
        public static void deleteAll()
        {
            OracleConnection con = null;
            OracleCommand command = null;
            string query = "";

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                query = "DELETE FROM REPORTE_ALUMNO";
                command = new OracleCommand(query, con);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw new DataAccessException("Error desconocido, contacte al administrador");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
