using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [DataContract]
    public class Empleado
    {
        public Empleado() {
        }
        [DataMember]
        public int IdRegistro{ get; set; }
        [DataMember]
        public int IdEmpleado { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string Estatus { get; set; }
        [DataMember]
        public DateTime FechaAlta{ get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string ApellidoPaterno { get; set; }
        [DataMember]
        public string ApellidoMaterno { get; set; }
        [DataMember]
        public string NombreCompleto { get; set; }
    }
}