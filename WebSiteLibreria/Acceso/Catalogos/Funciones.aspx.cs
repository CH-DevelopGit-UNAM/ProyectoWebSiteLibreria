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

public partial class Catalogos_Funciones : SecurePage
{
    FuncionesController catalogos = new FuncionesController();
    List<Funcion> _Funciones = null;
    Paginacion _Paginacion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _Paginacion = new Paginacion() { FilasPorPagina = GridViewResultado.PageSize, PaginaActual = 1 };
            ViewState["busquedaHabilitada"] = false;
            CargarTodos();
        }
        else
        {
            _Funciones = ViewState["funciones"] as List<Funcion>;
            _Paginacion = ViewState["paginacion"] as Paginacion;
            if (Request.Params["__EVENTTARGET"].Contains("TextBoxPaginaActual"))
            {
                string id = Request.Params["__EVENTARGUMENT"];
                PaginaSelected_Changed(id);
            }
        }

    }

    protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<Funcion> lista = ViewState["funciones"] as List<Funcion>;
            GridViewResultado.DataSource = lista;
            GridViewResultado.PageIndex = e.NewPageIndex;
            GridViewResultado.DataBind();
        }
        catch (Exception ex)
        {
            int a = 0;
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
        _Funciones = catalogos.CargarTodos();
        _Paginacion.PaginasTotales = (int)Math.Ceiling(((_Funciones != null ? _Funciones.Count : 0) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
        HiddenCampoBusqueda.Value = "";
        ViewState["funciones"] = _Funciones;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        MostrarFunciones(_Funciones);
    }

    private void Buscar()
    {
        string busqueda = HiddenCampoBusqueda.Value;
        if (String.IsNullOrEmpty(busqueda))
        {
            _Funciones = new List<Funcion>();
            _Paginacion.FilasTotales = 0;
            _Paginacion.PaginasTotales = 0;
            _Paginacion.PaginaActual = 0;
        }
        else
        {
            _Funciones = catalogos.BuscarPor(busqueda, null);
            _Paginacion.PaginasTotales = (int)Math.Ceiling(((_Funciones != null ? _Funciones.Count : 0 ) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
        }

        ViewState["funciones"] = _Funciones;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = true;
        MostrarFunciones(_Funciones);
    }

    private void CargarPorId(string id)
    {
        HiddenCampoBusqueda.Value = "";
        TextBoxBusqueda.Text = "";
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        _Funciones.Clear();
        _Funciones.Add(catalogos.CargarPorId(id));
        ViewState["funciones"] = _Funciones;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        MostrarFunciones(_Funciones);
    }

    private void MostrarFunciones(List<Funcion> lista)
    {
        this.GridViewResultado.DataSource = lista;
        this.GridViewResultado.DataBind();
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
    }

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
        List<Serie> lista = new List<Serie>();
        try
        {
            if (operacion != TipoOperacion.Invalido)
            {
                Funcion funcion = new Funcion();
                funcion.IdFuncion = clave;
                funcion.TipoFuncion = descripcion;

                if (operacion == TipoOperacion.Agregar)
                {
                    rowsAffected = catalogos.Registrar(funcion);
                }
                else if (operacion == TipoOperacion.Editar)
                {
                    rowsAffected = catalogos.Actualizar(funcion);
                }

                if (rowsAffected > 0)
                {
                    CargarPorId(funcion.IdFuncion);
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
}