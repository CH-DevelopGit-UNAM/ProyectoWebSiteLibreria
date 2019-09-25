using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unam.CoHu.Libreria.Controller;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

public partial class General_DetalleTitulo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string param = this.Request.QueryString["item"];
            if (!string.IsNullOrEmpty(param))
            {
                int idtitulo = 0;
                Int32.TryParse(param, out idtitulo);
                CargarTitulo(idtitulo);
            }
            else {
                this.bAutor.Visible = false;
                this.bEditor.Visible = false;                
                this.bTema.Visible = false;
                this.bContenido.Visible = false;
                this.bColofon.Visible = false;
                this.bObservaciones.Visible = false;
                this.bCiudad.Visible = false;
                this.bCualidades.Visible = false;
                this.bEdicion.Visible = false;
                this.bUFFYL.Visible = false;
                this.bUIIFL.Visible = false;
                this.ImagenTitulo.Visible = false;
                this.ImagenPdf.Visible = false;
                this.ImageVirtual.Visible = false;
                this.ImageOnline.Visible = false;
            }
        }
    }

    private void CargarTitulo(int idTitulo) {
        LibreriaController titulosBD = new LibreriaController();
        TituloLibreriaView titulo = titulosBD.CargarPorId(idTitulo);
        string stringDummy = string.Empty;
        if (titulo != null)
        {
            this.Title = titulo.Titulo;
            this.HiddenId.Value = titulo.IdTitulo.ToString();
            this.ImagenTitulo.ImageUrl = titulo.RutaArchivo;
            this.LabelTitulo.Text = string.Format("{0} (<i>{1}</i>) , {2}", titulo.Titulo, titulo.TituloOriginal, titulo.AnioPublicacion);
            if (titulo.Autor != null)
            {
                stringDummy = (!string.IsNullOrEmpty(titulo.Autor.NombreLatin)) ? titulo.Autor.NombreLatin : titulo.Autor.NombreGriego;
                //this.LabelAutor.Text = string.Format("{0} ({1})", titulo.Autor.NombreEspanol, stringDummy);
                this.LabelAutor.Text = string.Format("{0}", titulo.Autor.NombreEspanol);
                this.bAutor.Visible = (!string.IsNullOrEmpty(titulo.Autor.NombreEspanol));
            }
            else {
                this.LabelAutor.Text = "";
                this.bAutor.Visible = false;
            }

            if (titulo.Editor != null)
            {
                this.LabelEditor.Text = string.Format("{0} ", titulo.Editor.Nombre);
                this.bEditor.Visible = (!string.IsNullOrEmpty(titulo.Editor.Nombre));
            }
            else{
                this.bEditor.Visible = false;
            }

            //this.LabelPaginas.Text = string.Format(" Paginas: {0}  {1}", titulo.Paginas, titulo.Medidas);
            this.LabelTema.Text = titulo.Tema;
            this.bTema.Visible= (!string.IsNullOrEmpty(titulo.Tema));            
            this.LabelContenido.Text = titulo.Contenido;
            this.bContenido.Visible = (!string.IsNullOrEmpty(titulo.Contenido));
            this.LabelColofon.Text = titulo.Colofon;
            this.bColofon.Visible = (!string.IsNullOrEmpty(titulo.Colofon));
            this.LabelObservaciones.Text = titulo.Observaciones;
            this.bObservaciones.Visible = (!string.IsNullOrEmpty(titulo.Observaciones));
            this.LabelCiudad.Text = (titulo.Ciudad != null) ? titulo.Ciudad.Descripcion : string.Empty;                        
            this.bCiudad.Visible = (!string.IsNullOrEmpty((titulo.Ciudad != null) ? (titulo.Ciudad.Descripcion) : string.Empty));
            this.LabelCualidades.Text = titulo.Cualidades;
            this.bCualidades.Visible = (!string.IsNullOrEmpty(titulo.Cualidades));

            if (titulo.NumeroEdicion > 0)
            {
                this.LabelEdicion.Text = string.Format("<b>Edición:</b> {0} <sup><u>a</u></sup>.", titulo.NumeroEdicion);
                this.bEdicion.Visible = true;
            }
            else {                
                this.LabelEdicion.Text = " - ";
                this.bEdicion.Visible = false;
            }
            
            //this.LabelEdicion.Text = string.Format("{0}{1}{2}", LibreriaController.ObtenerNumeroEdicion(titulo.NumeroEdicion), (titulo.NumeroReimpresion > 0 ? ",":string.Empty), LibreriaController.ObtenerNumeroReimpresion(titulo.NumeroReimpresion));
            this.Label_UffYL.Text = titulo.UffYL;
            this.bUFFYL.Visible = (!string.IsNullOrEmpty(titulo.UffYL));
            this.Label_UiiFL.Text = titulo.UiiFL;
            this.bUIIFL.Visible = (!string.IsNullOrEmpty(titulo.UiiFL));
            this.bFlagLatin.Visible = (titulo.IsLatin | titulo.IsGriego);
            this.RadioIsLatin.Checked= titulo.IsLatin;
            this.RadioIsGriego.Checked = titulo.IsGriego;

            if (titulo.DetalleIsbn != null && titulo.DetalleIsbn.Count > 0)
            {
                foreach (Isbn item in titulo.DetalleIsbn)
                {
                    string edicion = (item.Edicion > 0) ? ", " + item.Edicion + "<sup><u>a</u></sup>" + " ed." : "";
                    string reedicion = (item.Reedicion > 0) ? ", "+item.Reedicion + "<sup><u>a</u></sup>" + " reed." : "";
                    string reimp = (item.Reimpresion > 0) ? ", "+item.Reimpresion + "<sup><u>a</u></sup>" + " reimpr." : "";
                    string ediciones = edicion + reedicion + reimp;

                    if (item.IdDescripcion > 0)
                    {
                        
                        this.LabelIsbn.Text += item.ClaveIsbn + " (" + item.DescripcionVersion + ediciones + ")" + "<br/>";
                    }
                    else
                    {
                        this.LabelIsbn.Text += item.ClaveIsbn + (ediciones.Length> 0 ? "("+ediciones+")": "") + "<br/>";
                    }
                }
                this.LabelIsbn.Visible = true;
            }
            else {
                this.LabelIsbn.Text = string.Empty;
                this.LabelIsbn.Visible = false;
            }

            if (titulo.DetalleResponsables != null && titulo.DetalleResponsables.Count > 0)
            {
                ResponsableTituloView resp = titulo.DetalleResponsables[0];
                foreach (ResponsableTituloDetail item in resp.Responsables)
                {
                    this.LabelResponsables.Text += string.Format("{0}  ({1}) {2}", item.NombreCompletoResponsable, item.TipoFuncion, "<br/>");
                }
                this.LabelResponsables.Visible = true;
            }
            else {
                this.LabelResponsables.Text = string.Empty;
                this.LabelResponsables.Visible = false;
            }

            if (!string.IsNullOrEmpty(titulo.UrlPdf)) {
                this.ImagenPdf.Visible = true;
                this.ImagenPdf.Attributes.Add("data-url", titulo.UrlPdf);
                this.ImagenPdf.Attributes.Add("onclick", "onSelectedResource('pdf','" + titulo.UrlPdf + "')");
            }
            if (!string.IsNullOrEmpty(titulo.UrlVirtual)) {
                this.ImageVirtual.Visible = true;
                this.ImageVirtual.Attributes.Add("data-url", titulo.UrlVirtual);
                this.ImageVirtual.Attributes.Add("onclick", "onSelectedResource('virtual','" + titulo.UrlVirtual + "')");
            }
            if (!string.IsNullOrEmpty(titulo.UrlOnline)){                
                this.ImageOnline.Visible = true;
                this.ImageOnline.Attributes.Add("data-url", titulo.UrlOnline);
                this.ImageOnline.Attributes.Add("onclick", "onSelectedResource('online','"+ titulo.UrlOnline + "')");
            }
        }
        else {
            this.ImagenTitulo.Visible = false;
            this.ImagenPdf.Visible = false;
            this.ImageVirtual.Visible = false;
            this.ImageOnline.Visible = false;
        }
    }

    protected void ButtonReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/");
    }
}