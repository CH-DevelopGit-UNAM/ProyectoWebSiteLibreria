using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.Cohu.Libreria.WinForm.Model
{
    public class Titulo
    {
        public Titulo() {

        }
        public string Coleccion { get; set; }
        public string DescripcionTitulo { get; set; }
        public int IdTitulo { get; set; }
        public string TipoTitulo { get; set; }
        public int Edicion { get; set; }        
        public string UrlPdf { get; set; }

    }
}
