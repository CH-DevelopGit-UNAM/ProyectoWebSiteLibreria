using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [DataContract]
    public class Busqueda
    {
        public Busqueda() { }

        [DataMember]
        public string CampoBusqueda { get; set; }
        [DataMember]
        public string Guid { get; set; }
    }
}
