using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Unam.CoHu.Libreria.Controller;
using Unam.CoHu.Libreria.Model.Views;

/// <summary>
/// Summary description for WebSearch
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebSiteServices : System.Web.Services.WebService
{

    public WebSiteServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod]
    public TituloLibreriaView CargaTitulo(int idTitulo)
    {
        TituloLibreriaView titulo = null;
        try
        {
            LibreriaController controller = new LibreriaController();
            titulo = controller.CargarPorId(idTitulo);
        }
        catch (Exception ex)
        {
            titulo = null;
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        return titulo;
    }

    [WebMethod]
    [ScriptMethod]
    public object PaginarNovedades(PaginacionAjax paginacion)
    {
        List<TituloLibreriaView> titulos = null;
        Paginacion p = null;
        try
        {
            LibreriaController controller = new LibreriaController();
            p = (Paginacion)paginacion;
            if (paginacion.IsBusqueda)
            {
                if (string.IsNullOrEmpty(paginacion.Busqueda1))
                {
                    titulos = null;
                    p.PaginasTotales = 0;
                    p.FilasTotales = 0;
                }
                else
                {

                    if (paginacion.TipoBusqueda.Equals("Nombre", StringComparison.InvariantCultureIgnoreCase))
                    {
                        titulos = controller.PaginarTitulosPorNombre(paginacion.Busqueda1, new bool?(true), "titulo", false, ref p);
                    }
                    else if (paginacion.TipoBusqueda.Equals("Tema", StringComparison.InvariantCultureIgnoreCase))
                    {
                        titulos = controller.PaginarTitulosPorTema(paginacion.Busqueda1, new bool?(true), "tema", false, ref p);
                    }
                    else if (paginacion.TipoBusqueda.Equals("Responsable", StringComparison.InvariantCultureIgnoreCase))
                    {
                        titulos = controller.PaginarTitulosPor(null, null, null, paginacion.Busqueda1, null, null, new bool?(true), "titulo", false, ref p);
                    }
                    else if (paginacion.TipoBusqueda.Equals("Ciudad", StringComparison.InvariantCultureIgnoreCase))
                    {
                        titulos = controller.PaginarTitulosPor(null, paginacion.Busqueda1, null, null, null, null, new bool?(true), "ciudad", false, ref p);
                    }
                    else if (paginacion.TipoBusqueda.Equals("Autor", StringComparison.InvariantCultureIgnoreCase))
                    {
                        titulos = controller.PaginarTitulosPor(null, null, null, null, paginacion.Busqueda1, null, new bool?(true), "autor", false, ref p);
                    }
                    else
                    {
                        titulos = controller.PaginarTitulosPorNombre(paginacion.Busqueda1, new bool?(true), "titulo", false, ref p);
                    }
                }
            }
            else
            {
                titulos = controller.PaginarTitulos(new bool?(true), "titulo", false, ref p);
            }
        }
        catch (Exception ex)
        {
            titulos = null;
            p.PaginasTotales = 0;
            p.FilasTotales = 0;
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return new { Titulos = titulos, Paginacion = p };
    }

}
