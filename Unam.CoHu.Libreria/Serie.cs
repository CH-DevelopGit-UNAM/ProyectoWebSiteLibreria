using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Serie
    {
        public Serie() { }

        public Serie(string idSerie, string nombreLatin) {
            this.IdSerie = idSerie;
            this.NombreLatin= nombreLatin;
        }

        public Serie(string idSerie, string nombreLatin, string nombreGriego) {
            this.IdSerie = idSerie;
            this.NombreLatin = nombreLatin;
            this.NombreGriego = nombreGriego;
        }

        public string IdSerie { get; set; }
        public string NombreLatin { get; set; }
        public string NombreGriego { get; set; }
    }
}