using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Controller.Web;

public partial class ResponsiveSite : System.Web.UI.MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine("----------------------------------> MasterPage -> Page_Init ");

        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;

        ShowProtectedMenus();
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Resignar el ViewState cuando no exista la sesión
            if (ConfiguracionSitio.UsuarioSistema == null)
            {
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }

            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                var user = ViewState[AntiXsrfUserNameKey];
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine("----------------------------------> MasterPage -> Page_Load ");
    }

    private void ShowProtectedMenus()
    {
        if (AutenticacionSitio.IsUsuarioAutenticado()) //(ConfiguracionSitio.UsuarioSistema != null)
        {
            MenuCatalogos.Visible = true;
            MenuTitulos.Visible = true;
            MenuReportes.Visible = true;
            SelectMenuNavigation(Request.Url.AbsolutePath.Trim());
        }
        else
        {
            MenuCatalogos.Visible = false;
            MenuTitulos.Visible = false;
            MenuReportes.Visible = false;
            // Eliminar la cookie de autenticacion
            //FormsAuthentication.SignOut();
            AutenticacionSitio.TerminarSesionPrincipal(false);
            SelectMenuNavigation(Request.Url.AbsolutePath.Trim());
        }

        if (Request.Url.AbsolutePath.Trim().Contains("SitiosInteres/Catalogo")) {
            this.TextBoxBusqueda.Visible = false;
            this.ButtonBuscar.Visible = false;
        }
    }

    protected void LinkButton_Click(object sender, EventArgs e)
    {
        AutenticacionSitio.TerminarSesionPrincipal(false);
        ViewState[AntiXsrfUserNameKey] = String.Empty;
        Response.Redirect("~/");
    }

    protected void ButtonBuscar_Click(object sender, EventArgs e)
    {
        string query = this.TextBoxBusqueda.Text;
        query = !string.IsNullOrEmpty(query) ? query.Trim(): string.Empty;
        string url = ResolveUrl("~/SitiosInteres/Catalogo");
        string httpRedirect = string.Format("{0}?q={1}", url, Server.UrlEncode(query)) ;
        ConfiguracionSitio.IsSearchRoot = true;
        Response.Redirect(httpRedirect);

    }

    private void SelectMenuNavigation(string urlRequest)
    {
        string css = string.Empty;
        if (!string.IsNullOrEmpty(urlRequest)) {
            urlRequest = urlRequest.ToLower().Trim();
            if (urlRequest.Contains("default"))
            {
                css = this.OpcionInicio.Attributes["class"];
                this.OpcionInicio.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("general/"))
            {
                if (urlRequest.Contains("general/detalletitulo"))
                {
                    css = this.OpcionInicio.Attributes["class"];
                    this.OpcionInicio.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
                }
                else {
                    css = this.OpcionBibliotheca.Attributes["class"];
                    this.OpcionBibliotheca.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
                }
            }
            else if (urlRequest.Contains("directorio/")) {
                css = this.OpcionInicio.Attributes["class"];
                this.OpcionInicio.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("general/creditos")) {
                css = this.OpcionInicio.Attributes["class"];
                this.OpcionInicio.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("sitiosinteres/catalogo"))
            {
                css = this.OpcionCatalogo.Attributes["class"];
                this.OpcionCatalogo.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("sitiosinteres/contacto"))
            {
                css = this.OpcionContacto.Attributes["class"];
                this.OpcionContacto.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("sitiosinteres/novedadeslibreria"))
            {
                css = this.OpcionNovedades.Attributes["class"];
                this.OpcionNovedades.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("acceso/catalogos/"))
            {
                css = this.OpcionCatalogos.Attributes["class"];
                this.OpcionCatalogos.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("acceso/titulos/"))
            {
                css = this.OpcionEditarTitulos.Attributes["class"];
                this.OpcionEditarTitulos.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("acceso/reportes/"))
            {
                css = this.OpcionReportes.Attributes["class"];
                this.OpcionReportes.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
            }
            else if (urlRequest.Contains("account/login"))
            {
                //opcion
                var control = LoginViewUsuario.FindControl("OpcionRegresar");
                if (control != null)
                {
                    HtmlGenericControl controlI = control as HtmlGenericControl;
                    if (controlI != null)
                    {
                        css = controlI.Attributes["class"];
                        controlI.Attributes["class"] = string.Format("{0} menuSitioSelected", css);
                    }
                }
            }

        }        
    }
}
