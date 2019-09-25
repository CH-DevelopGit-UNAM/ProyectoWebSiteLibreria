using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.Security
{
    public class SegModulo
    {
        public SegModulo() { }

        public int IdModulo { get; set; }
        public int IdModuloSGU { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdModuloPadre { get; set; }
        public int? IdModuloPadreSGU { get; set; }
        public string Icono { get; set; }
        public string Pagina { get; set; }
        public int Orden { get; set; }
        public bool IsProtegino { get; set; }
        public string Clave { get; set; }
        public string ClaveBusquedaMod { get; set; }
        public int CountChildren { get; set; }
    }
}
