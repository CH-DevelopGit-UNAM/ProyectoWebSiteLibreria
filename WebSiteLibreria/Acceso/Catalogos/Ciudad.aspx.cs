using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unam.CoHu.Libreria.Controller;
using Unam.CoHu.Libreria.Controller.Catalogos;
using Unam.CoHu.Libreria.Controller.Enums;
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

public partial class Catalogos_Ciudad : SecurePage
{
    CiudadesController catalogos = new CiudadesController();
    List<Ciudad> _Ciudades = null;
    Paginacion _Paginacion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {            
            _Paginacion = new Paginacion() { FilasPorPagina = GridViewResultado.PageSize, PaginaActual = 1 };
            ViewState["busquedaHabilitada"] = false;
            CargarTodos();
        }
        else {
            _Ciudades = ViewState["ciudades"] as List<Ciudad>;
            _Paginacion = ViewState["paginacion"] as Paginacion;
            if (Request.Params["__EVENTTARGET"].Contains("TextBoxPaginaActual"))
            {
                string id = Request.Params["__EVENTARGUMENT"];
                PaginaSelected_Changed(id);
            }
            //if (Request.Params["__EVENTTARGET"] == this.TextBoxPaginaActual.ClientID || Request.Params["__EVENTTARGET"] == this.TextBoxPaginaActual.UniqueID) {
            //    PaginaSelected_Changed(this.TextBoxPaginaActual);
            //}            
        }
        // registrar en cada postback (asincrono o no) el "enter" para el campo paginador
        //this.TextBoxPaginaActual.Attributes.Add("onkeyup", "javascript: return autoPostbackPaginador(event);");
    }

    protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<Ciudad> lista = ViewState["ciudades"] as List<Ciudad>;
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
    }

    protected void GridViewResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton button = e.CommandSource as LinkButton;
        CommandPage_Click(button, null);
    }

    protected void LinkBuscarNombre_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar();
    }

    protected void LinkTodos_Click(object sender, EventArgs e)
    {
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        CargarTodos();
    }

    private void CargarTodos()
    {
        _Ciudades = catalogos.CargarTodos();//catalogos.PaginarCiudades(ref _Paginacion);
        _Paginacion.PaginasTotales = (int)Math.Ceiling(((_Ciudades != null ? _Ciudades.Count : 0) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
        HiddenCampoBusqueda.Value = "";
        ViewState["ciudades"] = _Ciudades;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        MostrarCiudades(_Ciudades);//, _Paginacion);
    }

    private void Buscar()
    {
        string busqueda = HiddenCampoBusqueda.Value;
        if (String.IsNullOrEmpty(busqueda))
        {
            _Ciudades = new List<Ciudad>();
            _Paginacion.FilasTotales = 0;
            _Paginacion.PaginasTotales = 0;
            _Paginacion.PaginaActual = 0;
        }
        else
        {
            _Ciudades = catalogos.BuscarCiudadPor(busqueda, null); //catalogos.PaginarCiudadPorNombre(busqueda, ref _Paginacion);
            _Paginacion.PaginasTotales = (int)Math.Ceiling(((_Ciudades != null ? _Ciudades.Count : 0) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
        }
        ViewState["ciudades"] = _Ciudades;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = true;
        MostrarCiudades(_Ciudades);//, _Paginacion);
    }

    private void CargarPorId(string id) {
        HiddenCampoBusqueda.Value = "";
        TextBoxBusqueda.Text = "";
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        _Ciudades.Clear();
        _Ciudades.Add(catalogos.PaginarCiudadPorId(id, ref _Paginacion));
        ViewState["ciudades"] = _Ciudades;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        MostrarCiudades(_Ciudades);//, _Paginacion);
    }

    private void MostrarCiudades(List<Ciudad> lista) //, Paginacion paginacion)
    {
        this.GridViewResultado.DataSource = lista;
        this.GridViewResultado.DataBind();
        //int tamano = 0;
        //if (lista != null && lista.Count > 0)
        //{
        //    tamano = ((paginacion.PaginaActual - 1) * paginacion.FilasPorPagina) + lista.Count;
        //    this.LabelRegistroInicial.Text = (tamano - (lista.Count - 1)).ToString();
        //    this.LabelRegistroFinal.Text = (tamano).ToString();
        //    FooterGridResultado.Visible = true;
        //}
        //else
        //{
        //    this.LabelRegistroInicial.Text = "0";
        //    this.LabelRegistroFinal.Text = "0";
        //    FooterGridResultado.Visible = false;
        //}

        //this.LabelPaginaFinal.Text = paginacion.PaginasTotales.ToString();
        //this.TextBoxPaginaActual.Text = paginacion.PaginaActual.ToString();
        //DeshabilitarNavegacionInicial(paginacion);
    }

    protected void CommandPage_Click(object sender, EventArgs e)
    {
        /*LinkButton button = sender as LinkButton;
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
        else {
            TextBox textBox = sender as TextBox;
            if (textBox != null) {
                string numero = textBox.Text;
                int numeroPagina = 0;
                Int32.TryParse(numero, out numeroPagina);

                if (numeroPagina <= 0)
                {
                    numeroPagina = 1;
                }
                else if (numeroPagina > _Paginacion.PaginasTotales) {
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

        if (((bool)ViewState["busquedaHabilitada"]) == true)
        {
            Buscar();
        }
        else
        {
            CargarTodos();
        }*/

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
    }

    //private void DeshabilitarNavegacionInicial(Paginacion paginacion)
    //{
    //    if (paginacion.PaginasTotales <= 0 && paginacion.IsNavigating == false)
    //    {
    //        cmdAnterior.Enabled = false;
    //        cmdInicio.Enabled = false;
    //        cmdSiguiente.Enabled = false;
    //        cmdFinal.Enabled = false;
    //        TextBoxPaginaActual.Enabled = false;
    //    }
    //    else if (paginacion.PaginasTotales == paginacion.PaginaActual && paginacion.IsNavigating == false)
    //    {
    //        cmdAnterior.Enabled = false;
    //        cmdInicio.Enabled = false;
    //        cmdSiguiente.Enabled = false;
    //        cmdFinal.Enabled = false;
    //        TextBoxPaginaActual.Enabled = false;
    //    }
    //    else if (paginacion.PaginasTotales > 0 && paginacion.IsNavigating == false)
    //    {
    //        cmdAnterior.Enabled = false;
    //        cmdInicio.Enabled = false;
    //        cmdSiguiente.Enabled = true;
    //        cmdFinal.Enabled = true;
    //        TextBoxPaginaActual.Enabled = true;
    //    }

    //}

    protected void PaginaSelected_Changed(string currentPage)
    {
        int pageInt = 0;
        Int32.TryParse(currentPage, out pageInt);
        CommandPage_Click(pageInt, null);
    }

    protected void ButtonAceptar_Click(object sender, EventArgs e)
    {
        int tipoOperacion = -1;
        Int32.TryParse(HiddenTipoOperacion.Value, out tipoOperacion);
        TipoOperacion operacion = Utilidades.TipoOperacionFromInt(tipoOperacion);
        string clave = HiddenFieldClave.Value;
        string descripcion = TextBoxDescripcion.Text;
        int rowsAffected = 0;
        List<Ciudad> lista = new List<Ciudad>();
        try
        {
            if (operacion != TipoOperacion.Invalido)
            {
                Ciudad ciudad = new Ciudad();
                ciudad.IdCiudad = clave;
                ciudad.Descripcion = descripcion;

                if (operacion == TipoOperacion.Agregar)
                {
                    rowsAffected = catalogos.Registrar(ciudad);                    
                }
                else if (operacion == TipoOperacion.Editar)
                {
                    rowsAffected = catalogos.Actualizar(ciudad);                    
                }

                if (rowsAffected > 0)
                {
                    CargarPorId(ciudad.IdCiudad);                    
                    Utilidades.MostrarMensaje("Se guardaron los cambios", TipoMensaje.Success, null);
                }
                else {
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



}