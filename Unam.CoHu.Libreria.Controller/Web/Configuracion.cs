using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Security;

namespace Unam.CoHu.Libreria.Controller.Web
{
    public class ConfiguracionSitio
    {
        public static string ObtenerVariableConfig(string key)
        {
            string value = "";
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                value = ConfigurationManager.AppSettings[key];
            }

            return value;
        }

        private static List<Noticia> _Noticias = null;

        public static List<Noticia> Noticias {
            get{

                if (HttpContext.Current.Session["Noticias"] != null)
                {
                    _Noticias = HttpContext.Current.Session["Noticias"] as List<Noticia>;
                }
                else {
                    _Noticias = new List<Noticia>();
                }
                if (_Noticias == null)
                    _Noticias = new List<Noticia>();
                return _Noticias;
            }
            set {
                if (value != null)
                {
                    HttpContext.Current.Session["Noticias"] = null;
                    HttpContext.Current.Session["Noticias"] = value;
                }
            }
        }

        public static Usuario UsuarioSistema
        {
            get
            {
                Usuario usuario = null;
                if (HttpContext.Current.Session["Usuario"] != null)
                {
                    usuario = (Usuario)HttpContext.Current.Session["Usuario"];
                }
                return usuario;
            }
            set
            {
                HttpContext.Current.Session["Usuario"] = value;
            }
        }

        public static bool IsSearchRoot
        {
            get
            {
                bool isSearchRoot = false;
                if (HttpContext.Current.Session["IsSearchRoot"] != null)
                {
                    isSearchRoot = (bool) HttpContext.Current.Session["IsSearchRoot"];
                }
                return isSearchRoot;
            }
            set
            {
                HttpContext.Current.Session["IsSearchRoot"] = value;
            }
        }
    }
}
