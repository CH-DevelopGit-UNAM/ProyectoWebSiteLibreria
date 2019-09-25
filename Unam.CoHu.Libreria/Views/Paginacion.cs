using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.Views
{
    [Serializable]
    public class Paginacion
    {
        public Paginacion() { }

        public int PaginaActual;
        public int FilasPorPagina;
        public int PaginasTotales;
        public int FilasTotales;
        public bool IsNavigating;
    }
}