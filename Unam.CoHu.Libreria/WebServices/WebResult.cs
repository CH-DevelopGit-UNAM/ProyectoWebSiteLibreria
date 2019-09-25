using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.WebServices
{
    [DataContract]
    public class WebResult
    {
        public WebResult() {
        }
        
        [DataMember]
        public bool IsProcesado { get; set; }

        [DataMember]
        public bool HasException { get; set; }

        [DataMember]
        public string MensajeResultado { get; set; }

        [DataMember]
        public object Resultado { get; set; }

        [DataMember]
        public string ResultadoJson { get; set; }
        
    }
}
