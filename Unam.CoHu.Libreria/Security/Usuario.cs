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
    public class Usuario
    {
        public Usuario()
        {
        }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public string UsuarioNombre { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Password{ get; set; }
        [DataMember]
        public bool Activo { get; set; }
        [DataMember]
        public int? IdUsuarioJefe { get; set; }
    }
}
