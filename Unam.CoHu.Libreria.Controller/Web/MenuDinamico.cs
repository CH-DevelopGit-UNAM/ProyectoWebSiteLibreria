using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Unam.CoHu.Libreria.ADO.Security;
using Unam.CoHu.Libreria.Model.Security;

namespace Unam.CoHu.Libreria.Controller.Security
{
    public class MenuDinamico
    {
        public static List<Opcion> ObtenerMenuSGU() {
            List<Opcion> opciones = null;
            try
            {
                ModuloContext moduloBD = new ModuloContext("cnnSGU");
                
                    List<SegModulo> modulos= moduloBD.SelectAll(0);
                    opciones = new List<Opcion>();
                    if (modulos!=null) {
                        foreach (var modulo in modulos)
                        {
                            Opcion op = new Opcion();
                            op.IdOpcion = modulo.IdModuloSGU;
                            op.IdOpcionPadre = modulo.IdModuloPadreSGU;
                            op.NombreOpcion = modulo.Nombre;
                            op.Descripcion = modulo.Descripcion;
                            op.Comando = modulo.Pagina;
                            op.Orden = modulo.Orden;
                            op.HasRows = (modulo.CountChildren > 0) ?true : false;
                            opciones.Add(op);
                        }                
                }
                return opciones;
            }
            catch (Exception ex) {
                throw new Exception("No se pudo cargar el menú");
            }
        }

        public static List<MenuItem> ObtenerMenuSGUDataSource() {
            List<Opcion> opciones = null;
            List<MenuItem> listaMenus = null;
            try
            {
                opciones = MenuDinamico.ObtenerMenuSGU();
                if (opciones != null)
                {
                    listaMenus = AsignarMenuItems(opciones);
                }
                else
                {
                    listaMenus = new List<MenuItem>();
                }                
                return listaMenus;
            }
            catch (Exception e)
            {
                throw new Exception("Ocurrio un error al cargar los datos del Menu.");
            }
        }

        public static List<Opcion> ObtenerMenuRawDataSource() {
            List<Opcion> opciones = null;            
            try
            {
                OpcionContext opcionBD = new OpcionContext();
                opciones = opcionBD.SelectAll(null);
                return opciones;
            }
            catch (Exception e)
            {
                throw new Exception("Ocurrio un error al cargar el origen de datos del Menu.");
            }
        }

        public static List<MenuItem> ObtenerMenuDataSource()
        {            
            List<Opcion> opciones = null;
            List<MenuItem> listaMenus = null;
            try
            {
                OpcionContext opcionBD = new OpcionContext();
                opciones = opcionBD.SelectAll(null);
                listaMenus = AsignarMenuItems(opciones);
                return listaMenus;
            }
            catch (Exception e)
            {
                throw new Exception("Ocurrio un error al cargar los datos del Menu.");
            }
        }

        private static List<MenuItem> AsignarMenuItems(List<Opcion> opciones)
        {
            List<MenuItem> listaMenus = new List<MenuItem>();
            List<int> indices = new List<int>();
            int indice = 0;

            foreach (Opcion item in opciones)
            {
                if (item.IdOpcionPadre == null)
                {
                    MenuItem menuRaiz = new MenuItem();
                    menuRaiz.NavigateUrl = item.Comando;
                    menuRaiz.Text = item.NombreOpcion;
                    menuRaiz.ToolTip = item.Descripcion;
                    menuRaiz.Value = item.IdOpcion.ToString();
                    listaMenus.Add(menuRaiz);
                    indices.Add(indice);
                }

            }


            //Itero sobre la lista de menus raices
            for (int i = 0; i < listaMenus.Count; i++)
            {
                MenuItem menu = listaMenus[i];
                int idMenu = -1;
                Int32.TryParse(menu.Value, out idMenu);
                //System.Diagnostics.Debug.WriteLine( String.Format("Menu raiz ID={0}", idMenu));

                for (int j = 0; j < opciones.Count; j++)
                {
                    Opcion item = opciones[j];
                    //System.Diagnostics.Debug.WriteLine(String.Format("\t opcion IdPadre= {0}", item.IdOpcionPadre));
                    //Si el id padre es igual que el id hijo, entonces agrego
                    if (idMenu == item.IdOpcionPadre)
                    {
                        MenuItem subMenu = new MenuItem();
                        subMenu.NavigateUrl = item.Comando;
                        subMenu.Text = item.NombreOpcion;
                        subMenu.ToolTip = item.Descripcion;
                        subMenu.Value = item.IdOpcion.ToString();
                        menu.ChildItems.Add(subMenu);
                    }
                    else //Si no, entonces debería de iterar sobre sus submenus
                    {
                        AsignarHijosMenuItem(menu, item);
                    }
                }
            }

            return listaMenus;
        }

