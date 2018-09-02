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
    public class UsuarioAlumnoDAL
    {
        public static bool validateUser(UsuarioAlumno usuario)
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

                query = "SELECT run, clave, estado_login FROM usuario_alumno WHERE run = :p_username AND clave = :p_clave";
                command = new OracleCommand(query, con);
                command.Parameters.Add(new OracleParameter("p_username", usuario.run));
                command.Parameters.Add(new OracleParameter("p_clave", usuario.clave));

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        usuario.estadoLogin = UtilNumero.parseInt(reader["estado_login"].ToString());
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

        public static bool changePasswordUser(UsuarioAlumno usuario)
        {
            OracleConnection con = null;
            OracleCommand command = null;
            string query = "";
            bool itChanged = false;

            try
            {
                con = new OracleConnection(Constantes.DATA_SOURCE);
                con.Open();

                query = "UPDATE usuario_alumno SET clave = :p_nuevaClave, estado_login = 1 WHERE run = :p_run";
                command = new OracleCommand(query, con);
                command.Parameters.Add(new OracleParameter("p_nuevaClave", usuario.clave));
                command.Parameters.Add(new OracleParameter("p_run", usuario.run));
                command.ExecuteNonQuery();
                itChanged = true;

            }
            catch (Exception)
            {
                throw new Exception("Error desconocido, contacte al administrador");
            }
            finally
            {
                con.Close();
            }

            return itChanged;
        }

    }
}
