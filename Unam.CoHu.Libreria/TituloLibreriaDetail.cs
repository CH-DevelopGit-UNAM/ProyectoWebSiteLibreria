using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class TituloLibreriaDetail : TituloLibreria
    {
        public TituloLibreriaDetail() :base(){
        }

        public TituloLibreriaDetail(int idTitulo, string idEditor, string idResponsable, string idAutor, string tituloOriginal, int anioPublicacion, string idSerie) : base(idTitulo, idEditor, idResponsable, idAutor, tituloOriginal, anioPublicacion, idSerie)
        {            
        }

        public TituloLibreriaDetail(int idTitulo, string idEditor, string idResponsable, string idAutor, string tituloOriginal, int anioPublicacion, string idSerie, bool isGriego, bool isLatin) : base(idTitulo, idEditor, idResponsable, idAutor, tituloOriginal, anioPublicacion, idSerie, isGriego, isLatin)
        {
        }

        //public Isbn ISBN2 { get; set; }
        //public Ciudad Ciudad { get; set; }
        //public Editor Editor { get; set; }        
        //public Autor Autor { get; set; }
        //public Serie Serie { get; set; }

        public int IdTituloIsbn { get; set; }
        public int IdFkTituloIsbn { get; set; }
        public int IdDescripcionIsbn  { get; set; }
        public string DescripcionIsbn { get; set; }
        public int ReImpresionIsbn { get; set; }
        public int ReedicionIsbn { get; set; }
        public int EdicionIsbn { get; set; }
        public string ISBN { get; set; }

        public string CiudadDescripcion { get; set; }
        public string NombreEditor { get; set; }
        public string IdFuncionResponsable { get; set; }
        public string TipoFuncionResponsable { get; set; }
        public string DescripcionFuncionResponsable { get; set; }
        public int? OrdenFuncionResponsable { get; set; }
        public string IdResponsableUnico { get; set; }
        public string RfcResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public string ApPaternoResponsable { get; set; }
        public string ApMaternoResponsable { get; set; }        
        //public string IdFuncionResponsable2 { get; set; }
        //public string FuncionResponsable2 { get; set; }
        public string AutorLatin { get; set; }
        public string AutorGriego { get; set; }
        public string AutorEspanol { get; set; }
        public string SerieLatin { get; set; }
        public string SerieGriego { get; set; }
    }
}
