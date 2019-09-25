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

public partial class Catalogos_Autores : SecurePage
{
    AutoresController catalogos = new AutoresController();
    List<Autor> _Autores = null;    
    Paginacion _Paginacion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("----------------------------------> Page -> Page_Load ");
        if (!IsPostBack)
        {
            _Paginacion = new Paginacion() { FilasPorPagina = GridViewResultado.PageSize, PaginaActual = 1 };
            ViewState["busquedaHabilitada"] = false;
            CargarTodos();
        }
        else
        {
            _Autores = ViewState["autores"] as List<Autor>;
            _Paginacion = ViewState["paginacion"] as Paginacion;
            if (Request.Params["__EVENTTARGET"] == this.TextBoxPaginaActual.ClientID || Request.Params["__EVENTTARGET"] == this.TextBoxPaginaActual.UniqueID)
            {
                PaginaSelected_Changed(this.TextBoxPaginaActual);
            }
        }
        // registrar en cada postback (asincrono o no) el "enter" para el campo paginador
        this.TextBoxPaginaActual.Attributes.Add("onkeyup", "javascript: return autoPostbackPaginador(event);");
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
        _Autores = catalogos.PaginarTodos(ref _Paginacion);
        HiddenCampoBusqueda.Value = "";
        ViewState["autores"] = _Autores;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        MostrarAutores(_Autores, _Paginacion);
    }

    private void Buscar()
    {
        string busqueda = HiddenCampoBusqueda.Value;
        if (String.IsNullOrEmpty(busqueda))
        {
            _Autores = new List<Autor>();
            _Paginacion.FilasTotales = 0;
            _Paginacion.PaginasTotales = 0;
            _Paginacion.PaginaActual = 0;
        }
        else
        {
            _Autores = catalogos.PaginarAutorPor(busqueda, ref _Paginacion);
        }
        ViewState["autores"] = _Autores;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = true;
        MostrarAutores(_Autores, _Paginacion);
    }

    private void CargarPorId(string id)
    {
        HiddenCampoBusqueda.Value = "";
        TextBoxBusqueda.Text = "";
        _Paginacion.PaginaActual = 1;
        _Paginacion.IsNavigating = false;
        _Autores.Clear();
        _Autores.Add(catalogos.PaginarPorId(id, ref _Paginacion));
        ViewState["autores"] = _Autores;
        ViewState["paginacion"] = _Paginacion;
        ViewState["busquedaHabilitada"] = false;
        MostrarAutores(_Autores, _Paginacion);
    }

    private void MostrarAutores(List<Autor> lista, Paginacion paginacion)
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

        if (((bool)ViewState["busquedaHabilitada"]) == true)
        {
            Buscar();
        }
        else
        {
            CargarTodos();
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

    protected void ButtonAceptar_Click(object sender, EventArgs e)
    {
        int tipoOperacion = -1;
        Int32.TryParse(HiddenTipoOperacion.Value, out tipoOperacion);
        TipoOperacion operacion = Utilidades.TipoOperacionFromInt(tipoOperacion);
        string clave = HiddenFieldClave.Value;
        string nombreLatin = TextBoxLatin.Text;
        string nombreGriego = TextBoxGriego.Text;
        string nombreEspanol = TextBoxEspanol.Text;
        
        int rowsAffected = 0;
        List<Autor> lista = new List<Autor>();
        try
        {
            if (operacion != TipoOperacion.Invalido)
            {
                Autor autor = new Autor();
                autor.IdAutor= clave;
                autor.NombreLatin = nombreLatin;
                autor.NombreGriego = nombreGriego;
                autor.NombreEspanol= nombreEspanol;

                if (string.IsNullOrEmpty(nombreEspanol))
                {
                    Utilidades.MostrarMensaje("El campo 'Nombre Español' es obligatorio. Revise la información e intente de nuevo", TipoMensaje.Warning, null);
                    return;
                }

                if (operacion == TipoOperacion.Agregar)
                {
                    rowsAffected = catalogos.Registrar(autor);
                }
                else if (operacion == TipoOperacion.Editar)
                {
                    rowsAffected = catalogos.Actualizar(autor);
                }

                if (rowsAffected > 0)
                {
                    CargarPorId(autor.IdAutor);
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

    protected void PaginaSelected_Changed(object sender)
    {
        CommandPage_Click(sender, null);
    }

    protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void GridViewResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}