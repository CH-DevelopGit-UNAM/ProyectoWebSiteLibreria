using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Controller.Web;

namespace Unam.CoHu.Libreria.Controller.Security
{
    public class SecurePage : System.Web.UI.Page
    {
        protected AutenticacionSitio _Autenticacion =new AutenticacionSitio();

        protected int SessionLengthMinutes
        {
            get { return Session.Timeout; }
        }

        protected string SessionExpireDestinationUrl
        {
            get { return  ConfiguracionSitio.ObtenerVariableConfig("PathSessionEnd"); }
        }

        protected string PageRestrictDestinationUrl
        {
            get { return "~/Acceso"; }
        }

        public SecurePage(): base() {
            
        }

        protected override void OnInit(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("----------------------------------> SecurePage -> OnInit ");
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("----------------------------------> SecurePage -> OnLoad ");
            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page page = HttpContext.Current.CurrentHandler as Page;
                if (!AutenticacionSitio.IsUsuarioAutenticado()) //(ConfiguracionSitio.UsuarioSistema == null || Request.IsAuthenticated == false )
                {
                    Response.Redirect(SessionExpireDestinationUrl + "?op=logout");
                }
                // Be sure to call the base class's OnLoad method!
                base.OnLoad(e);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            var url = SessionExpireDestinationUrl;
            var urlPing = this.ResolveUrl("~/Acceso/PingServer.ashx");            
            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page page = HttpContext.Current.CurrentHandler as Page;
                if (page != null)
                {
                    // registrar script despues de la declaración de jquery: ejecutar el intervalo 30 seg. antes de que termine la sesión
                    ClientScript.RegisterStartupScript(
                            page.GetType(), 
                            "pingServer", 
                            "<script type='text/javascript'> var interval = " + (( 1000 * 60 * this.SessionLengthMinutes) - 30000).ToString() + " ;  $(document).ready( function(){ var intervalKey = window.setInterval( function() { try{ $.get('" + urlPing + "?v="+ DateTime.Now.ToString("yyyymmdd") + ";" + "');} catch (err){ }}, interval); }); </script>");
                }
            }
            // Doesn't works: agrega el script antes de que se declare jquery
            //this.Header.Controls.Add(new LiteralControl("<script type='text/javascript'> var interval = "+ ((this.SessionLengthMinutes * 60) - 20).ToString() + " ;  $(document).ready( function(){var intervalKey = window.setInterval( function() {$.get('"+ url + "?v=Date.now();"  + "');}, interval); }); </script>"));

            // Deshabilito el anexado del tag meta, para que ya no recarge hacia una url 
            //if (HttpContext.Current.CurrentHandler is Page)
            //{
            //    Page page = HttpContext.Current.CurrentHandler as Page;
            //    if (page != null)
            //    {                    
            //        this.Header.Controls.Add(new LiteralControl( String.Format("<meta http-equiv='refresh' content='{0};url={1}'>", (this.SessionLengthMinutes * 60) - 20,page.ResolveUrl(page.AppRelativeVirtualPath))));
            //    }
            //    else {
            //        this.Header.Controls.Add(new LiteralControl( String.Format("<meta http-equiv='refresh' content='{0};url={1}'>", (this.SessionLengthMinutes * 60) - 20, url)));
            //    }
            //}
        }
    }
}
