using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.Security
{
    [Serializable]
    [DataContract]
    public class Opcion
    {
        public Opcion() { }
        [DataMember]
        public int IdOpcion { get; set; }
        [DataMember]
        public int? IdOpcionPadre { get; set; }
        [DataMember]
        public string NombreOpcion { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Comando { get; set; }
        [DataMember]
        public int Orden { get; set; }
        [DataMember]
        public bool HasRows { get; set; }
    }
}
