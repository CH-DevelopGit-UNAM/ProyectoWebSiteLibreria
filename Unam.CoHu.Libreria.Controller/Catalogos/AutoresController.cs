using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

namespace Unam.CoHu.Libreria.Controller.Catalogos
{
    public class AutoresController
    {
        
        public AutorDb _AutorBd = null;        

        public AutoresController() {                       
            _AutorBd = new AutorDb();
        }

        public int Actualizar(Autor autor)
        {
            int rowsAffected = 0;
            try
            {
                if (autor != null)
                {
                    rowsAffected = _AutorBd.Update(autor, null);
                    _AutorBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Registrar(Autor autor)
        {
            int rowsAffected = 0;
            try
            {
                if (autor != null)
                {
                    rowsAffected = _AutorBd.Insert(autor, null);
                    _AutorBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Borrar(string idKey)
        {
            try
            {
                int rowsAffected = _AutorBd.Delete(idKey, null);
                _AutorBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Autor> BuscarAutorPor(string descripcion, int? top)
        {
            try
            {
                List<Autor> autores = _AutorBd.SelectBy(null, descripcion, descripcion, descripcion, top);
                _AutorBd.CloseConnection();
                return autores;                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Autor> CargarTodos()
        {
            try
            {
                List <Autor> autores = _AutorBd.SelectAll(null); ;
                _AutorBd.CloseConnection();
                return autores;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Autor CargarPorId(string id)
        {
            try
            {
                Autor autor = _AutorBd.Select(id);
                _AutorBd.CloseConnection();
                return autor;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<Autor> PaginarAutorPor(string descripcion, ref Paginacion paginacion)
        {
            try
            {
                List<Autor> autores = _AutorBd.SelectPaginacionBy(null, descripcion, descripcion, descripcion, ref paginacion);
                _AutorBd.CloseConnection();
                return autores;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Autor> PaginarTodos(ref Paginacion paginacion)
        {
            try
            {
                List<Autor> autores = _AutorBd.SelectPaginacionAll(ref paginacion);
                _AutorBd.CloseConnection();
                return autores;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Autor PaginarPorId(string id, ref Paginacion paginacion)
        {
            try
            {
                Autor autor = _AutorBd.SelectPaginacion(id, ref paginacion);
                _AutorBd.CloseConnection();
                return autor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
