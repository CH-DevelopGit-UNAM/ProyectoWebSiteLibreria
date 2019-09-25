using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model;

public partial class UserControls_NovedadesCarousel_v2_0 : System.Web.UI.UserControl
{
    private List<Noticia> _Noticias = null;
    private bool _LoadCustom = false;

    public bool LoadCustom {
        get {
            return _LoadCustom;
        }
        set {
            _LoadCustom = value;
        }
    }

    public List<Noticia> Noticias
    {
        get
        {
            return _Noticias;
        }
        set
        {
            _Noticias = value;
        }
    }
        
    public UpdatePanel PanelNoticias {
        get {
            return this.UpdatePanelNoticias;
        }
    }

    protected void Page_Init()
    {
        if (!_LoadCustom) {
            _Noticias = LoadCarousel();
        }        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!_LoadCustom)
        {
            RenderCarousel(_Noticias);
        }        
    }

    protected void LinkUpdateSource_Click(object sender, EventArgs e)
    {
        List<Noticia> noticias = ReLoadCarousel();
        RenderCarousel(noticias);
    }

    public List<Noticia> LoadCarousel()
    {
        ConfiguracionSitio.Noticias = LoaderNoticias.GetNoticias();
        return ConfiguracionSitio.Noticias;
    }

    public List<Noticia> ReLoadCarousel()
    {
        try
        {
            //ConfiguracionSitio.Noticias = LoaderNoticias.GetNoticias(ConfiguracionSitio.ObtenerVariableConfig("PathBaseFilesCarousel"));
            ConfiguracionSitio.Noticias = LoaderNoticias.GetNoticias();
        }
        catch (Exception)
        {
            ConfiguracionSitio.Noticias = new List<Noticia>();
        }
        return ConfiguracionSitio.Noticias;
    }

    public  void RenderCarousel(List<Noticia> noticias)
    {
        if (noticias != null && noticias.Count > 0)
        {            
            this.DivNoticias.InnerHtml = LoaderNoticias.RenderMultipleItemsCarousel(noticias, new int?(12000));
        }
        else
        {
            string url = VirtualPathUtility.ToAbsolute("~/");
            noticias = new List<Noticia>();            
            noticias.Add(new Noticia() { Id = 0, Titulo = "Sin resultados", Descripcion = "No existen novedades a mostrar", RutaImagen = "/Images/Box_Empty_Circle.png", Url = url });
            this.DivNoticias.InnerHtml = LoaderNoticias.RenderMultipleItemsCarousel(noticias, new int?(12000));
        }                
    }    
}