using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Unam.CoHu.Libreria.Controller;
using Unam.CoHu.Libreria.Controller.Catalogos;
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;
using Unam.CoHu.Libreria.Model.WebServices;

/// <summary>
/// Summary description for WebServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebServices : System.Web.Services.WebService
{

    public WebServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    [ScriptMethod]
    public List<ResponsableTituloDetail> Metodo(int idTitulo)
    {
        List<ResponsableTituloDetail> lista = new List<ResponsableTituloDetail>();
        LibreriaController controller = new LibreriaController();
        if (AutenticacionSitio.IsUsuarioAutenticado())
        {
            lista = controller.CargarResponsablesTitulo(idTitulo);
        }
        else
        {
            lista = new List<ResponsableTituloDetail>();
        }


        return lista;
    }


    [WebMethod]
    [ScriptMethod]
    public  TituloLibreriaView GetTitulo(int idTitulo)
    {
        TituloLibreriaView titulo = null;
        try
        {
            LibreriaController controller = new LibreriaController();
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                titulo = controller.CargarPorId(idTitulo);
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
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
    public List<Autor> BuscarAutores(string busqueda)
    {
        List<Autor> autores = null;
        try
        {
            AutoresController controller = new AutoresController();
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                autores = controller.BuscarAutorPor(busqueda, new int?(30));
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            if (autores == null || autores.Count <= 0)
            {
                autores = new List<Autor>() { new Autor() { IdAutor = "", NombreLatin = "NO SE ENCONTRO EL AUTOR", NombreEspanol = "NO SE ENCONTRO EL AUTOR", NombreGriego = "NO SE ENCONTRO EL AUTOR" } };
            }
        }
        catch (Exception ex)
        {
            autores = new List<Autor>() { new Autor() { IdAutor = "", NombreLatin = "NO SE ENCONTRO EL AUTOR", NombreEspanol = "NO SE ENCONTRO EL AUTOR", NombreGriego = "NO SE ENCONTRO EL AUTOR" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        return autores;
    }

    [WebMethod]
    [ScriptMethod]
    public List<Ciudad> BuscarCiudades(string busqueda)
    {
        List<Ciudad> ciudades = null;
        try
        {
            CiudadesController controller = new CiudadesController();
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                ciudades = controller.BuscarCiudadPor(busqueda, new int?(30));
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            if (ciudades == null || ciudades.Count <= 0)
            {
                ciudades = new List<Ciudad>() { new Ciudad() { IdCiudad = "", Descripcion = "NO SE ENCONTRO LA CIUDAD" } };
            }
        }
        catch (Exception ex)
        {
            ciudades = new List<Ciudad>() { new Ciudad() { IdCiudad = "", Descripcion = "NO SE ENCONTRO LA CIUDAD" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        return ciudades;
    }

    [WebMethod]
    [ScriptMethod]
    public List<Editor> BuscarEditores(string busqueda)
    {
        List<Editor> editores = null;
        try
        {
            EditoresController controller = new EditoresController();
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                editores = controller.BuscarPor(busqueda, new int?(30));
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            if (editores == null || editores.Count <= 0)
            {
                editores = new List<Editor>() { new Editor() { IdEditor = "", Nombre = "NO SE ENCONTRO EL EDITOR" } };
            }
        }
        catch (Exception ex)
        {
            editores = new List<Editor>() { new Editor() { IdEditor = "", Nombre = "NO SE ENCONTRO EL EDITOR" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        return editores;
    }

    //[WebMethod]
    //[ScriptMethod]
    //public List<Isbn> BuscarIsbn(string busqueda)
    //{
    //    List<Isbn> isbns = null;
    //    IsbnController controller = new IsbnController();
    //    try
    //    {
    //        if (AutenticacionSitio.IsUsuarioAutenticado())
    //        {
    //            isbns = controller.BuscarPor(busqueda, null, new int?(30));
    //        }
    //        else
    //        {
    //            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    //        }
    //        if (isbns == null || isbns.Count <= 0)
    //        {
    //            isbns = new List<Isbn>() { new Isbn() { IdIsbn = 0, ClaveIsbn = "NO SE ENCONTRO EL ISBN" } };
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        isbns = new List<Isbn>() { new Isbn() { IdIsbn = 0, ClaveIsbn = "NO SE ENCONTRO EL ISBN" } };
    //        HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //    }

    //    return isbns;
    //}

    [WebMethod]
    [ScriptMethod]
    public List<Serie> BuscarSerie(string busqueda)
    {
        List<Serie> isbns = null;
        SeriesLibroController controller = new SeriesLibroController();
        try
        {
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                isbns = controller.BuscarPor(busqueda, new int?(30));
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            if (isbns == null || isbns.Count <= 0)
            {
                isbns = new List<Serie>() { new Serie() { IdSerie = "", NombreLatin = "NO SE ENCONTRO EL ISBN", NombreGriego = "NO SE ENCONTRO EL ISBN" } };
            }
        }
        catch (Exception ex)
        {
            isbns = new List<Serie>() { new Serie() { IdSerie = "", NombreLatin = "NO SE ENCONTRO EL ISBN", NombreGriego = "NO SE ENCONTRO EL ISBN" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return isbns;
    }

    [WebMethod]
    [ScriptMethod]
    public List<Responsable> BuscarResponsables(string busqueda)
    {
        List<Responsable> responsables = null;
        ResponsableController controller = new ResponsableController();
        try
        {
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                responsables = controller.BusquedaResponsables(busqueda, new int?(30));
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            if (responsables == null || responsables.Count <= 0)
            {
                responsables = new List<Responsable>() { new Responsable() { IdResponsable = "", Nombre = "NO SE ENCONTRO EL RESPONSABLE" } };
            }
        }
        catch (Exception ex)
        {
            responsables = new List<Responsable>() { new Responsable() { IdResponsable = "", Nombre = "NO SE ENCONTRO EL RESPONSABLE" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        return responsables;
    }

    [WebMethod]
    [ScriptMethod]
    public List<Funcion> CargarFunciones()
    {
        List<Funcion> funciones = null;
        List<Funcion> retorno = null;

        FuncionesController controller = new FuncionesController();
        try
        {
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                funciones = controller.CargarTodos();
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            if (funciones == null || funciones.Count <= 0)
            {
                retorno = new List<Funcion>() { new Funcion() { IdFuncion = "-1", TipoFuncion = "NO SE ENCONTRO LA FUNCION" } };
            }
            else
            {
                retorno = new List<Funcion>();
                retorno.Add(new Funcion() { IdFuncion = "", TipoFuncion = "Seleccione una opción..." });
                retorno.AddRange(funciones);
            }
        }
        catch (Exception ex)
        {
            retorno = new List<Funcion>() { new Funcion() { IdFuncion = "-1", TipoFuncion = "NO SE ENCONTRO LA FUNCION" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return retorno;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public WebResult LoadFile()
    {
        WebResult result = new WebResult();
        try
        {
            var file2 = HttpContext.Current.Request.Files;

            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                result.IsProcesado = true;
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                result.IsProcesado = false;
            }

        }
        catch (Exception ex)
        {
            result.IsProcesado = false;
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return result;
    }

}
