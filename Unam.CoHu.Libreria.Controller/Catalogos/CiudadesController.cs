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
    public class CiudadesController
    {
        public CiudadDb _CiudadBd = null;

        public CiudadesController() {
            _CiudadBd = new CiudadDb();
        }

        public int Actualizar(Ciudad ciudad)
        {
            int rowsAffected = 0;
            try
            {
                if (ciudad != null)
                {
                    rowsAffected = _CiudadBd.Update(ciudad, null);
                    _CiudadBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Registrar(Ciudad ciudad)
        {
            int rowsAffected = 0;
            try
            {
                if (ciudad != null)
                {
                    rowsAffected = _CiudadBd.Insert(ciudad, null);
                    _CiudadBd.CloseConnection();
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
                int rowsAffected = _CiudadBd.Delete(idKey, null);
                _CiudadBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ciudad> BuscarCiudadPor(string descripcion, int? top)
        {
            try
            {
                List<Ciudad>  retorno = _CiudadBd.SelectBy(null, descripcion, top);
                _CiudadBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Ciudad> CargarTodos()
        {
            try
            {
                List<Ciudad> retorno = _CiudadBd.SelectAll(null);
                _CiudadBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }                

        public Ciudad CargarPorId(string id)
        {
            try
            {
                Ciudad retorno = _CiudadBd.Select(id);
                _CiudadBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<Ciudad> PaginarCiudadPorNombre(string descripcion, ref Paginacion paginacion)
        {
            try
            {
                List<Ciudad> retorno = _CiudadBd.SelectPaginacionByDescripcion(descripcion, ref paginacion);
                _CiudadBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Ciudad> PaginarCiudades(ref Paginacion paginacion)
        {
            try
            {
                List<Ciudad> retorno = _CiudadBd.SelectPaginacionAll(ref paginacion);
                _CiudadBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Ciudad PaginarCiudadPorId(string id, ref Paginacion paginacion)
        {
            try
            {
                Ciudad retorno = _CiudadBd.SelectPaginacion(id, ref paginacion);
                _CiudadBd.CloseConnection();
                return retorno;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
