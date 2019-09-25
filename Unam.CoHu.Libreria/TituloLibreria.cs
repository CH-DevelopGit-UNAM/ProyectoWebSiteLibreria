using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class TituloLibreria
    {
        public TituloLibreria() {
        }

        public TituloLibreria(int idTitulo, string idEditor, string idResponsable, string idAutor, string tituloOriginal, int anioPublicacion, string idSerie)
        {
            this.IdTitulo = idTitulo;
            this.IdEditor = idEditor;
            this.IdResponsableDetalle = idResponsable;
            this.IdAutor = idAutor;
            this.TituloOriginal = tituloOriginal;
            this.AnioPublicacion = anioPublicacion;
            this.IdSerie = idSerie;
        }



        public TituloLibreria(int idTitulo, string idEditor, string idResponsable, string idAutor, string tituloOriginal, int anioPublicacion, string idSerie, bool isGriego, bool isLatin)
        {
            this.IdTitulo = idTitulo;
            this.IdEditor = idEditor;
            this.IdResponsableDetalle = idResponsable;
            this.IdAutor = idAutor;
            this.TituloOriginal = tituloOriginal;
            this.AnioPublicacion = anioPublicacion;
            this.IdSerie = idSerie;
            this.IsGriego = isGriego;
            this.IsLatin = isLatin;
        }

        public int IdTitulo { get; set; }
        public string IdIsbn { get; set; }
        public string IdCiudad { get; set; }
        public string IdEditor { get; set; }
        public string IdResponsableDetalle { get; set; }
        public string IdAutor { get; set; }
        public string TituloOriginal { get; set; }
        public string Titulo { get; set; }
        public string Edicion { get; set; }
        public int AnioPublicacion { get; set; }
        public string Paginas { get; set; }
        public string Medidas { get; set; }
        public string Serie500 { get; set; }
        public string Contenido { get; set; }
        public string Cualidades { get; set; }
        public string Colofon { get; set; }
        public string Tema { get; set; }
        public string Secundarias { get; set; }
        public string UffYL { get; set; }
        public string UiiFL { get; set; }
        public string Observaciones { get; set; }
        public string IdSerie { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public bool IsNovedad { get; set; }
        public string UrlPdf { get; set; }
        public string UrlVirtual { get; set; }
        public string UrlOnline { get; set; }
        public int NumeroEdicion { get; set; }
        public int NumeroReimpresion   { get; set; }
        public bool IsGriego { get; set; }
        public bool IsLatin { get; set; }
    }
}
