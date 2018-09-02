using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Totem.VO;

namespace Totem.UI.Paginas
{
    public partial class MenuPanel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["administrador"] == null)
                {
                    Response.Redirect("../Paginas/Login.aspx");
                }

            }
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../Paginas/Login.aspx");
        }

        protected void linkCambiarClave_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Paginas/CambioPassword.aspx");
        }

        protected void linkPanel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Paginas/PanelAdministrador.aspx");
        }
    }
}