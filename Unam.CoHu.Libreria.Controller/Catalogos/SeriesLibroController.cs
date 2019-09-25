using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO;
using Unam.CoHu.Libreria.Model;

namespace Unam.CoHu.Libreria.Controller.Catalogos
{
    public class SeriesLibroController
    {
        public SerieDb _SerieBd = null;

        public SeriesLibroController()
        {
            _SerieBd = new SerieDb();
        }

        public int Actualizar(Serie serie)
        {
            int rowsAffected = 0;
            try
            {
                if (serie != null)
                {
                    rowsAffected = _SerieBd.Update(serie, null);
                    _SerieBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Registrar(Serie serie)
        {
            int rowsAffected = 0;
            try
            {
                if (serie != null)
                {
                    rowsAffected = _SerieBd.Insert(serie, null);
                    _SerieBd.CloseConnection();
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
                int rowsAffected = _SerieBd.Delete(idKey, null);
                _SerieBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Serie> BuscarPor(string descripcion, int? top)
        {
            try
            {
                List<Serie> retorno = _SerieBd.SelectBy(null, descripcion, descripcion, top);
                _SerieBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Serie> CargarTodos()
        {
            try
            {
                List<Serie> retorno = _SerieBd.SelectAll(null);
                _SerieBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Serie CargarPorId(string id)
        {
            try
            {
                Serie retorno = _SerieBd.Select(id);
                _SerieBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
