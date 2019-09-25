using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class ResponsableTituloDetail : ResponsableTitulo
    {
        public ResponsableTituloDetail()
        {
        }

        public ResponsableTituloDetail(string idRespDetalle):base(idRespDetalle)
        {
            this.IdResponsableDetalle = idRespDetalle;
        }

        public ResponsableTituloDetail(string idRespDetalle, string idResponsable, int? ordenFuncion) : base (idRespDetalle, idResponsable, ordenFuncion)
        {
           
        }

        public ResponsableTituloDetail(string idRespDetalle, string idResponsable, string idFuncion, string descripcionFuncion, int? ordenFuncion) : base(idRespDetalle, idResponsable, idFuncion, descripcionFuncion, ordenFuncion)
        {
            this.IdResponsableDetalle = idRespDetalle;
            this.IdResponsable = idResponsable;
            this.IdFuncion = idFuncion;
            this.DescripcionFuncion = descripcionFuncion;
            this.OrdenFuncion = ordenFuncion;
        }

        public string RfcResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public string ApPaternoResponsable { get; set; }
        public string ApMaternoResponsable { get; set; }
        public string TipoFuncion { get; set; }
        public string NombreCompletoResponsable { get {
                return string.Format("{0} {1} {2}", ApPaternoResponsable, ApMaternoResponsable, NombreResponsable);
            }
        }
    }

    [Serializable]
    public class ResponsableTituloView
    {
        public ResponsableTituloView()
        {
        }

        public ResponsableTituloView(string idRespDetalle)
        {
            this.IdResponsableDetalle = idRespDetalle;
        }

        public string IdResponsableDetalle { get; set; }
        public List<ResponsableTituloDetail> Responsables { get; set; }
    }

}
