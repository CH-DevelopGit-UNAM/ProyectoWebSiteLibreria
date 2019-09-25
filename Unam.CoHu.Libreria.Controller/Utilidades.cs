using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Unam.CoHu.Libreria.Controller.Enums;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model.Web;

namespace Unam.CoHu.Libreria.Controller
{
    public sealed class Utilidades
    {
        private Utilidades() {

        }

        public  static List<T> LimpiarNulosLista<T>(List<T> entrada, bool nullIfNoElements)
        {
            List<T> listaRetorno = null;
            if (entrada != null)
            {
                listaRetorno = new List<T>();
                foreach (T item in entrada)
                {
                    if (item != null)
                    {
                        listaRetorno.Add(item);
                    }
                }
                if (nullIfNoElements) {
                    if (listaRetorno.Count <= 0) {
                        listaRetorno = null;
                    }
                }
            }
            return listaRetorno;
        }

        public static TipoOperacion TipoOperacionFromInt(int tipoOperacion) {
            TipoOperacion tipoOp = TipoOperacion.Invalido;
            switch (tipoOperacion)
            {
                case (int)TipoOperacion.Agregar:
                    tipoOp = TipoOperacion.Agregar;
                    break;
                case (int)TipoOperacion.Editar:
                    tipoOp = TipoOperacion.Editar;
                    break;
                case (int)TipoOperacion.Eliminar:
                    tipoOp = TipoOperacion.Eliminar;
                    break;
                case (int)TipoOperacion.SoloLectura:
                    tipoOp = TipoOperacion.SoloLectura;
                    break;                
                default:
                    tipoOp = TipoOperacion.Invalido;
                    break;
            }
            return tipoOp;
        }

        public static void MostrarMensaje(string message, TipoMensaje tipoMensaje)
        {
            MostrarMensaje(message, tipoMensaje, null);
        }

        public static void MostrarMensaje(string message, TipoMensaje tipoMensaje, System.Web.UI.WebControls.Panel panel)
        {
            string function = "";        
            if (panel == null)
            {
                function = string.Format("message('{0}', '{1}');", HttpUtility.HtmlEncode(message).
                    Replace("\r\n", "<br />").Replace("\n", "<br />"), tipoMensaje.ToString());
            }
            else
            {
                function = string.Format("message('{0}', '{1}', '{2}');", HttpUtility.HtmlEncode(message.
                    Replace("\r\n", "<br />").Replace("\n", "<br />")), tipoMensaje.ToString(), panel.ClientID);
            }

            SitioScriptManager.RegistrarScript(function);
        }

        public static void LlenarDropDown<T>(DropDownList dropdown, IEnumerable<T> dataSource, string valueProperty, string textProperty, bool insertDefault, string valueDefault) {
            if (dropdown != null)
            {
                dropdown.DataSource = dataSource;
                dropdown.DataValueField = valueProperty;
                dropdown.DataTextField = textProperty;
                dropdown.DataBind();
                if (insertDefault) {
                    dropdown.Items.Insert(0, new ListItem("Seleccione una opción...", valueDefault));
                }
            }
        }
    }
}
