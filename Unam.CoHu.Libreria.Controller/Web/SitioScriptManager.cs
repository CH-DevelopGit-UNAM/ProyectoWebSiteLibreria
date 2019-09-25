using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Unam.CoHu.Libreria.Controller.Web
{

    public static class SitioScriptManager
    {
        public static void RegistrarScript(string functionJs)
        {
            int numrandom = 0;
            Random random = new Random();
            numrandom = random.Next(0, 1000);

            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page p = (Page)HttpContext.Current.CurrentHandler;

                if (ScriptManager.GetCurrent(p) != null)
                {
                    ScriptManager.RegisterStartupScript(p, typeof(Page), "id" + numrandom.ToString(), functionJs, true);
                }
                else
                {
                    p.ClientScript.RegisterStartupScript(typeof(Page), "id" + numrandom.ToString(), functionJs, true);
                }
            }

        }
    }
}
