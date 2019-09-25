using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO;
using Unam.CoHu.Libreria.Model;

namespace Unam.CoHu.Libreria.Controller.Catalogos
{
    public class IsbnController
    {
        public IsbnDb _IsbnBd = null;

        public IsbnController()
        {
            _IsbnBd = new IsbnDb();
        }

        public int Actualizar(Isbn isbn)
        {
            int rowsAffected = 0;
            try
            {
                if (isbn != null)
                {
                    rowsAffected = _IsbnBd.Update(isbn, null);
                    _IsbnBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Registrar(Isbn isbn)
        {
            int rowsAffected = 0;
            try
            {
                if (isbn != null)
                {
                    rowsAffected = _IsbnBd.Insert(isbn, null);
                    _IsbnBd.CloseConnection();
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
                int rowsAffected = _IsbnBd.Delete(idKey, null);
                _IsbnBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Isbn> BuscarPor(string isbn, string descripcion, int? top)
        {
            try
            {
                List<Isbn> retorno = _IsbnBd.SelectBy(null, isbn, descripcion, null, null, top);
                _IsbnBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Isbn> CargarTodos()
        {
            try
            {
                List<Isbn> retorno = _IsbnBd.SelectAll(null);
                _IsbnBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Isbn CargarPorId(string id)
        {
            try
            {
                Isbn retorno = _IsbnBd.Select(id);
                _IsbnBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
