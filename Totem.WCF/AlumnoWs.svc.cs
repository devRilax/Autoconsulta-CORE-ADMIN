using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Totem.BLL;
using Totem.Util;
using Totem.VO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Totem.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "AlumnoWs" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione AlumnoWs.svc o AlumnoWs.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class AlumnoWs : IAlumnoWs
    {

        /// <summary>
        /// Desarrollado por @Danilo Caro Aparicio, 
        /// Año: 2017
        /// Email: danilocaro21@gmail.com
        /// </summary>

        //Servicio web para la aplicación cliente, que será utilizada por los alumnos



        public string cambiarClaveUsuario(string username, string password)
        {
            UsuarioAlumno usuario = null;
            JObject jsonObject = null;

            try
            {
                usuario = new UsuarioAlumno(username, password);

                if (UsuarioAlumnoBLL.getInstance.cambiarClaveAlumno(usuario))
                {
                    jsonObject = new JObject();
                    jsonObject.Add("status", "ok");
                    jsonObject.Add("mensaje", "Cambio de clave realizado con exito");
                }

            }
            catch (Exception ex)
            {
                jsonObject = new JObject();
                jsonObject.Add("status", "error");
                jsonObject.Add("mensaje", ex.Message);
            }

            return JsonConvert.SerializeObject(jsonObject);
        }

        public string obtenerReporteAlumno(string run)
        {
            List<ReporteFinanciero> reporte = null;
            JArray array = null;

            try
            {

                reporte = ReporteFinancieroBLL.getInstance.getReporteByRut(run);
                array = JArray.FromObject(reporte);

            }
            catch (Exception)
            {

            }

            return JsonConvert.SerializeObject(array);
        }

        public RespuestaReporte obtenerReporteAlumnoWs(string run)
        {
            List<ReporteFinanciero> reporte = null;
            RespuestaReporte respuesta = null;

            try
            {
                reporte = ReporteFinancieroBLL.getInstance.getReporteByRut(run);
                respuesta = new RespuestaReporte();
                respuesta.status = "ok";
                respuesta.mensaje = "success";
                foreach (ReporteFinanciero item in reporte)
                {
                    respuesta.reporteList.Add(item);
                }


            }
            catch (Exception ex)
            {
                respuesta = new RespuestaReporte();
                respuesta.status = "error";
                respuesta.mensaje = ex.Message;
            }

            return respuesta;
        }

        public string validaLoginAlumno(string username, string password)
        {
            UsuarioAlumno usuario = null;
            JObject jsonObject = null;

            try
            {
                usuario = new UsuarioAlumno();
                usuario.run = username;
                usuario.clave = password;

                if (UsuarioAlumnoBLL.getInstance.validarUsuario(usuario))
                {
                    jsonObject = JObject.FromObject(usuario);
                    jsonObject.Add("status", "ok");
                }
                else
                {
                    jsonObject = new JObject();
                    jsonObject.Add("status", "error");
                    jsonObject.Add("msg", "Credenciales inválidas");
                }
            }
            catch (Exception ex)
            {
                jsonObject = new JObject();
                jsonObject.Add("status", "error");
                jsonObject.Add("msg", ex.Message);
            }

            return JsonConvert.SerializeObject(jsonObject);
        }
    }
}
