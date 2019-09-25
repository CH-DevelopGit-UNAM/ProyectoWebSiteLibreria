using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Editor
    {
        public Editor() { }

        public Editor(string idEditor) {
            this.IdEditor = idEditor;            
        }

        public Editor(string idEditor, string nombre)
        {
            this.IdEditor = idEditor;
            this.Nombre = nombre;
        }

        public string IdEditor{ get; set; }
        public string Nombre { get; set; }
    }
}