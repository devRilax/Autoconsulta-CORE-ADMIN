using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Totem.BLL;
using Totem.Util;
using Totem.VO;

namespace Totem.UI.Paginas
{
    public partial class CambioPassword : System.Web.UI.Page
    {
        private UsuarioAdministrador usuarioAdmin = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            else
            {
                usuarioAdmin = new UsuarioAdministrador();
                usuarioAdmin = (UsuarioAdministrador)(Session["administrador"]);
            }
        }

        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            UsuarioAdministrador usuario = null;
            string claveActual = "";
            string claveConfirm = "";

            try
            {
                usuario = new UsuarioAdministrador();
                usuario.username = usuarioAdmin.username;
                usuario.clave = txtNuevClave.Text;
                claveActual = txtClaveActual.Text;
                claveConfirm = txtNuevaClaveConfirm.Text;
                UsuarioAdministradorBLL.getInstance.changePassword(usuario, claveActual, claveConfirm);

                UtilScript.executeJS("msgRespuesta('Contraseña cambiada con éxito','success');", this.Page, 500);

            }
            catch (Exception ex)
            {
                UtilScript.executeJS("msgRespuesta('" + ex.Message + "','error');", this.Page, 500);
            }
        }
    }
}