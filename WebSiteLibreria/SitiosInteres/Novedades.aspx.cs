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
using Unam.CoHu.Libreria.Model.Views;

public partial class SitiosInteres_Novedades : System.Web.UI.Page
{
    LibreriaController libreriaBd = new LibreriaController();
    ResponsableController respDetalle = new ResponsableController();
    List<TituloLibreriaView> _Titulos = null;
    Paginacion _Paginacion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _Paginacion = new Paginacion() { FilasPorPagina = GridViewResultado.PageSize, PaginaActual = 1 };
            ViewState["busquedaHabilitada"] = false;
            ViewState["isSortDescending"] = false;
            CargarTodos(string.Empty, false);
        }
        else
        {
            _Titulos = ViewState["titulos"] as List<TituloLibreriaView>;
            _Paginacion = ViewState["paginacion"] as Paginacion;
            var target = Request.Params["__EVENTTARGET"];
            if (Request.Params["__EVENTTARGET"] == this.TextBoxPaginaActual.ClientID || Request.Params["__EVENTTARGET"] == this.TextBoxPaginaActual.UniqueID)
            {
                PaginaSelected_Changed(this.TextBoxPaginaActual);
            }
        }
        this.TextBoxPaginaActual.Attributes.Add("onkeyup", "javascript: return autoPostbackPaginador(event);");
    }

    protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridViewResultado.PageIndex = e.NewPageIndex;
        MostrarTitulos(_Titulos, _Paginacion);
    }

    protected void GridViewResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView grid = sender as GridView;
                var row = e.Row;
                string sortedExpression = (ViewState["currentSort"] != null ? ViewState["currentSort"].ToString() : string.Empty);
                int indexColumn = 0;
                switch (sortedExpression)
                {
                    case "tituloOriginal":
                        indexColumn = 3;
                        break;
                    case "titulo":
                        indexColumn = 4;
                        break;
                    case "autor":
                        indexColumn = 5;
                        break;
                    case "año":
                        indexColumn = 9;
                        break;
                    default:
                        indexColumn = 0;
                        break;
                }
                if (indexColumn > 0)
                {
                    bool isdescending = (ViewState["isSortDescending"] != null ? (bool)ViewState["isSortDescending"] : false);
                    string sort = (isdescending ? "desc" : "asc");
                    e.Row.Cells[indexColumn].Attributes.Add("data-column-sorted", sort);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TituloLibreriaView fila = e.Row.DataItem as TituloLibreriaView;
                if (fila != null)
                {
                    Label label = e.Row.FindControl("LabelAutor") as Label;
                    if (label != null)
                    {
                        string nombre = fila.Autor.NombreEspanol;
                        //if (!string.IsNullOrEmpty(fila.Autor.NombreLatin))
                        //{
                        //    nombre = nombre + "(" + fila.Autor.NombreLatin + ")";
                        //}
                        //else if (!string.IsNullOrEmpty(fila.Autor.NombreGriego))
                        //{
                        //    nombre = nombre + "(" + fila.Autor.NombreGriego + ")";
                        //}
                        label.Text = nombre;
                    }
                    Image imgPdf = e.Row.FindControl("ImagenPdf") as Image;
                    Image imgVirtual = e.Row.FindControl("ImageVirtual") as Image;
                    Image imgOnline = e.Row.FindControl("ImageOnline") as Image;
                    if (imgPdf != null)
                    {
                        imgPdf.Visible = !string.IsNullOrEmpty(fila.UrlPdf);
                        imgPdf.Attributes.Add("data-url", fila.UrlPdf);
                    }
                    if (imgVirtual != null)
                    {
                        imgVirtual.Visible = !string.IsNullOrEmpty(fila.UrlVirtual);
                        imgVirtual.Attributes.Add("data-url", fila.UrlVirtual);
                    }
                    if (imgOnline != null)
                    {
                        imgOnline.Visible = !string.IsNullOrEmpty(fila.UrlOnline);
                        imgOnline.Attributes.Add("data-url", fila.UrlOnline);
                    }

                    Label labelTrad = e.Row.FindControl("LabelTraductor") as Label;
                    if (labelTrad != null)
                    {
                        if (fila.DetalleResponsables != null && fila.DetalleResponsables.Count > 0)
                        {
                            if (fila.DetalleResponsables[0] != null && fila.DetalleResponsables[0].Responsables != null)
                            {
                                if (fila.DetalleResponsables[0].Responsables.Count > 0)
                                {
                                    var traductores = (from q in fila.DetalleResponsables[0].Responsables
                                                       where q.TipoFuncion.ToLower().Contains("traductor") || q.TipoFuncion.ToLower().Contains("traducción")
                                                       select q).ToList();
                                    if (traductores != null && traductores.Count > 0)
                                    {
                                        if (traductores.Count > 1)
                                        {
                                            labelTrad.Text = traductores[0].NombreCompletoResponsable + " ...";
                                        }
                                        else
                                        {
                                            labelTrad.Text = traductores[0].NombreCompletoResponsable;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    protected void LinkBuscarTema_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(LinkBuscarTema.Text, string.Empty, false);
    }

    protected void LinkBuscarResponsable_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(LinkBuscarResponsable.Text, string.Empty, false);
    }

    protected void LinkBuscarNombre_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(LinkBuscarNombre.Text, string.Empty, false);
    }

    protected void LinkBuscarAutor_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(LinkBuscarAutor.Text, string.Empty, false);

    }

    protected void LinkBuscarCiudad_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(LinkBuscarCiudad.Text, string.Empty, false);
    }

    protected void LinkTodos_Click(object sender, EventArgs e)
    {
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        CargarTodos(string.Empty, false);
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
        MostrarTitulos(_Titulos, _Paginacion);
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
        this.GridViewResultado.DataSource = lista;
        this.GridViewResultado.DataBind();
        int tamano = 0;
        if (lista != null && lista.Count > 0)
        {
            tamano = ((paginacion.PaginaActual - 1) * paginacion.FilasPorPagina) + lista.Count;
            this.LabelRegistroInicial.Text = (tamano - (lista.Count - 1)).ToString();
            this.LabelRegistroFinal.Text = (tamano).ToString();
            FooterGridResultado.Visible = true;
        }
        else
        {
            this.LabelRegistroInicial.Text = "0";
            this.LabelRegistroFinal.Text = "0";
            FooterGridResultado.Visible = false;
        }

        this.LabelPaginaFinal.Text = paginacion.PaginasTotales.ToString();
        this.TextBoxPaginaActual.Text = paginacion.PaginaActual.ToString();
        DeshabilitarNavegacionInicial(paginacion);
    }

    protected void CommandPage_Click(object sender, EventArgs e)
    {
        LinkButton button = sender as LinkButton;
        _Paginacion = (ViewState["paginacion"] != null) ? (Paginacion)ViewState["paginacion"] : new Paginacion() { PaginaActual = 1, FilasPorPagina = GridViewResultado.PageSize };
        _Paginacion.IsNavigating = true;
        if (button != null)
        {

            if (button.CommandName == "Page")
            {
                if (button.CommandArgument == "First")
                {
                    _Paginacion.PaginaActual = 1;
                }
                else if (button.CommandArgument == "Prev")
                {
                    if (_Paginacion.PaginaActual > 1)
                    {
                        _Paginacion.PaginaActual--;
                    }
                    else
                    {
                        _Paginacion.PaginaActual = 1;
                    }
                }
                else if (button.CommandArgument == "Next")
                {
                    if (_Paginacion.PaginaActual < _Paginacion.PaginasTotales)
                    {
                        _Paginacion.PaginaActual++;
                    }
                    else
                    {
                        _Paginacion.PaginaActual = _Paginacion.PaginasTotales;
                    }
                }
                else if (button.CommandArgument == "Last")
                {
                    _Paginacion.PaginaActual = _Paginacion.PaginasTotales;
                }
            }
        }
        else
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string numero = textBox.Text;
                int numeroPagina = 0;
                Int32.TryParse(numero, out numeroPagina);

                if (numeroPagina <= 0)
                {
                    numeroPagina = 1;
                }
                else if (numeroPagina > _Paginacion.PaginasTotales)
                {
                    numeroPagina = _Paginacion.PaginasTotales;
                }
                _Paginacion.PaginaActual = numeroPagina;

            }
        }
        if (_Paginacion.PaginaActual == 1)
        {
            cmdAnterior.Enabled = false;
            cmdInicio.Enabled = false;
            cmdFinal.Enabled = true;
            cmdSiguiente.Enabled = true;
        }
        else if (_Paginacion.PaginaActual > 1 && _Paginacion.PaginaActual < _Paginacion.PaginasTotales)
        {
            cmdAnterior.Enabled = true;
            cmdInicio.Enabled = true;
            cmdFinal.Enabled = true;
            cmdSiguiente.Enabled = true;
        }
        else if (_Paginacion.PaginaActual == _Paginacion.PaginasTotales)
        {
            cmdAnterior.Enabled = true;
            cmdInicio.Enabled = true;
            cmdFinal.Enabled = false;
            cmdSiguiente.Enabled = false;
        }
        else
        {
            cmdAnterior.Enabled = false;
            cmdInicio.Enabled = false;
            cmdFinal.Enabled = false;
            cmdSiguiente.Enabled = false;
        }

        ViewState["paginacion"] = _Paginacion;
        bool isDescending = (ViewState["isSortDescending"] == null ? false : (bool)ViewState["isSortDescending"]);
        string columnOrder = (ViewState["currentSort"] == null ? string.Empty : ViewState["currentSort"].ToString());

        if (((bool)ViewState["busquedaHabilitada"]) == true)
        {
            Buscar(ViewState["tipoBusqueda"] == null ? "" : ViewState["tipoBusqueda"].ToString(), columnOrder, isDescending);
        }
        else
        {
            CargarTodos(columnOrder, isDescending);
        }
    }

    private void DeshabilitarNavegacionInicial(Paginacion paginacion)
    {
        if (paginacion.PaginasTotales <= 0 && paginacion.IsNavigating == false)
        {
            cmdAnterior.Enabled = false;
            cmdInicio.Enabled = false;
            cmdSiguiente.Enabled = false;
            cmdFinal.Enabled = false;
            TextBoxPaginaActual.Enabled = false;
        }
        else if (paginacion.PaginasTotales == paginacion.PaginaActual && paginacion.IsNavigating == false)
        {
            cmdAnterior.Enabled = false;
            cmdInicio.Enabled = false;
            cmdSiguiente.Enabled = false;
            cmdFinal.Enabled = false;
            TextBoxPaginaActual.Enabled = false;
        }
        else if (paginacion.PaginasTotales > 0 && paginacion.IsNavigating == false)
        {
            cmdAnterior.Enabled = false;
            cmdInicio.Enabled = false;
            cmdSiguiente.Enabled = true;
            cmdFinal.Enabled = true;
            TextBoxPaginaActual.Enabled = true;
        }

    }

    protected void PaginaSelected_Changed(object sender)
    {
        CommandPage_Click(sender, null);
    }

    protected void GridViewResultado_Sorting(object sender, GridViewSortEventArgs e)
    {
        bool busqueda = (ViewState["busquedaHabilitada"] != null) ? (bool)ViewState["busquedaHabilitada"] : false;
        string campoOrden = e.SortExpression;
        bool isDescending = (ViewState["isSortDescending"] != null) ? (bool)ViewState["isSortDescending"] : false;
        string currentSort = (ViewState["currentSort"] != null ? ViewState["currentSort"].ToString() : string.Empty);
        if (currentSort != e.SortExpression)
        {
            isDescending = false;
        }
        else
        {
            if (isDescending)
            {
                isDescending = false;
            }
            else
            {
                isDescending = true;
            }
        }
       
        if (busqueda)
        {
            Buscar(ViewState["tipoBusqueda"].ToString(),campoOrden, isDescending);
        }
        else
        {
            CargarTodos(campoOrden, isDescending);
        }
    }
}