using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    // [Resp_detalle]
    [Serializable]
    public class ResponsableTitulo
    {
        public ResponsableTitulo()
        {
        }

        public ResponsableTitulo(string idRespDetalle )
        {
            this.IdResponsableDetalle = idRespDetalle;
        }

        public ResponsableTitulo(string idRespDetalle, string idResponsable, int? ordenFuncion )
        {
            this.IdResponsableDetalle = idRespDetalle;
            this.IdResponsable = idResponsable;
            this.OrdenFuncion = ordenFuncion;
        }

        public ResponsableTitulo(string idRespDetalle, string idResponsable, string idFuncion, string descripcionFuncion, int? ordenFuncion)
        {
            this.IdResponsableDetalle = idRespDetalle;
            this.IdResponsable = idResponsable;
            this.IdFuncion = idFuncion;
            this.DescripcionFuncion = descripcionFuncion;
            this.OrdenFuncion = ordenFuncion;            
        }

        //[id_titulo_resp]
        public string IdResponsableDetalle { get; set; }
        public string IdResponsable { get; set; }
        public string IdFuncion { get; set; }
        public string DescripcionFuncion { get; set; }
        public int? OrdenFuncion { get; set; }
    }
}