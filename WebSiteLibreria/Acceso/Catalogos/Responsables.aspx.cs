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

public partial class Catalogos_Responsables : SecurePage
{
    ResponsableController catalogos = new ResponsableController();
    List<Responsable> _Responsables = null;
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
            _Responsables = ViewState["responsables"] as List<Responsable>;
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
            List<Responsable> lista = ViewState["responsables"] as List<Responsable>;
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

    protected void LinkBuscarRfc_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(LinkBuscarRfc.Text);
    }

    protected void LinkBuscarNombre_Click(object sender, EventArgs e)
    {
        HiddenCampoBusqueda.Value = this.TextBoxBusqueda.Text;
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        Buscar(LinkBuscarNombre.Text);
    }

    protected void LinkTodos_Click(object sender, EventArgs e)
    {
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        CargarTodos();
    }

    private void CargarTodos()
    {
        _Responsables = catalogos.CargarResponsables();
        _Paginacion.PaginasTotales = (int)Math.Ceiling(((_Responsables != null ? _Responsables.Count : 0) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
        HiddenCampoBusqueda.Value = "";
        ViewState["responsables"] = _Responsables;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        ViewState["tipoBusqueda"] = "";
        MostrarResponsables(_Responsables);
    }

    private void Buscar(string tipoBusqueda)
    {
        string busqueda = HiddenCampoBusqueda.Value;        
        if (!tipoBusqueda.Equals("rfc", StringComparison.InvariantCultureIgnoreCase) && !tipoBusqueda.Equals("nombre", StringComparison.InvariantCultureIgnoreCase))
        {
            busqueda = string.Empty;
        }

        if (String.IsNullOrEmpty(busqueda))
        {
            _Responsables = new List<Responsable>();
            _Paginacion.FilasTotales = 0;
            _Paginacion.PaginasTotales = 0;
            _Paginacion.PaginaActual = 0;
        }
        else
        {
            if (tipoBusqueda.Equals("rfc", StringComparison.InvariantCultureIgnoreCase))
            {
                _Responsables = catalogos.CargarResponsablesPor(busqueda, null, null);
            }
            else
            {
                _Responsables = catalogos.CargarResponsablesPor(null, busqueda, null);
            }
            _Paginacion.PaginasTotales = (int)Math.Ceiling(( (_Responsables != null ? _Responsables.Count : 0) * 1.00M) / (_Paginacion.FilasPorPagina * 1.00M));
        }

        ViewState["responsables"] = _Responsables;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = true;
        ViewState["tipoBusqueda"] = tipoBusqueda;
        MostrarResponsables(_Responsables);
    }

    private void CargarPorId(string id)
    {
        HiddenCampoBusqueda.Value = "";
        TextBoxBusqueda.Text = "";
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        _Responsables.Clear();
        _Responsables.Add(catalogos.CargarResponsablePorId(id));
        ViewState["responsables"] = _Responsables;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        ViewState["tipoBusqueda"] = "";
        MostrarResponsables(_Responsables);
    }

    private void MostrarResponsables(List<Responsable> lista)
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
        string nombre = TextBoxNombre.Text;
        string apPaterno = TextBoxApPaterno.Text;
        string apMaterno = TextBoxApMaterno.Text;
        string rfc = TextBoxRfc.Text;
        string descripcion = TextBoxDescripcion.Text;
        int rowsAffected = 0;
        List<Responsable> lista = new List<Responsable>();
        try
        {
            if (operacion != TipoOperacion.Invalido)
            {
                Responsable resp = new Responsable();
                resp.IdResponsable = clave;
                resp.Nombre = nombre;
                resp.ApellidoPaterno = apPaterno;
                resp.ApellidoMaterno = apMaterno;
                resp.Rfc = rfc;
                resp.Descripcion = descripcion;

                if (string.IsNullOrEmpty(resp.Nombre))
                {
                    Utilidades.MostrarMensaje("El campo 'Nombre' es obligatorio. Revise la información e intente de nuevo", TipoMensaje.Warning, null);
                    return;
                }

                if (operacion == TipoOperacion.Agregar)
                {
                    rowsAffected = catalogos.RegistrarResponsable(resp);
                }
                else if (operacion == TipoOperacion.Editar)
                {
                    rowsAffected = catalogos.ActualizarResponsable(resp);
                }

                if (rowsAffected > 0)
                {
                    CargarPorId(resp.IdResponsable);
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