        private static void AsignarHijosMenuItem(MenuItem menu, Opcion item)
        {
            for (int k = 0; k < menu.ChildItems.Count; k++)
            {
                //Obtener subitems del menu Raiz
                MenuItem subItem = menu.ChildItems[k];
                int idOpcionSubitem = -1;
                Int32.TryParse(subItem.Value, out idOpcionSubitem);
                if (idOpcionSubitem == item.IdOpcionPadre)
                {
                    MenuItem subMenu2 = new MenuItem();
                    subMenu2.NavigateUrl = item.Comando;
                    subMenu2.Text = item.NombreOpcion;
                    subMenu2.ToolTip = item.Descripcion;
                    subMenu2.Value = item.IdOpcion.ToString();
                    subItem.ChildItems.Add(subMenu2);
                }
                else
                {
                    AsignarHijosMenuItem(subItem, item);
                }
            }
        }

        public static XmlDocument GenerarDocumentoXMLMenu(string nombreNodoRaiz, string nombreSubNodos)
        {
            try
            {
                XmlDocument documento = new XmlDocument();
                XmlDeclaration xmlDeclaracion = documento.CreateXmlDeclaration("1.0", "utf-8", null);
                XmlElement nodoRaiz = documento.CreateElement(nombreNodoRaiz);
                documento.InsertBefore(xmlDeclaracion, documento.DocumentElement);
                documento.AppendChild(nodoRaiz);

                List<MenuItem> items = ObtenerMenuDataSource();

                for (int i = 0; i < items.Count; i++)
                {
                    XmlElement nodoNivel1 = documento.CreateElement(nombreSubNodos);
                    nodoNivel1.SetAttribute("NavigateUrl", items[i].NavigateUrl);
                    nodoNivel1.SetAttribute("Text", items[i].Text);
                    nodoNivel1.SetAttribute("ToolTip", items[i].ToolTip);
                    nodoNivel1.SetAttribute("Value", items[i].Value);

                    System.Diagnostics.Debug.WriteLine(nodoNivel1);

                    string tab = "\t";
                    AsignarHijosXML(items[i], nodoNivel1, documento, tab, nombreSubNodos);

                    nodoRaiz.AppendChild(nodoNivel1);
                    //documento.AppendChild(nodoNivel1);
                }

                System.Diagnostics.Debug.WriteLine(documento);
                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AsignarHijosXML(MenuItem menu, XmlElement nodoAAgregar, XmlDocument documento, string tab, string nombreSubNodos)
        {
            tab = tab + "\t";
            for (int i = 0; i < menu.ChildItems.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine(tab + menu.ChildItems[i]);
                XmlElement nodoNivelInterno = documento.CreateElement(nombreSubNodos);
                nodoNivelInterno.SetAttribute("NavigateUrl", menu.ChildItems[i].NavigateUrl);
                nodoNivelInterno.SetAttribute("Text", menu.ChildItems[i].Text);
                nodoNivelInterno.SetAttribute("ToolTip", menu.ChildItems[i].ToolTip);
                nodoNivelInterno.SetAttribute("Value", menu.ChildItems[i].Value);
                nodoAAgregar.AppendChild(nodoNivelInterno);
                AsignarHijosXML(menu.ChildItems[i], nodoNivelInterno, documento, tab, nombreSubNodos);
            }
        }

        public static string RenderMenuItems(List<MenuItem> datasource)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter stWriter = new System.IO.StringWriter(sb);
            System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stWriter);
            foreach (var item in datasource)
            {
                _RenderMenu(item, htmlWriter);
                if (item.ChildItems != null && item.ChildItems.Count > 0)
                {
                    htmlWriter.Write("</ul>\r");
                    htmlWriter.Write("</li>\r");
                }
            }
            htmlWriter.Flush();
            htmlWriter.Close();
            //System.Diagnostics.Debug.WriteLine(sb.ToString());
            return sb.ToString();
        }

        private static void _RenderMenu(MenuItem item, HtmlTextWriter writer)
        {
            if (item != null)
            {

                if (item.ChildItems != null && item.ChildItems.Count > 0)
                {
                    writer.Write("<li class=\"dropdown\">\r");
                    writer.Write($"<a href=\"javascript: void(0);\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">{item.Text}  <span class=\"caret\"></span></a>\r");
                    writer.Write("<ul class=\"dropdown-menu\">\r");
                }
                else
                {
                    writer.Write($"<li><a href=\"#\">{item.Text}</a></li>\r");
                }

                foreach (MenuItem subItem in item.ChildItems)
                {
                    _RenderMenu(subItem, writer);
                    if (subItem.ChildItems != null && subItem.ChildItems.Count > 0)
                    {
                        writer.Write("</ul>\r");
                        writer.Write("</li>\r");
                    }
                }
            }
        }
    }
}
