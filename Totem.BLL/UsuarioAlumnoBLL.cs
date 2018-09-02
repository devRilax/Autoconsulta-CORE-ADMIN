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
    public class UsuarioAlumnoBLL
    {
        private static UsuarioAlumnoBLL instance;
        public static UsuarioAlumnoBLL getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UsuarioAlumnoBLL();
                }
                return instance;
            }
            set { instance = value; }
        }

        private UsuarioAlumnoBLL()
        {
        }

        public bool validarUsuario(UsuarioAlumno usuario)
        {
            if (usuario.run.Equals(""))
            {
                throw new ValidacionException("Campo rut es requerido");
            }

            if (usuario.clave.Equals(""))
            {
                throw new ValidacionException("Campo constraseña es requerido");
            }

            return UsuarioAlumnoDAL.validateUser(usuario);
        }

        public bool cambiarClaveAlumno(UsuarioAlumno usuario)
        {
            return UsuarioAlumnoDAL.changePasswordUser(usuario);
        }
    }
}
