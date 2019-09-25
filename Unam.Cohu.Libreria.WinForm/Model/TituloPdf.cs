using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.Cohu.Libreria.WinForm.Model
{
    public class TituloPdf: Archivos
    {
        public TituloPdf() {
        }

        
        public string Query { get; set; }
        public string MensajeCopia { get; set; }
        public string MensajeSQL { get; set; }
        public string UrlPdfAnterior { get; set; }
        public string NombrePdf { get; set; }
        public bool IsSeleccionado { get; set; }
        public bool IsInBD { get; set; }
        public bool IsArchivoCopiado { get; set; }
        public bool IsEjecutadoSQL { get; set; }
    }
}
