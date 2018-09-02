using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Totem.Util;
using Totem.VO;

namespace Totem.DAL
{
    public class UsuarioAdministradorDAL
    {
        /// <summary>
        /// Consulta la base de datos en base a los datos ingresados
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static bool validateUser(UsuarioAdministrador usuario)
        {
            OracleConnection con = null;
            OracleCommand command = null;
            OracleDataReader reader = null;
            string query = "";
            bool existeUsuario = false;

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                query = "SELECT nombre_usuario, clave FROM usuario_admin WHERE nombre_usuario = :p_username AND clave = :p_clave";
                command = new OracleCommand(query, con);
                command.Parameters.Add(new OracleParameter("p_username", usuario.username));
                command.Parameters.Add(new OracleParameter("p_clave", usuario.clave));

                reader = command.ExecuteReader();

                //Si hay filas
                if (reader.HasRows)
                {
                    //Si logra leer
                    if (reader.Read())
                    {
                        existeUsuario = true;
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Error desconocido, contacte al administrador");
            }
            finally
            {
                con.Close();
            }

            return existeUsuario;
        }

        public static void changeUserPassword(UsuarioAdministrador user)
        {
            OracleConnection con = null;
            OracleCommand command = null;
            string query = "";

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                query = "UPDATE usuario_admin SET clave = :p_nuevaClave WHERE nombre_usuario = :p_username";
                command = new OracleCommand(query, con);
                command.Parameters.Add(new OracleParameter("p_nuevaClave", user.clave));
                command.Parameters.Add(new OracleParameter("p_username", user.username));
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw new Exception("Error desconocido, contacte al administrador");
            }
            finally
            {
                con.Close();
            }

        }

        public static bool validatePasswordUser(UsuarioAdministrador user, string currentPassword)
        {
            OracleConnection con = null;
            OracleCommand command = null;
            OracleDataReader reader = null;
            string query = "";
            bool isValid = false;

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                query = "SELECT clave FROM usuario_admin WHERE nombre_usuario = :p_username AND clave =:p_clave";
                command = new OracleCommand(query, con);
                command.Parameters.Add(new OracleParameter("p_username", user.username));
                command.Parameters.Add(new OracleParameter("p_clave", currentPassword));

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        isValid = true;
                    }
                }

            }
            catch (Exception)
            {
                throw new Exception("Error desconocido, contacte al administrador");
            }
            finally
            {
                con.Close();
            }

            return isValid;
        }

        /// <summary>
        /// Carga todos los rut de la planilla excel, para ser cargados en una tabla
        /// usuarios
        /// </summary>
        /// <param name="list">Listado de ruts que son extraidos desde la planilla base</param>
        public static void loadUsers(List<string> list)
        {
            OracleConnection con = null;
            OracleCommand command = null;
            string query = "";
            OracleDataReader reader = null;

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                foreach (string rut in list)
                {
                    if (!(rut.Equals("")))
                    {

                        query = "SELECT run  FROM usuario_alumno WHERE run = :p_rut";
                        command = new OracleCommand(query, con);
                        command.Parameters.Add(new OracleParameter("p_rut", rut));

                        reader = command.ExecuteReader();

                        //Si no encuentra registros con el rut que se está procesando, ingresa el nuevo rut
                        //quiere decir que no existía previamente
                        if (!reader.HasRows)
                        {
                            command = new OracleCommand("PKG_USUARIO.SP_CARGAR_USUARIO", con);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(":p_run", rut);
                            command.ExecuteNonQuery();
                        }


                        query = "";
                        reader.Close();
                    }
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

        private static bool existUser(string rut)
        {
            OracleConnection con = null;
            OracleCommand command = null;
            OracleDataReader reader = null;
            string query = "";
            bool exist = false;

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                query = "SELECT run  FROM usuario_alumno WHERE run = :p_rut";
                command = new OracleCommand(query, con);
                command.Parameters.Add(new OracleParameter("p_rut", rut));

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        exist = true;
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Error desconocido, por favor contacte al administrador");
            }
            finally
            {
                con.Close();
            }

            return exist;
        }
    }
}
