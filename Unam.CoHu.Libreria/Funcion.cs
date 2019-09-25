using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Funcion
    {
        public Funcion() { }

        public Funcion(string idFuncion)
        {
            this.IdFuncion = idFuncion;
        }

        public Funcion(string idFuncion, string tipoFuncion)
        {
            this.IdFuncion = idFuncion;
            this.TipoFuncion = tipoFuncion;
        }

        public string IdFuncion { get; set; }
        public string TipoFuncion{ get; set; }
    }
}
