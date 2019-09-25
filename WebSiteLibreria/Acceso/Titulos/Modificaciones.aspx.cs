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
using Unam.CoHu.Libreria.Controller.Enums;
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;
using Unam.CoHu.Libreria.Model.WebServices;

public partial class Acceso_Titulos_Modificaciones : SecurePage
{
    LibreriaController libreriaBd = new LibreriaController();
    ResponsableController respDetalle = new ResponsableController();
    List<TituloLibreriaView> _Titulos = null;
    Paginacion _Paginacion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("----------------------------------> Page -> Page_Load ");
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
                        indexColumn = 8;
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
                    Label labelEdicion = e.Row.FindControl("LabelEdicion") as Label;
                    if (labelEdicion != null)
                    {
                        labelEdicion.Text = LibreriaController.ObtenerNumeroEdicion(fila.NumeroEdicion);
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
        _Titulos = libreriaBd.PaginarTitulos(null, campoOrdenacion, isDescending, ref _Paginacion);
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
             && !tipoBusqueda.Equals("Autor", StringComparison.InvariantCultureIgnoreCase) )
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
                _Titulos = libreriaBd.PaginarTitulosPorNombre(busqueda, null , campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if(tipoBusqueda.Equals("Tema", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPorTema(busqueda, null, campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if (tipoBusqueda.Equals("Responsable", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPor(null, null, null, busqueda, null, null, null , campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if (tipoBusqueda.Equals("Ciudad", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPor(null, busqueda, null, null, null, null, null, campoOrdenacion, isDescending, ref _Paginacion);
            }
            else if (tipoBusqueda.Equals("Autor", StringComparison.InvariantCultureIgnoreCase))
            {
                _Titulos = libreriaBd.PaginarTitulosPor(null, null, null, null, busqueda, null, null, campoOrdenacion, isDescending, ref _Paginacion);
            }
            else 
            {
                _Titulos = libreriaBd.PaginarTitulosPorNombre(busqueda, null, campoOrdenacion, isDescending, ref _Paginacion);
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
        _Titulos.Add(libreriaBd.PaginarTituloPorId(id,ref _Paginacion));
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
            Buscar(ViewState["tipoBusqueda"] == null ? "": ViewState["tipoBusqueda"].ToString(), columnOrder, isDescending);
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

    protected void ButtonAceptar_Click(object sender, EventArgs e)
    {
        int tipoOperacion = -1;
        Int32.TryParse(HiddenTipoOperacion.Value, out tipoOperacion);
        TipoOperacion operacion = Utilidades.TipoOperacionFromInt(tipoOperacion);
        string clave = HiddenFieldClave.Value;
        int intDummy = 0;
        int rowsAffected = 0;

        Int32.TryParse(clave, out intDummy);
        int idTitulo = intDummy;
        //string idIsbn = HiddenIdIsbn.Value;
        string idCiudad = HiddenIdCiudad.Value;
        string idEditor = HiddenIdEditor.Value;        
        string idAutor = HiddenIdAutor.Value;
        string tituloOriginal = TextBoxTituloOriginal.Text;
        string titulo = TextBoxTitulo.Text;
        string edicion = string.Empty; // TextBoxEdicion.Text;
        intDummy = 0;
        Int32.TryParse(TextBoxAnio.Text, out intDummy);
        int anioPub = intDummy;
        string paginas = TextBoxPaginas.Text;
        string medidas = TextBoxMedidas.Text;
        string serie500 = TextBoxSerie500.Text;
        string contenido = TextBoxContenido.Text;
        string cualidades = TextBoxCualidades.Text;
        string colofon = TextBoxColofon.Text;
        string tema = TextBoxTema.Text;
        string secundarias = TextBoxSecundarias.Text;
        string uFFyl = TextBoxU_FFYL.Text;
        string uIIfl = TextBoxU_IIFL.Text;
        string observaciones = TextBoxObservaciones.Text;
        int intReimpresion = 0;
        string reimpresion = TextBoxReimpresionNumero.Text;
        Int32.TryParse(reimpresion, out intReimpresion);
        int intEdicion = 0;
        string edicionNumero = TextBoxEdicionNumero.Text;
        Int32.TryParse(edicionNumero, out intEdicion);

        //string urlPdf = (!string.IsNullOrEmpty(TextBoxUrlPdf.Text) ? HttpUtility.UrlPathEncode(string.Format("{0}{1}",ConfiguracionSitio.ObtenerVariableConfig("HostPdf"), TextBoxUrlPdf.Text.Trim())) : string.Empty); 
        string urlPdf = (!string.IsNullOrEmpty(TextBoxUrlPdf.Text) ? HttpUtility.UrlPathEncode(TextBoxUrlPdf.Text.Trim()) : string.Empty);
        string urlVirtual = (!string.IsNullOrEmpty(TextBoxUrlVirtual.Text) ? HttpUtility.UrlPathEncode(TextBoxUrlVirtual.Text.Trim()) : string.Empty);
        string urlOnline = (!string.IsNullOrEmpty(TextBoxUrlOnline.Text) ? HttpUtility.UrlPathEncode(TextBoxUrlOnline.Text.Trim()) : string.Empty);
        string idSerie = HiddenIdSerie.Value;
        string idResponsableDetalle = HiddenIdResp.Value;
        string listaResponsables = HiddenListaResponsable.Value;
        string listaIsbn= HiddenListaIsbn.Value;


        bool isNovedad = CheckIsNovedad.Checked;
        try
        {
            if (operacion != TipoOperacion.Invalido)
            {
                TituloLibreria tituloLib = new TituloLibreria();
                tituloLib.IdTitulo = idTitulo;
                //tituloLib.IdIsbn = idIsbn;
                tituloLib.IdCiudad= idCiudad;
                tituloLib.IdEditor = idEditor;
                tituloLib.IdAutor = idAutor;
                tituloLib.IdResponsableDetalle = idResponsableDetalle;
                tituloLib.Titulo = titulo;
                tituloLib.TituloOriginal = tituloOriginal;
                //tituloLib.Edicion = edicion;
                tituloLib.Edicion = LibreriaController.ObtenerNumeroEdicion(intEdicion);
                tituloLib.AnioPublicacion = anioPub;
                tituloLib.Paginas = paginas;
                tituloLib.Medidas = medidas;
                tituloLib.Serie500 = serie500;
                tituloLib.Contenido = contenido;
                tituloLib.Cualidades = cualidades;
                tituloLib.Colofon = colofon;
                tituloLib.Tema = tema;
                tituloLib.Secundarias = secundarias;
                tituloLib.UffYL = uFFyl;
                tituloLib.UiiFL = uIIfl;
                tituloLib.Observaciones = observaciones;
                tituloLib.IdSerie = idSerie;
                tituloLib.IsNovedad = isNovedad;
                tituloLib.NumeroEdicion = intEdicion;
                tituloLib.NumeroReimpresion = intReimpresion;
                tituloLib.UrlPdf = urlPdf;
                tituloLib.UrlOnline = urlOnline;
                tituloLib.UrlVirtual = urlVirtual;
                tituloLib.IsLatin = RadioIsLatin.Checked;
                tituloLib.IsGriego= RadioIsGriego.Checked;

                if (string.IsNullOrEmpty(tituloLib.IdEditor) || string.IsNullOrEmpty(tituloLib.IdAutor)
                    || string.IsNullOrEmpty(tituloLib.TituloOriginal) || tituloLib.AnioPublicacion <=0 || string.IsNullOrEmpty(tituloLib.IdSerie)
                    || string.IsNullOrEmpty(listaResponsables))
                {
                    if (operacion == TipoOperacion.Editar)
                    {
                        if (string.IsNullOrEmpty(tituloLib.IdResponsableDetalle))
                        {
                            Utilidades.MostrarMensaje("Los campos 'Editor', 'Responsable', 'Autor','Titulo Original', 'Año Publicacion', 'Serie' y 'Responsables' son obligatorios. Revise la información e intente de nuevo", TipoMensaje.Warning, null);
                            return;
                        }
                    }
                    else {
                        Utilidades.MostrarMensaje("Los campos 'Editor', 'Responsable', 'Autor','Titulo Original', 'Año Publicacion', 'Serie' y 'Responsables' son obligatorios. Revise la información e intente de nuevo", TipoMensaje.Warning, null);
                        return;
                    }
                }

                List<ResponsableTitulo> responsables = GetListaResponsables(listaResponsables);
                List<Isbn> isbns = GetListaIsbn(listaIsbn);
                
                if (responsables == null) {
                    Utilidades.MostrarMensaje("No se pudieron guardar los cambios: No hay una lista de responsables a asignar", TipoMensaje.Warning, null);
                    return;
                }
                                
                if (operacion == TipoOperacion.Agregar)
                {
                    rowsAffected = libreriaBd.Registrar(tituloLib, responsables, isbns);
                }
                else if (operacion == TipoOperacion.Editar)
                {
                    rowsAffected = libreriaBd.Actualizar(tituloLib, responsables, isbns);
                }

                if (rowsAffected > 0)
                {
                    //CargarPorId(tituloLib.IdTitulo);
                    CargarTodos(string.Empty, false);
                    Utilidades.MostrarMensaje("Se guardaron los cambios", TipoMensaje.Success, null);
                }
                else
                {
                    Utilidades.MostrarMensaje("No se guardaron los cambios", TipoMensaje.Warning, null);
                }
            }
            else
            {
                Utilidades.MostrarMensaje("Operacion no reconocida", TipoMensaje.Error, null);
            }
        }
        catch (Exception ex)
        {
            if (rowsAffected > 0)
            {
                Utilidades.MostrarMensaje("Ocurrió un error desconocido, pero se guardaron los datos", TipoMensaje.Warning, null);
            }
            else
            {
                Utilidades.MostrarMensaje("Error en BD: No se guardaron los datos", TipoMensaje.Error, null);
            }
        }    
    }

    protected void LinkDelete_Click(object sender, EventArgs e) 
    {
        int tipoOperacion = -1;
        Int32.TryParse(HiddenTipoOperacion.Value, out tipoOperacion);
        TipoOperacion operacion = Utilidades.TipoOperacionFromInt(tipoOperacion);
        string clave = HiddenFieldClave.Value;
        int intDummy = 0;
        int rowsAffected = 0;

        Int32.TryParse(clave, out intDummy);
        int idTitulo = intDummy;
        try
        {
            if (operacion == TipoOperacion.Eliminar)
            {
                rowsAffected = libreriaBd.Borrar(idTitulo);

                if (rowsAffected > 0)
                {                    
                    CargarTodos(string.Empty, false);
                    Utilidades.MostrarMensaje("Se eliminó el registro", TipoMensaje.Success, null);
                }
                else
                {
                    Utilidades.MostrarMensaje("No se pudo eliminar el registro", TipoMensaje.Warning, null);
                }
            }
            else
            {
                Utilidades.MostrarMensaje("Operacion no reconocida", TipoMensaje.Error, null);
            }
        }
        catch (Exception ex)
        {
            Utilidades.MostrarMensaje("Error en BD: No se guardaron los cambios", TipoMensaje.Error, null);
        }
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
            Buscar(ViewState["tipoBusqueda"].ToString(), campoOrden, isDescending);
        }
        else
        {
            CargarTodos(campoOrden, isDescending);
        }
    }


    private List<ResponsableTitulo> GetListaResponsables(string listaResponsables)
    {
        string[] detalleResponsables = listaResponsables.Split(new char[] { '|' });
        List<ResponsableTitulo> lista = null;
        try
        {
            int indice = 1;
            lista = new List<ResponsableTitulo>();
            foreach (string detalle in detalleResponsables)
            {
                if (!string.IsNullOrEmpty(detalle.Trim())) {
                    string[] titulo = detalle.Split(new char[] { '#' });
                    ResponsableTitulo obj = new ResponsableTitulo() { };
                    obj.IdResponsableDetalle = titulo[0];
                    obj.IdResponsable = titulo[1];
                    obj.IdFuncion = titulo[2];
                    obj.DescripcionFuncion = null;
                    obj.OrdenFuncion = indice;
                    lista.Add(obj);
                    indice++;
                }                
            }
        }
        catch (Exception ex)
        {
            lista = null;            
        }
        return lista;
    }

    private List<Isbn> GetListaIsbn(string listaIsbn)
    {
        string[] detalleIsbn= listaIsbn.Split(new char[] { '|' });
        List<Isbn> lista = null;
        try
        {
            int indice = 1;
            lista = new List<Isbn>();
            int intDummy = -1;
            foreach (string detalle in detalleIsbn)
            {
                if (!string.IsNullOrEmpty(detalle.Trim()))
                {
                    string[] isbn = detalle.Split(new char[] { '#' });
                    Isbn obj = new Isbn() { };

                    intDummy = 0;
                    Int32.TryParse(isbn[0], out intDummy);
                    obj.IdTitulo = intDummy;
                                        
                    obj.ClaveIsbn = isbn[1];
                    intDummy = 0;

                    Int32.TryParse(isbn[2], out intDummy);
                    obj.IdDescripcion = intDummy;


                    intDummy = 0;
                    Int32.TryParse(isbn[3], out intDummy);
                    obj.Edicion = intDummy;

                    intDummy = 0;
                    Int32.TryParse(isbn[4], out intDummy);
                    obj.Reedicion = intDummy;

                    intDummy = 0;
                    Int32.TryParse(isbn[5], out intDummy);
                    obj.Reimpresion = intDummy;



                    lista.Add(obj);
                    indice++;
                }
            }
        }
        catch (Exception ex)
        {
            lista = null;
        }
        return lista;
    }


    [WebMethod]
    [ScriptMethod]
    public static List<ResponsableTituloDetail> Metodo(int idTitulo) {
        List<ResponsableTituloDetail> lista = new List<ResponsableTituloDetail>();
        LibreriaController controller = new LibreriaController();
        if (AutenticacionSitio.IsUsuarioAutenticado())
        {
            lista = controller.CargarResponsablesTitulo(idTitulo);
        }
        else {
            lista = new List<ResponsableTituloDetail>();
        }
        

        return lista;
    }

    [WebMethod(EnableSession = true)]
    public static void PokePage()
    {
        
    }

    [WebMethod(EnableSession =true, TransactionOption = System.EnterpriseServices.TransactionOption.Supported )]
    public static TituloLibreriaView GetTitulo(int idTitulo)
    {
        var data = HttpContext.Current.Session;

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

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Autor> BuscarAutores(string busqueda)
    {
        List<Autor> autores= null;
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

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Ciudad> BuscarCiudades(string busqueda)
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

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Editor> BuscarEditores(string busqueda)
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
    //public static List<Isbn> BuscarIsbn(string busqueda)
    //{
    //    List<Isbn> isbns= null;
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
    //            isbns = new List<Isbn>() { new Isbn() { IdIsbn = "", ClaveIsbn = "NO SE ENCONTRO EL ISBN" } };
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        isbns = new List<Isbn>() { new Isbn() { IdIsbn = "", ClaveIsbn = "NO SE ENCONTRO EL ISBN" } };
    //        HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //    }
        
    //    return isbns;
    //}

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Serie> BuscarSerie(string busqueda)
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
                isbns = new List<Serie>() { new Serie() { IdSerie = "", NombreLatin = "NO SE ENCONTRO EL ISBN" , NombreGriego = "NO SE ENCONTRO EL ISBN" } };
            }
        }
        catch (Exception ex)
        {
            isbns = new List<Serie>() { new Serie() { IdSerie = "", NombreLatin = "NO SE ENCONTRO EL ISBN", NombreGriego = "NO SE ENCONTRO EL ISBN" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return isbns;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Responsable> BuscarResponsables (string busqueda)
    {
        List<Responsable> responsables = null;        
        ResponsableController controller = new ResponsableController();
        try
        {
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {                
                responsables = controller.BusquedaResponsables(busqueda , new int?(30));
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
            responsables = new List<Responsable>() { new Responsable() { IdResponsable= "", Nombre = "NO SE ENCONTRO EL RESPONSABLE" } };
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        return responsables;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Funcion> CargarFunciones()
    {
        List<Funcion> funciones= null;
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
            else {
                retorno = new List<Funcion>();
                retorno.Add(new Funcion() { IdFuncion="", TipoFuncion="Seleccione una opción..." });
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


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet =false)]
    public static WebResult LoadFile()
    {
        WebResult result = new WebResult();
        try
        {
            var file2 =  HttpContext.Current.Request.Files;

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


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static WebResult DeleteFile(int id)
    {
        WebResult result = new WebResult();
        try
        { 
            if (AutenticacionSitio.IsUsuarioAutenticado())
            {
                string pathUpload = ConfiguracionSitio.ObtenerVariableConfig("FolderSavePostedFiles");
                string urlImage = ConfiguracionSitio.ObtenerVariableConfig("PathPostedFiles");
                string mismoServidor = ConfiguracionSitio.ObtenerVariableConfig("IsSameServerPostedFiles");
                bool isMismoServidor = true;
                bool.TryParse(mismoServidor, out isMismoServidor);
                string urlPostedFile = string.Empty;

                bool fileDeleted = false;

                if (isMismoServidor)
                {
                    pathUpload = HttpContext.Current.Server.MapPath(pathUpload);
                    fileDeleted = true;
                }

                LibreriaController libController = new LibreriaController();
                int rowsAffected = libController.EliminarImagen(id, pathUpload, fileDeleted);
                if (rowsAffected > 0)
                {
                    result.MensajeResultado = "Imagen eliminada";
                    result.IsProcesado = true;
                }
                else {
                    result.MensajeResultado = "La imagen no se pudo eliminar";
                    result.IsProcesado = false;
                }
                
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