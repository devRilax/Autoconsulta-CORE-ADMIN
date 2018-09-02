using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Totem.Util
{
    public class UtilScript
    {
        public static void executeJS(string strComando, Page web, int tiempo)
        {
            ScriptManager.RegisterStartupScript(web, web.GetType(), "msg", strComando, true);
        }
    }
}
