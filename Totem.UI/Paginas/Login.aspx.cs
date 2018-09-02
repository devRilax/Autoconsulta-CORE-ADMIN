using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Totem.BLL;
using Totem.Util;
using Totem.VO;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Totem.UI.Paginas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                Session.Abandon();
            }
        }

        protected void btnLogeo_Click(object sender, EventArgs e)
        {
            UsuarioAdministrador usuario = null;

            try
            {
                usuario = new UsuarioAdministrador();
                usuario.username = txtUsermane.Text;
                usuario.clave = txtPassword.Text;

                if (UsuarioAdministradorBLL.getInstance.validarUsuario(usuario))
                {
                    Session["administrador"] = usuario;
                    Response.Redirect("../Paginas/PanelAdministrador.aspx");
                }
                else
                {
                    UtilScript.executeJS("msgLoginError('Credenciales inválidas');", this.Page, 500);
                }
            }
            catch (Exception ex)
            {
                UtilScript.executeJS("msgLoginError('" + ex.Message + "');", this.Page, 500); ;
            }
        }
    }
}