using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totem.DAL;
using Totem.VO;
using Totem.Util;

namespace Totem.BLL
{
    public class ReporteFinancieroBLL
    {
        private static ReporteFinancieroBLL instance;
        public static ReporteFinancieroBLL getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReporteFinancieroBLL();
                }
                return instance;
            }
            set { instance = value; }
        }

        private ReporteFinancieroBLL()
        {

        }


        /// <summary>
        /// Procesa la carga de datos, alumnos y estados financieros
        /// </summary>
        /// <param name="list"></param>
        /// <param name="alumnos"></param>
        public void loadExcelList(List<ReporteFinanciero> list, List<string> alumnos)
        {

            if (list.Count() == 0 || list == null)
            {
                throw new Exception("Error al leer la planilla");
            }

            //Cada vez que se carga un reporte, se debe limpiar la base de datos
            ReporteFinancieroDAL.deleteAll();

            //Carga los usuarios que fueron separados del excel
            UsuarioAdministradorDAL.loadUsers(alumnos);

            //Procesa los cálculos
            foreach (ReporteFinanciero item in list)
            {
                item.montoInteres = calcularInteres(item.montoCuota, getDiferenciaDias(item.fechaDocumento, item.fechaVencimiento));
                item.gastoCobranza = calcularGastoCobranza(getDiferenciaDiasAux(getDiferenciaDias(item.fechaDocumento, item.fechaVencimiento)), item.montoCuota);
                item.totalCuota = calcularTotalDeuda(item.montoCuota, item.montoInteres, item.gastoCobranza);
                item.glosaCuota = getGlosaEstadoCuota(item.fechaDocumento, item.fechaVencimiento);
            }

            ReporteFinancieroDAL.loadReports(list);
        }



        public List<ReporteFinanciero> getReporteByRut(string run)
        {
            List<ReporteFinanciero> list = ReporteFinancieroDAL.getReporteByRut(run);

            if (list.Count() == 0 || list == null)
            {
                throw new Exception("Usted no posee deudas hasta la fecha");
            }

            return list;
        }


        /// <summary>
        /// Obtiene la diferencia de días entre la fecha del documento
        /// y la fecha de vencimiento de la cuota
        /// </summary>
        /// <param name="dia"></param>
        /// <param name="vencimiento"></param>
        /// <returns></returns>
        private double getDiferenciaDias(DateTime dia, DateTime vencimiento)
        {
            TimeSpan fecha = dia - vencimiento;
            double diferencia = fecha.Days;
            double totalDiferencia = 0;

            if (diferencia >= 0)
            {
                totalDiferencia = diferencia;
            }

            return totalDiferencia;
        }

        /// <summary>
        /// Auxiliar de la diferencia de días
        /// </summary>
        /// <param name="diferenciaDias"></param>
        /// <returns></returns>
        private double getDiferenciaDiasAux(double diferenciaDias)
        {
            double diferenciaAux = 0;

            if (diferenciaDias < 20)
            {
                diferenciaAux = 0;
            }
            else
            {
                diferenciaAux = diferenciaDias;
            }

            return diferenciaAux;
        }


        /// <summary>
        /// Calcula el interés
        /// </summary>
        /// <param name="valorCuota"></param>
        /// <param name="diferenciaDias"></param>
        /// <returns></returns>
        private double calcularInteres(double valorCuota, double diferenciaDias)
        {
            double interesCalculado = Math.Round((((valorCuota * Constantes.interesFijo) / 100) / 30) * diferenciaDias, 1);
            return interesCalculado;
        }



        private double calcularGastoCobranza(double diferenciaDias, double valorCuota)
        {
            double gastoCobranza = 0;

            if (diferenciaDias > 15)
            {
                gastoCobranza = (valorCuota) * Constantes.gastoCobranzaFijo / 100;
            }

            return Math.Round(gastoCobranza, 1);
        }

        private double calcularTotalDeuda(double valorCuota, double montoInteres, double gastoCobranza)
        {
            return valorCuota + montoInteres + gastoCobranza;
        }


        /// <summary>
        /// Obtiene la descripción el estado
        /// de la cuota
        /// </summary>
        /// <param name="fechaDocumento"></param>
        /// <param name="fechaVencimiento"></param>
        /// <returns></returns>
        private string getGlosaEstadoCuota(DateTime fechaDocumento, DateTime fechaVencimiento)
        {
            TimeSpan fecha = fechaVencimiento - fechaDocumento;
            double diferencia = fecha.Days;

            if (diferencia < 0)
            {
                return "Vencida";
            }
            else
            {
                return "Por vencer";
            }
        }
    }
}
