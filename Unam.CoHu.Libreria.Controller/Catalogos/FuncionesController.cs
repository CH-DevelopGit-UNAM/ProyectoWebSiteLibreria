using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO;
using Unam.CoHu.Libreria.Model;

namespace Unam.CoHu.Libreria.Controller.Catalogos
{
    public class FuncionesController
    {
        public FuncionDb _FuncionBd = null;

        public FuncionesController()
        {
            _FuncionBd = new FuncionDb();
        }

        public int Actualizar(Funcion funcion)
        {
            int rowsAffected = 0;
            try
            {
                if (funcion != null)
                {
                    rowsAffected = _FuncionBd.Update(funcion, null);
                    _FuncionBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Registrar(Funcion funcion)
        {
            int rowsAffected = 0;
            try
            {
                if (funcion != null)
                {
                    rowsAffected = _FuncionBd.Insert(funcion, null);
                    _FuncionBd.CloseConnection();
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
                int rowsAffected = _FuncionBd.Delete(idKey, null);
                _FuncionBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Funcion> BuscarPor(string descripcion, int? top)
        {
            try
            {
                List<Funcion> retorno = _FuncionBd.SelectByNombre(descripcion, top);
                _FuncionBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Funcion> CargarTodos()
        {
            try
            {
                List<Funcion> retorno = _FuncionBd.SelectAll(null);
                _FuncionBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Funcion CargarPorId(string id)
        {
            try
            {
                Funcion retorno = _FuncionBd.Select(id);
                _FuncionBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
