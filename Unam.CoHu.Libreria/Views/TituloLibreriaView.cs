using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.Views
{
    [Serializable]
    public class TituloLibreriaView : TituloLibreria
    {
        public TituloLibreriaView() {

        }

        public TituloLibreriaView(int idTitulo, string idEditor, string idResponsable, string idAutor, string tituloOriginal, int anioPublicacion, string idSerie) : base(idTitulo, idEditor, idResponsable, idAutor, tituloOriginal, anioPublicacion, idSerie)
        {
        }

        //public Isbn CatalogoIsbn { get; set; }
        public List<Isbn> DetalleIsbn { get; set; }
        public Ciudad Ciudad { get; set; }
        public Editor Editor { get; set; }
        public Autor Autor { get; set; }
        public Serie Serie { get; set; }
        public List<ResponsableTituloView> DetalleResponsables { get; set; }

    }
}
