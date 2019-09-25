using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Responsable
    {
        public Responsable() {
        }

        public Responsable(string idResponsable, string nombre)
        {
            this.IdResponsable = idResponsable;
            this.Nombre = nombre;
        }

        public Responsable(string idResponsable, string rfc, string nombre)
        {
            this.IdResponsable = idResponsable;
            this.Rfc = rfc;
            this.Nombre = nombre;
        }

        public Responsable(string idResponsable, string rfc, string nombre, string apPaterno, string apMaterno, string descripcion)
        {
            this.IdResponsable = idResponsable;
            this.Rfc = rfc;
            this.Nombre = nombre;
            this.ApellidoPaterno = apPaterno;
            this.ApellidoMaterno = apMaterno;
        }

        public string IdResponsable { get; set; }
        public string Rfc { get; set; }
        public string Nombre{ get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Descripcion { get; set; }

        public string NombreCompleto { get {
                return string.Format("{0} {1} {2}", ApellidoPaterno, ApellidoMaterno, Nombre);
            }
        }

    }
}
