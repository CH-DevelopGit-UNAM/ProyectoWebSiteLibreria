using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Autor
    {
        public Autor() {
        }

        public Autor(string idAutor,string nombreLatin)
        {
            this.IdAutor = idAutor;
            this.NombreLatin = nombreLatin;
        }

        public Autor(string idAutor, string nombreLatin, string nombreGriego, string nombreEspanol)
        {
            this.IdAutor = idAutor;
            this.NombreLatin = nombreLatin;
            this.NombreGriego = nombreGriego;
            this.NombreEspanol = nombreEspanol;
        }


        public string IdAutor { get; set; }
        public string NombreLatin { get; set; }
        public string NombreGriego { get; set; }
        public string NombreEspanol { get; set; }
    }
}
