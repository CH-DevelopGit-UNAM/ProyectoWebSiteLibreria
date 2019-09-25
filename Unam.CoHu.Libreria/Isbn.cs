using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model
{
    [Serializable]
    public class Isbn
    {
        public Isbn() {
        }

        public Isbn(int idIsbn, string claveIsbn)
        {
            this.IdIsbn = idIsbn;
            this.ClaveIsbn = claveIsbn;            
        }

        public Isbn(int idIsbn, int idTitulo, string claveIsbn)
        {
            this.IdIsbn = idIsbn;
            this.IdTitulo = idTitulo;
            this.ClaveIsbn = claveIsbn;
        }

        public Isbn(int idIsbn, int idTitulo, string claveIsbn, int idDescripcion, string descripcion)
        {
            this.IdIsbn = idIsbn;
            this.IdTitulo = idTitulo;
            this.ClaveIsbn = claveIsbn;
            this.IdDescripcion = IdDescripcion;
            this.DescripcionVersion = descripcion;
        }

        public Isbn(int idIsbn, int idTitulo, string claveIsbn, int idDescripcion, string descripcion, int reimpresion, int reedicion)
        {
            this.IdIsbn = idIsbn;
            this.IdTitulo = idTitulo;
            this.ClaveIsbn = claveIsbn;
            this.IdDescripcion = idDescripcion;
            this.DescripcionVersion = descripcion;
            this.Reimpresion = reimpresion;
            this.Reedicion = reedicion;
        }

        public Isbn(int idIsbn, int idTitulo, string claveIsbn, int idDescripcion, string descripcion, int reimpresion, int reedicion, int edicion)
        {
            this.IdIsbn = idIsbn;
            this.IdTitulo = idTitulo;
            this.ClaveIsbn = claveIsbn;
            this.IdDescripcion = idDescripcion;
            this.DescripcionVersion = descripcion;
            this.Reimpresion = reimpresion;
            this.Reedicion = reedicion;
            this.Edicion = edicion;
        }

        public int IdIsbn { get; set; }
        public int IdTitulo{ get; set; }        
        public string ClaveIsbn { get; set; }
        public int IdDescripcion { get; set; }
        public string DescripcionVersion { get; set; }
        public int Reimpresion{ get; set; }
        public int Reedicion{ get; set; }
        public int Edicion { get; set; }
    }
}