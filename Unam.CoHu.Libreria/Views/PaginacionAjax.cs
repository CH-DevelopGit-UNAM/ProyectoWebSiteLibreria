using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.Views
{
    [Serializable]
    public class PaginacionAjax :Paginacion
    {
        public PaginacionAjax() { }

        public bool IsBusqueda { get; set; }
        public string TipoBusqueda { get; set; }
        public string Busqueda1 { get; set; }
        public string Busqueda2 { get; set; }

    }
}
