using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.Cohu.Libreria.WinForm.Model
{
    [Serializable]
    public class Archivos: Titulo
    {
        public Archivos() {

        }

        public bool IsArchivoConvertido { get; set; }
        public string ExtensionArchivo { get; set; }
        public string MensajeArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
    }
}
