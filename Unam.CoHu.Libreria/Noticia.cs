using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Noticia
    {
        public int Id{ get; set; }
        public string Titulo { get; set; }

        public string DescripcionCorta
        {
            get {
                string description = String.Empty;
                string newString = String.Empty;
                if (String.IsNullOrEmpty(this._Descripcion)==false) {
                    newString = this._Descripcion.Replace("\r", "").Replace("\n", "").Replace("\t","").TrimStart().TrimEnd();
                    if (newString.Length > 100)
                    {                        
                        description = newString.Substring(0,99) + "...";
                    }
                    else {                        
                        description = newString.Substring(0, this._Descripcion.Length-1) ;
                    }
                }
                return description;
            }
        }

        private string _Descripcion ;
        public string Descripcion {
            get { return this._Descripcion;  }
            set { this._Descripcion = value; }
        }

        public string RutaImagen{ get; set; }

        public string RutaNotaCompleta { get; set; }

        public string Url { get; set; }

    }
}
