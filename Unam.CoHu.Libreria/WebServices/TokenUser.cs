using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.WebServices
{
    [DataContract]
    public class TokenUser
    {
        public TokenUser() { }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public DateTime UntilDate { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public bool IsTokenValid { get; set; }
        [DataMember]
        public bool IsUserAuthenticated { get; set; }
    }
}
