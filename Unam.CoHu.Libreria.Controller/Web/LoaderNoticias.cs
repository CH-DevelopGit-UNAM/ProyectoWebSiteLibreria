using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

namespace Unam.CoHu.Libreria.Controller.Web
{
    public class LoaderNoticias
    {

        private static string IdCarouselControl = "ContenedorCarousel";

        public static List<Noticia> GetNoticias()
        {
            List<Noticia> items = new List<Noticia>();
            LibreriaController libreria = new LibreriaController();
            List<TituloLibreriaView> novedades = null;
            string urlKey = ConfiguracionSitio.ObtenerVariableConfig("PathDetalleTitulo");            
            try
            {
                string url = System.Web.VirtualPathUtility.ToAbsolute(urlKey);                
                novedades = libreria.CargarTodos(new bool?(true), null, false, null);
                if (novedades != null && novedades.Count > 0)
                {
                    foreach (TituloLibreriaView titulo in novedades)
                    {
                        Noticia obj = new Noticia();
                        obj.Id = titulo.IdTitulo;
                        obj.Titulo = titulo.Titulo;
                        obj.Descripcion = titulo.TituloOriginal;
                        obj.RutaImagen = titulo.RutaArchivo;
                        obj.RutaNotaCompleta = "";
                        obj.Url = string.Format("{0}?item={1}",url, obj.Id);
                        items.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }

        public static List<Noticia> GetNoticias(List<TituloLibreriaView> novedades)
        {
            List<Noticia> items = new List<Noticia>();
            string urlKey = ConfiguracionSitio.ObtenerVariableConfig("PathDetalleTitulo");
            try
            {
                string url = System.Web.VirtualPathUtility.ToAbsolute(urlKey);
                if (novedades != null && novedades.Count > 0)
                {
                    foreach (TituloLibreriaView titulo in novedades)
                    {
                        Noticia obj = new Noticia();
                        obj.Id = titulo.IdTitulo;
                        obj.Titulo = titulo.Titulo;
                        obj.Descripcion = titulo.TituloOriginal;
                        obj.RutaImagen = titulo.RutaArchivo;
                        obj.RutaNotaCompleta = "";
                        obj.Url = string.Format("{0}?item={1}", url, obj.Id);
                        items.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }

        public static List<Noticia> GetNoticias(string pathFileXmlCarousel) {
            List<Noticia> items = new List<Noticia>();
            string pathFull =  System.Web.HttpContext.Current.Server.MapPath(pathFileXmlCarousel);            
            int intDummy = 0;

            try
            {
                if (File.Exists(pathFull))
                {
                    XmlDocument documento = new XmlDocument();
                    documento.Load(pathFull);
                    XmlNodeList listaNodes = documento.SelectNodes("/Noticias/Noticia");
                    foreach (XmlNode subnode in listaNodes)
                    {
                        intDummy = 0;
                        Noticia obj = new Noticia();
                        Int32.TryParse(subnode.Attributes["Id"].Value, out intDummy);
                        obj.Id = intDummy;
                        obj.Titulo = subnode.Attributes["Titulo"].Value;
                        obj.RutaImagen = subnode.Attributes["Imagen"].Value;
                        obj.RutaNotaCompleta = subnode.Attributes["NoticiaCompleta"].Value;
                        if (subnode.HasChildNodes)
                        {
                            XmlNode nodoDescripcion = subnode.SelectSingleNode("Descripcion");
                            obj.Descripcion = nodoDescripcion.InnerText;
                        }
                        items.Add(obj);
                    }
                }

            }
            catch (Exception ex)
            {
                //throw ex;                
            }            
            return items;
        }

        public static string RenderCarousel(List<Noticia> noticias, int? interval) {
            string html = String.Empty;
            int itemsCount = 0;
            int index = 0;            
            XmlDocument documento = new XmlDocument();

            if (noticias != null && noticias.Count > 0)
            {
                itemsCount = noticias.Count;

                // Div principal
                XmlElement contenedorRaiz = documento.CreateElement("div");
                contenedorRaiz.SetAttribute("id", IdCarouselControl);
                contenedorRaiz.SetAttribute("class", "carousel slide img-responsive");
                if (interval != null) {
                    if (interval.Value > 5000)
                    {
                        contenedorRaiz.SetAttribute("data-interval", interval.Value.ToString());
                    }
                    else {
                        contenedorRaiz.SetAttribute("data-interval", "5000");
                    }
                    
                }
                contenedorRaiz.SetAttribute("data-ride", "carousel");
                contenedorRaiz.SetAttribute("style", "width: 100%; height: auto; margin-top: 30px;");

                // indicadores
                XmlElement indicadorContenedor = documento.CreateElement("ol");
                indicadorContenedor.SetAttribute("class", "carousel-indicators");
                indicadorContenedor.SetAttribute("style", "margin-bottom:-15px;");
                contenedorRaiz.AppendChild(indicadorContenedor);

                // indicadores items
                for (int i = 0; i < itemsCount; i++)
                {
                    XmlElement indicadorItem = documento.CreateElement("li");
                    indicadorItem.SetAttribute("data-target", "#" + IdCarouselControl);
                    indicadorItem.SetAttribute("data-slide-to", i.ToString());
                    if (i == 0)
                    {
                        indicadorItem.SetAttribute("class", "active");
                    }
                    indicadorContenedor.AppendChild(indicadorItem);
                }

                // Items Noticias
                XmlElement contenedorNoticias = documento.CreateElement("div");
                contenedorNoticias.SetAttribute("class", "carousel-inner carouselContenedor");
                contenedorNoticias.SetAttribute("role", "listbox");

                // noticias
                foreach (Noticia item in noticias)
                {
                    // contenedor 
                    XmlElement contenedorNoticia = documento.CreateElement("div");
                    if (index == 0)
                    {
                        contenedorNoticia.SetAttribute("class", "active item");
                    }
                    else
                    {
                        contenedorNoticia.SetAttribute("class", "item");
                    }
                    contenedorNoticia.SetAttribute("style", "height: 100%;min-height:100%;");

                    XmlElement noticia = documento.CreateElement("div");
                    noticia.SetAttribute("class", "center text-center");
                    noticia.SetAttribute("style", "height: 100%;");

                    XmlElement centrarImagen = documento.CreateElement("div");
                    centrarImagen.SetAttribute("class", "carouselContenedorImagen");

                    XmlElement imagen = documento.CreateElement("img");
                    imagen.SetAttribute("src", item.RutaImagen);
                    //imagen.SetAttribute("width", "400");
                    imagen.SetAttribute("alt", item.Titulo);
                    imagen.SetAttribute("class", "carouselImagen img-responsive");
                    //imagen.SetAttribute("style", "height:470px;margin:auto;");
                    centrarImagen.AppendChild(imagen);

                    XmlElement contenedorDescripcion = documento.CreateElement("div");
                    contenedorDescripcion.SetAttribute("class", "carousel-caption navbar-fixed-bottom carouselContenedorTitulo");

                    XmlElement ahref = documento.CreateElement("a");
                    ahref.SetAttribute("href", item.Url);

                    XmlElement h4 = documento.CreateElement("h4");
                    h4.InnerXml = item.Titulo;
                    XmlElement p = documento.CreateElement("p");
                    p.InnerXml = item.DescripcionCorta;


                    ahref.AppendChild(h4);
                    ahref.AppendChild(p);

                    contenedorDescripcion.AppendChild(ahref);                    

                    //noticia.AppendChild(imagen);
                    noticia.AppendChild(centrarImagen);

                    noticia.AppendChild(contenedorDescripcion);



                    // Control descripcion
                    XmlElement controlDescripcion = documento.CreateElement("div");
                    controlDescripcion.SetAttribute("class", "navbar-fixed-bottom carouselFondoTitulo");

                    controlDescripcion.InnerText = "";
                    controlDescripcion.InnerXml = "";

                    noticia.AppendChild(controlDescripcion);



                    contenedorNoticia.AppendChild(noticia);
                    contenedorNoticias.AppendChild(contenedorNoticia);

                    


                    index++;
                }
                contenedorRaiz.AppendChild(contenedorNoticias);
                // Control Prev
                XmlElement controlPrevio = documento.CreateElement("a");
                controlPrevio.SetAttribute("class", "carousel-control left");
                controlPrevio.SetAttribute("href", "#" + IdCarouselControl);
                controlPrevio.SetAttribute("data-slide", "prev");

                XmlElement iconoPrevio = documento.CreateElement("span");
                iconoPrevio.SetAttribute("class", "glyphicon glyphicon-chevron-left");
                controlPrevio.AppendChild(iconoPrevio);

                // Control Next
                XmlElement controlSiguiente = documento.CreateElement("a");
                controlSiguiente.SetAttribute("class", "carousel-control right");
                controlSiguiente.SetAttribute("href", "#" + IdCarouselControl);
                controlSiguiente.SetAttribute("data-slide", "next");

                XmlElement iconoSiguiente = documento.CreateElement("span");
                iconoSiguiente.SetAttribute("class", "glyphicon glyphicon-chevron-right");
                controlSiguiente.AppendChild(iconoSiguiente);

                contenedorRaiz.AppendChild(controlPrevio);
                contenedorRaiz.AppendChild(controlSiguiente);

               
                documento.AppendChild(contenedorRaiz);
            }
            return documento.OuterXml ;
        }

        public static string RenderMultipleItemsCarousel(List<Noticia> noticias, int? interval)
        {
            string html = String.Empty;
            int itemsCount = 0;
            int index = 0;
            XmlDocument documento = new XmlDocument();
            // Div principal
            XmlElement contenedorRaiz = documento.CreateElement("div");

            if (noticias != null && noticias.Count > 0)
            {
                itemsCount = noticias.Count;
                

                //// indicadores
                //XmlElement indicadorContenedor = documento.CreateElement("ol");
                //indicadorContenedor.SetAttribute("class", "carousel-indicators");
                //indicadorContenedor.SetAttribute("style", "margin-bottom:-15px;");
                //contenedorRaiz.AppendChild(indicadorContenedor);

                //// indicadores items
                //for (int i = 0; i < itemsCount; i++)
                //{
                //    XmlElement indicadorItem = documento.CreateElement("li");
                //    indicadorItem.SetAttribute("data-target", "#" + IdCarouselControl);
                //    indicadorItem.SetAttribute("data-slide-to", i.ToString());
                //    if (i == 0)
                //    {
                //        indicadorItem.SetAttribute("class", "active");
                //    }
                //    indicadorContenedor.AppendChild(indicadorItem);
                //}

                // noticias
                foreach (Noticia item in noticias)
                {
                    // contenedor 
                    XmlElement contenedorNoticia = documento.CreateElement("div");
                    contenedorNoticia.InnerText = "";
                    contenedorNoticia.SetAttribute("class", "flex-item");
                    contenedorNoticia.SetAttribute("data-src", item.RutaImagen);
                    contenedorNoticia.SetAttribute("data-title", item.Titulo.Replace("\"","").Replace("\'",""));
                    contenedorNoticia.SetAttribute("data-description-large", item.Descripcion.Replace("\"", "").Replace("\'", ""));
                    contenedorNoticia.SetAttribute("data-description-small", item.DescripcionCorta.Replace("\"", "").Replace("\'", ""));
                    contenedorNoticia.SetAttribute("data-url", item.Url);
                    contenedorNoticia.SetAttribute("data-id", item.Id.ToString());
                    contenedorRaiz.AppendChild(contenedorNoticia);
                    index++;
                }
                //documento.AppendChild(contenedorRaiz);
            }
            //return documento.InnerXml;
            return contenedorRaiz.InnerXml;
        }

    }
}
