using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totem.DAL;
using Totem.Util;
using Totem.VO;

namespace Totem.BLL
{
    /// <summary>
    /// Desarrollado por @Danilo Caro Aparicio, 
    /// Año: 2017
    /// Email: danilocaro21@gmail.com
    /// </summary>
    /// 
    public class UsuarioAdministradorBLL
    {
        private static UsuarioAdministradorBLL instance;
        public static UsuarioAdministradorBLL getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UsuarioAdministradorBLL();
                }
                return instance;
            }
            set { instance = value; }
        }

        private UsuarioAdministradorBLL()
        {
        }

        public bool validarUsuario(UsuarioAdministrador usuario)
        {
            if (usuario.username.Equals(""))
            {
                throw new ValidacionException("Campo rut es requerido");
            }

            if (usuario.clave.Equals(""))
            {
                throw new ValidacionException("Campo contraseña es requerido");
            }

            return UsuarioAdministradorDAL.validateUser(usuario);
        }

        public void changePassword(UsuarioAdministrador usuario, string claveActual, string claveConfirm)
        {
            validateChangePassword(usuario, claveActual, claveConfirm);
            if (UsuarioAdministradorDAL.validatePasswordUser(usuario, claveActual))
            {
                UsuarioAdministradorDAL.changeUserPassword(usuario);

            }
            else
            {
                throw new Exception("La clave actual es inválida");
            }
        }

        private void validateChangePassword(UsuarioAdministrador usuario, string claveActual, string claveConfirm)
        {
            if (usuario.clave == "")
            {
                throw new ValidacionException("Campo contraseña es requerido");
            }

            if (claveActual == "")
            {
                throw new Exception("Debe ingresar su clave actual");
            }

            if (usuario.clave != claveConfirm)
            {
                throw new ValidacionException("La clave de confirmación no coinciden");
            }
        }
    }
}
