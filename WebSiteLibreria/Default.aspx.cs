using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unam.CoHu.Libreria.Controller;
using Unam.CoHu.Libreria.Controller.Catalogos;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Security;
using Unam.CoHu.Libreria.Model.Views;

public partial class _Default : Page
{

    LibreriaController bd = new LibreriaController();
    ResponsableController respDetalle = new ResponsableController();
    List<TituloLibreriaView> _Titulos = null;
    Paginacion _Paginacion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            ViewState["busquedaHabilitada"] = false;
            ViewState["isSortDescending"] = false;
            //CargarTodos(string.Empty, false);
        }
        else
        {
            _Titulos = ViewState["titulos"] as List<TituloLibreriaView>;
            _Paginacion = ViewState["paginacion"] as Paginacion;
            var target = Request.Params["__EVENTTARGET"];
           
        }
        // registrar en cada postback (asincrono o no) el "enter" para el campo paginador
        
    }



    protected void ButtonCatalogo_Click(object sender, EventArgs e)
    {
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        
    }


    [WebMethod]
    [ScriptMethod]
    public static TituloLibreriaView CargaTitulo(int idTitulo)
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

   
}
