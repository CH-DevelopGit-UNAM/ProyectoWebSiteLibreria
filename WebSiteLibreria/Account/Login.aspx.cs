using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Unam.CoHu.Libreria.Controller;
using Unam.CoHu.Libreria.Controller.Enums;
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Controller.Web;
using WebSiteLibreria;

public partial class Account_Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            RegisterHyperLink.NavigateUrl = "Register";            
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        if (!IsPostBack)
        {
            if (Request.QueryString.GetValues("op") != null)
            {
                Utilidades.MostrarMensaje("Debe iniciar sesión.", TipoMensaje.Error, null);
            }
        }
        else
        {            
        }
    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(TextBoxUserName.Text) || string.IsNullOrEmpty(TextBoxPassword.Text)) {
                Utilidades.MostrarMensaje("Escriba un usuario y/o contraseña.", TipoMensaje.Warning, null);
                return;
            }

            AutenticacionSitio autenticacion = new AutenticacionSitio(TextBoxUserName.Text, TextBoxPassword.Text);
            if (autenticacion.AutorizarAcceso())
            {
                ConfiguracionSitio.UsuarioSistema = autenticacion.UsuarioSistema;
                FormsAuthentication.SetAuthCookie(ConfiguracionSitio.UsuarioSistema.Nombre, true);
                FormsAuthentication.RedirectFromLoginPage(ConfiguracionSitio.UsuarioSistema.Nombre, true);
            }
            else
            {
                Utilidades.MostrarMensaje(autenticacion.MensajeValidacion, TipoMensaje.Warning, null);
            }
        }
        catch (Exception ex)
        {
            AutenticacionSitio.TerminarSesionPrincipal(false);       
            Utilidades.MostrarMensaje("Ocurrió un error al ingresar, intente de nuevo.", TipoMensaje.Error, null);
        }        
    }
}