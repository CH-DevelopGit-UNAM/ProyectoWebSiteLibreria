using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model;


public partial class NovedadesCarousel : System.Web.UI.UserControl
{

    private List<Noticia> _Noticias = null;

    protected void Page_Init() {
        _Noticias = LoadCarousel();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RenderCarousel(_Noticias);
    }

    protected void LinkUpdateSource_Click(object sender, EventArgs e)
    {
        List<Noticia> noticias = ReLoadCarousel();
        RenderCarousel(noticias);
    }

    protected List<Noticia> LoadCarousel() {
        //if (ConfiguracionSitio.Noticias == null || ConfiguracionSitio.Noticias.Count<=0) {
        //    try
        //    {
        //        //ConfiguracionSitio.Noticias = LoaderNoticias.GetNoticias(ConfiguracionSitio.ObtenerVariableConfig("PathBaseFilesCarousel"));
        //        ConfiguracionSitio.Noticias = LoaderNoticias.GetNoticias();
        //    }
        //    catch (Exception ex)
        //    {

        //    }            
        //}        
        ConfiguracionSitio.Noticias = LoaderNoticias.GetNoticias();
        return ConfiguracionSitio.Noticias;
    }

    protected List<Noticia> ReLoadCarousel()
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

    protected void RenderCarousel(List<Noticia> noticias) {
        if (noticias != null && noticias.Count > 0 )
        {            
            this.DivNoticias.InnerHtml = LoaderNoticias.RenderCarousel(noticias, new int?(12000));
        }
        else {            
            string url = VirtualPathUtility.ToAbsolute("~/");
            noticias = new List<Noticia>();
            noticias.Add(new Noticia() { Id = 0, Titulo = "Acropolis", Descripcion = "No existen novedades a mostrar", RutaImagen = "/Images/Acropolis-Restoration.jpg", Url = url });
            noticias.Add(new Noticia() { Id = 1, Titulo = "Acropolis", Descripcion = "No existen novedades a mostrar", RutaImagen = "/Images/Acropolis.jpg", Url= url });

            this.DivNoticias.InnerHtml = LoaderNoticias.RenderCarousel(noticias, new int?(12000));
        }
        
    }

}