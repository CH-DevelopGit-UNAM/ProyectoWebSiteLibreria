using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.Cohu.Libreria.WinForm.Model
{
    public class TituloGuardado: Titulo
    {
        public TituloGuardado() { }

        public string Query { get; set; }
        public Guid GuidGenerado { get; set; }
        public string Extension { get; set; }
        public string MensajeArchivo { get; set; }
        public string MensajeSQL { get; set; }
        public string ArchivoAnterior { get; set; }
        public bool IsProcesado { get; set; }
        public bool IsArchivoGenerado { get; set; }
        public bool IsEjecutadoSQL { get; set; }

    }
}
