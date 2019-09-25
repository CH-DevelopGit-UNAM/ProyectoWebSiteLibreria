using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Ciudad
    {
        public Ciudad() { }

        public Ciudad(string idCiudad) {
            this.IdCiudad = idCiudad;
        }

        public Ciudad(string idCiudad, string descripcion) {
            this.IdCiudad = idCiudad;
            this.Descripcion = descripcion;
        }

        public string IdCiudad { get; set; }
        public string Descripcion { get; set; }
    }
}
