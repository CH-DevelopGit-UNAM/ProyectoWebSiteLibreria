using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Controller.Enums
{
    public enum TipoOperacion
    {
        Agregar = 0,
        Editar = 1,
        Eliminar = 2,
        SoloLectura = 3,
        Invalido = 4
    }

    public enum TipoMensaje
    {
        Error,
        Info,
        Success,
        Warning
    }

    public enum TipoBusqueda {
        Todos = 0,
        Filtrado = 1,
        ClavePrimaria = 2
    }

    public enum TipoReportePublicacion {
        Anio = 0,
        Autor = 1,
        TipoTexto = 2,
        Traductor = 3,
        Edicion = 4,
        Editorial = 5
    }
}
