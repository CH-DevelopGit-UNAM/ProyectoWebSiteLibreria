using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.ADO.Enumerations
{
    public enum TipoComando
    {
        Query = 0,
        StoreProcedure = 1
    }

    public enum TipoFiltroConsulta {
        Or = 0,
        And = 1
    }
}
