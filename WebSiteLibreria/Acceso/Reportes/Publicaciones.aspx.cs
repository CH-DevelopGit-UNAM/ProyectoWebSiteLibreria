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
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;
using Unam.CoHu.Libreria.Controller.Enums;

public partial class Acceso_Reportes_Publicaciones : SecurePage
{
    ReportesController libreriaBd = new ReportesController();

    List<TituloLibreriaView> _Titulos = null;
    Paginacion _Paginacion = null;
    protected TipoReportePublicacion _TipoReporte = TipoReportePublicacion.Autor;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _TipoReporte = (this.Request["rpt"] != null ?  ReportesController.GetTipoReporte(this.Request["rpt"].ToString().ToLower()) : TipoReportePublicacion.Autor);
            HiddenTipoReporte.Value = ((int)_TipoReporte).ToString();
            ViewState["rpt"] = _TipoReporte;
            _Paginacion = new Paginacion() { FilasPorPagina = GridViewResultado.PageSize, PaginaActual = 1, IsNavigating = false };
            ViewState["busquedaHabilitada"] = false;
            ViewState["isSortDescending"] = false;
            ViewState["paginacion"] = _Paginacion;
            //CargarTodos(string.Empty, false);
            ShowMenusBusqueda(_TipoReporte);

        }
        else
        {
            _TipoReporte = ( ViewState["rpt"] != null ? (TipoReportePublicacion) ViewState["rpt"] : TipoReportePublicacion.Autor);
            HiddenTipoReporte.Value = ((int)_TipoReporte).ToString();
            _Titulos = ViewState["titulos"] as List<TituloLibreriaView>;
            _Paginacion = ViewState["paginacion"] as Paginacion;
            //ShowMenusBusqueda(_TipoReporte);
            if (Request.Params["__EVENTTARGET"].Contains("TextBoxPaginaActual"))
            {
                string id = Request.Params["__EVENTARGUMENT"];
                PaginaSelected_Changed(id);
            }
        }
        this.ButtonBuscar.Attributes.Add("style","z-index:1;");
    }

    protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<TituloLibreriaView> lista = ViewState["titulos"] as List<TituloLibreriaView>;
            GridViewResultado.DataSource = lista;
            GridViewResultado.PageIndex = e.NewPageIndex;
            GridViewResultado.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridViewResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    GridView grid = sender as GridView;
            //    var row = e.Row;
            //    string sortedExpression = (ViewState["currentSort"] != null ? ViewState["currentSort"].ToString() : string.Empty);
            //    int indexColumn = 0;
            //    switch (sortedExpression)
            //    {
            //        case "tituloOriginal":
            //            indexColumn = 4;
            //            break;
            //        case "titulo":
            //            indexColumn = 5;
            //            break;
            //        case "autor":
            //            indexColumn = 6;
            //            break;
            //        case "año":
            //            indexColumn = 10;
            //            break;
            //        default:
            //            indexColumn = 0;
            //            break;
            //    }
            //    if (indexColumn > 0)
            //    {
            //        bool isdescending = (ViewState["isSortDescending"] != null ? (bool)ViewState["isSortDescending"] : false);
            //        string sort = (isdescending ? "desc" : "asc");
            //        e.Row.Cells[indexColumn].Attributes.Add("data-column-sorted", sort);
            //    }
            //}


            if (e.Row.RowType == DataControlRowType.Pager)
            {
                LinkButton cmdAnterior = e.Row.FindControl("cmdAnterior") as LinkButton;
                LinkButton cmdInicio = e.Row.FindControl("cmdInicio") as LinkButton;
                LinkButton cmdFinal = e.Row.FindControl("cmdFinal") as LinkButton;
                LinkButton cmdSiguiente = e.Row.FindControl("cmdSiguiente") as LinkButton;
                TextBox textPaginaActual = e.Row.FindControl("TextBoxPaginaActual") as TextBox;
                if (textPaginaActual != null)
                {
                    textPaginaActual.Attributes.Add("onkeyup", "javascript: return autoPostbackPaginador(this,event);");
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
                        imgPdf.Attributes.Add("onclick", "onSelectedResource('pdf','" + fila.UrlPdf + "')");
                    }
                    if (imgVirtual != null)
                    {
                        imgVirtual.Visible = !string.IsNullOrEmpty(fila.UrlVirtual);
                        imgVirtual.Attributes.Add("data-url", fila.UrlVirtual);
                        imgVirtual.Attributes.Add("onclick", "onSelectedResource('virtual','" + fila.UrlVirtual + "')");
                    }
                    if (imgOnline != null)
                    {
                        imgOnline.Visible = !string.IsNullOrEmpty(fila.UrlOnline);
                        imgOnline.Attributes.Add("data-url", fila.UrlOnline);
                        imgOnline.Attributes.Add("onclick", "onSelectedResource('online','" + fila.UrlOnline + "')");
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

    protected void GridViewResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton button = e.CommandSource as LinkButton;
        if (e.CommandName != "Sort")
        {
            CommandPage_Click(button, null);
        }
    }

    protected void PaginaSelected_Changed(string currentPage)
    {
        int pageInt = 0;
        Int32.TryParse(currentPage, out pageInt);
        CommandPage_Click(pageInt, null);
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
            ViewState["paginacion"] = _Paginacion;
        }
        else
        {
            if (sender != null)
            {
                if (sender.GetType() == typeof(int))
                {
                    int numeroPagina = (int)sender;
                    if (numeroPagina <= 0)
                    {
                        numeroPagina = 1;
                    }
                    else if (numeroPagina > _Paginacion.PaginasTotales)
                    {
                        numeroPagina = _Paginacion.PaginasTotales;
                    }
                    _Paginacion.PaginaActual = numeroPagina;
                    ViewState["paginacion"] = _Paginacion;
                    // Debe ser -1 ya que el comportamiento del evento, lo incrementa en +1 al asignarlo al GridView
                    GridViewResultado_PageIndexChanging(null, new GridViewPageEventArgs(_Paginacion.PaginaActual - 1));
                }
            }
        }

        //ViewState["paginacion"] = _Paginacion;
        //bool isDescending = (ViewState["isSortDescending"] == null ? false : (bool)ViewState["isSortDescending"]);
        //string columnOrder = (ViewState["currentSort"] == null ? string.Empty : ViewState["currentSort"].ToString());

        //if (((bool)ViewState["busquedaHabilitada"]) == true)
        //{
        //    Buscar(ViewState["tipoBusqueda"] == null ? "" : ViewState["tipoBusqueda"].ToString(), columnOrder, isDescending);
        //}
        //else
        //{
        //    CargarTodos(columnOrder, isDescending);
        //}
    }

    private void Buscar(bool isDescending)
    {
        string busqueda = HiddenCampoBusqueda.Value;
        string nombreFuncion = HiddenNombreFuncion.Value;
        string rangoInicio = HiddenCampoInicio.Value;
        string rangoFin = HiddenCampoFin.Value;
        string campoOrdenacion = ReportesController.GetCampoOrdenacionReporte(_TipoReporte);
        bool isLoadedAll = false;
        
        _Titulos = libreriaBd.ReportePublicacionesPor(_TipoReporte, busqueda, rangoInicio, rangoFin, nombreFuncion, null, null, false, ref isLoadedAll);
        if (isLoadedAll)
        {
            //CargarTodos(isDescending);
            HiddenNombreFuncion.Value = "";
            HiddenCampoBusqueda.Value = "";
            HiddenCampoInicio.Value = "";
            HiddenCampoFin.Value = "";
            _Paginacion.PaginasTotales = (int)Math.Ceiling(((_Titulos != null ? _Titulos.Count : 0) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
            ViewState["busquedaHabilitada"] = false;
        }
        else
        {                       
            _Paginacion.PaginasTotales = (int)Math.Ceiling(((_Titulos != null ? _Titulos.Count : 0) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
            ViewState["busquedaHabilitada"] = true;
        }

        ViewState["titulos"] = _Titulos;
        ViewState["paginacion"] = _Paginacion;        
        //ViewState["tipoBusqueda"] = "";
        ViewState["isSortDescending"] = isDescending;
        ViewState["currentSort"] = campoOrdenacion;

        if (_Titulos != null && _Titulos.Count > 0)
        {
            this.ButtonExportar.Visible = true;
        }
        else
        {
            this.ButtonExportar.Visible = false;
        }
        MostrarTitulos(_Titulos);
    }

    private void MostrarTitulos(List<TituloLibreriaView> lista)
    {
        this.GridViewResultado.DataSource = lista;
        this.GridViewResultado.DataBind();
    }

    protected void ButtonBuscar_Click(object sender, EventArgs e)
    {
        if (_TipoReporte == TipoReportePublicacion.TipoTexto)
        {
            HiddenCampoBusqueda.Value = this.DropDownTipoTexto.SelectedItem.Text.ToLower();
        }
        else {
            HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        }
        HiddenNombreFuncion.Value = this.DropDownFuncion.SelectedItem.Value;
        HiddenCampoInicio.Value = this.TextBoxInicio.Text;
        HiddenCampoFin.Value = this.TextBoxFinal.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;        
        Buscar(false);
    }

    private void ShowMenusBusqueda(TipoReportePublicacion tipoReporte) {
        switch (tipoReporte)
        {
            case TipoReportePublicacion.Anio:
                this.LabelTipoReporte.Text = "Publicaciones por año";
                ////this.DivBotonBusqueda.Attributes.Add("style", "float:left; padding-left:30px;");
                this.DivGroupBusqueda.Attributes.Add("class", "input-group busqueda-custom");
                this.DivBotonBusqueda.Attributes.Add("class", "input-group-btn busqueda-custom");
                this.DivRangosBusqueda.Visible = true;
                this.TextBoxBusqueda.Visible = false;
                this.DivContenedorTipoTexto.Visible = false;
                this.DivContenedorFuncion.Visible = false;
                this.Title = "Publicaciones por año";
                break;
            case TipoReportePublicacion.Autor:
                LabelTipoReporte.Text = "Publicaciones por Autor";
                Title = "Publicaciones por autor";
                this.DivRangosBusqueda.Visible = false;
                this.TextBoxBusqueda.Visible = true;
                this.DivContenedorTipoTexto.Visible = false;
                this.DivContenedorFuncion.Visible = false;
                break;
            case TipoReportePublicacion.TipoTexto:
                LabelTipoReporte.Text = "Publicaciones por tipo texto";
                Title = "Publicaciones por tipo texto";
                ////this.DivBotonBusqueda.Attributes.Add("style", "float:left; padding-left:30px;");
                this.DivGroupBusqueda.Attributes.Add("class", "input-group busqueda-custom");
                this.DivBotonBusqueda.Attributes.Add("class", "input-group-btn busqueda-custom");
                this.DivRangosBusqueda.Visible = false;
                this.TextBoxBusqueda.Visible = false;
                this.DivContenedorTipoTexto.Visible = true;
                this.DivContenedorFuncion.Visible = false;
                break;
            case TipoReportePublicacion.Traductor:
                LabelTipoReporte.Text = "Publicaciones por traductor";
                Title = "Publicaciones por traductor";
                this.TextBoxBusqueda.CssClass = "form-control clase";
                //this.TextBoxBusqueda.CssClass = "clase2";
                //this.TextBoxBusqueda.Attributes.Add("style", "float:left;");
                                
                this.DivGroupBusqueda.Attributes.Add("class", "input-group busqueda-custom");
                this.DivBotonBusqueda.Attributes.Add("class", "input-group-btn busqueda-custom");
                this.DivRangosBusqueda.Visible = false;
                this.TextBoxBusqueda.Visible = true;
                this.DivContenedorTipoTexto.Visible = false;
                this.DivContenedorFuncion.Visible = true;
                break;
            case TipoReportePublicacion.Edicion:
                LabelTipoReporte.Text = "Publicaciones por edicion";
                Title = "Publicaciones por edicion";
                this.DivRangosBusqueda.Visible = false;
                this.TextBoxBusqueda.Visible = true;
                this.DivContenedorTipoTexto.Visible = false;
                this.DivContenedorFuncion.Visible = false;
                break;
            case TipoReportePublicacion.Editorial:
                LabelTipoReporte.Text = "Publicaciones por editorial";
                Title = "Publicaciones por editorial";
                this.DivRangosBusqueda.Visible = false;
                this.TextBoxBusqueda.Visible = true;
                this.DivContenedorTipoTexto.Visible = false;
                this.DivContenedorFuncion.Visible = false;
                break;                
            default:
                LabelTipoReporte.Text = "Publicaciones por Autor";
                Title = "Publicaciones por autor";
                this.DivRangosBusqueda.Visible = false;
                this.TextBoxBusqueda.Visible = true;
                this.DivContenedorTipoTexto.Visible = false;
                this.DivContenedorFuncion.Visible = false;
                break;
        }
    }

   

    //protected void GridViewResultado_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    string campoOrden = e.SortExpression;
    //    bool isDescending = (ViewState["isSortDescending"] != null) ? (bool)ViewState["isSortDescending"] : false;
    //    string currentSort = (ViewState["currentSort"] != null ? ViewState["currentSort"].ToString() : string.Empty);
    //    if (currentSort != e.SortExpression)
    //    {
    //        isDescending = false;
    //    }
    //    else
    //    {
    //        if (isDescending)
    //        {
    //            isDescending = false;
    //        }
    //        else
    //        {
    //            isDescending = true;
    //        }
    //    }
    //    List<TituloLibreriaView> lista = ViewState["titulos"]  as List<TituloLibreriaView>;
    //    lista = LibreriaController.TitulosOrderBy(lista, campoOrden, isDescending);
    //    ViewState["isSortDescending"] = isDescending;
    //    ViewState["currentSort"] = campoOrden;
    //    ViewState["titulos"] = lista;
    //    MostrarTitulos(lista);
    //}

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