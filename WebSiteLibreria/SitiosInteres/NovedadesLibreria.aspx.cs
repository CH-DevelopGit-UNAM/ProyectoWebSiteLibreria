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
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

public partial class SitiosInteres_NovedadesLibreria : System.Web.UI.Page
{
    LibreriaController libreriaBd = new LibreriaController();
    ResponsableController respDetalle = new ResponsableController();
    List<TituloLibreriaView> _Titulos = null;
    Paginacion _Paginacion = new Paginacion() { FilasPorPagina = 10000000, PaginaActual = 1 };

    protected void Page_Init()
    {
        if (!IsPostBack)
        {           
            ViewState["paginacion"] = _Paginacion;
            ViewState["busquedaHabilitada"] = false;
            ViewState["isSortDescending"] = false;
            CargarTodos(string.Empty, false);
        }
        else
        {
            _Titulos = ViewState["titulos"] as List<TituloLibreriaView>;            
            var target = Request.Params["__EVENTTARGET"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var campo = IsPostBack;

        //if (!IsPostBack)
        //{
        //    _Paginacion = new Paginacion() { FilasPorPagina = 10000000, PaginaActual = 1 };
        //    ViewState["busquedaHabilitada"] = false;
        //    ViewState["isSortDescending"] = false;
        //    CargarTodos(string.Empty, false);
        //}
        //else
        //{
        //    _Titulos = ViewState["titulos"] as List<TituloLibreriaView>;
        //    _Paginacion = ViewState["paginacion"] as Paginacion;
        //    var target = Request.Params["__EVENTTARGET"];            
        //}
    }

    protected void BtnBuscarNombre_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(BtnBuscarNombre.Text, string.Empty, false);
    }

    protected void BtnBuscarTema_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(BtnBuscarTema.Text, string.Empty, false);
    }

    protected void BtnBuscarAutor_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(BtnBuscarAutor.Text, string.Empty, false);
    }

    protected void BtnBuscarResponsable_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(BtnBuscarResponsable.Text, string.Empty, false);
    }

    protected void BtnBuscarCiudad_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(BtnBuscarCiudad.Text, string.Empty, false);
    }

    protected void BtnTodos_Click(object sender, EventArgs e)
    {
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        CargarTodos(string.Empty, false);
    }

    private void Buscar(string tipoBusqueda, string campoOrdenacion, bool isDescending)
    {
        string busqueda = HiddenCampoBusqueda.Value;
        if (!tipoBusqueda.Equals("Nombre", StringComparison.InvariantCultureIgnoreCase) && !tipoBusqueda.Equals("Tema", StringComparison.InvariantCultureIgnoreCase)
            && !tipoBusqueda.Equals("Responsable", StringComparison.InvariantCultureIgnoreCase) && !tipoBusqueda.Equals("Ciudad", StringComparison.InvariantCultureIgnoreCase)
             && !tipoBusqueda.Equals("Autor", StringComparison.InvariantCultureIgnoreCase))
        {
            busqueda = string.Empty;
        }

        SpanSearch.Attributes.Clear();
        SpanSearch.InnerText =tipoBusqueda;

        if (String.IsNullOrEmpty(busqueda))
        {
            _Titulos = new List<TituloLibreriaView>();
            _Paginacion.FilasTotales = 0;
            _Paginacion.PaginasTotales = 0;
            _Paginacion.PaginaActual = 0;
        }
        else
        {
            if (tipoBusqueda.Equals("Nombre", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPorNombre(busqueda, new bool?(true), campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if (tipoBusqueda.Equals("Tema", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPorTema(busqueda, new bool?(true), campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if (tipoBusqueda.Equals("Responsable", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPor(null, null, null, busqueda, null, null, new bool?(true), campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if (tipoBusqueda.Equals("Ciudad", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPor(null, busqueda, null, null, null, null, new bool?(true), campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if (tipoBusqueda.Equals("Autor", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPor(null, null, null, null, busqueda, null, new bool?(true), campoOrdenacion, isDescending, ref _Paginacion);
            }
            else
            {
                _Titulos = libreriaBd.PaginarTitulosPorNombre(busqueda, new bool?(true), campoOrdenacion, isDescending, ref _Paginacion);
            }
        }

        ViewState["titulos"] = _Titulos;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = true;
        ViewState["tipoBusqueda"] = tipoBusqueda;
        ViewState["isSortDescending"] = isDescending;
        ViewState["currentSort"] = campoOrdenacion;
        MostrarTitulos(_Titulos, _Paginacion);
    }

    private void CargarPorId(int id)
    {
        HiddenCampoBusqueda.Value = "";
        TextBoxBusqueda.Text = "";
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        _Titulos.Clear();
        _Titulos.Add(libreriaBd.PaginarTituloPorId(id, ref _Paginacion));
        ViewState["titulos"] = _Titulos;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        ViewState["tipoBusqueda"] = "";
        ViewState["isSortDescending"] = false;
        ViewState["currentSort"] = "";
        MostrarTitulos(_Titulos, _Paginacion);
    }

    private void MostrarTitulos(List<TituloLibreriaView> lista, Paginacion paginacion)
    {
        List<Noticia> noticias = LoaderNoticias.GetNoticias(lista);
        Carousel.Noticias = noticias;
        Carousel.RenderCarousel(noticias);
        Carousel.PanelNoticias.Update();
    }

    private void CargarTodos(string campoOrdenacion, bool isDescending)
    {
        _Titulos = libreriaBd.PaginarTitulos(new bool?(true), campoOrdenacion, isDescending, ref _Paginacion);
        HiddenCampoBusqueda.Value = "";
        ViewState["titulos"] = _Titulos;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        ViewState["tipoBusqueda"] = "";
        ViewState["isSortDescending"] = isDescending;
        ViewState["currentSort"] = campoOrdenacion;
        SpanSearch.InnerText = string.Empty;
        SpanSearch.Attributes.Clear();
        SpanSearch.Attributes.Add("class", "glyphicon glyphicon-search");
        MostrarTitulos(_Titulos, _Paginacion);
    }
}