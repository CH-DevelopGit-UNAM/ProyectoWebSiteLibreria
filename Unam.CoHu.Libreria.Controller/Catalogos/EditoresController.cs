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
    public class EditoresController
    {

        public EditorDb _EditorBd = null;

        public EditoresController()
        {
            _EditorBd = new EditorDb();
        }

        public int Actualizar(Editor editor)
        {
            int rowsAffected = 0;
            try
            {
                if (editor != null)
                {
                    rowsAffected = _EditorBd.Update(editor, null);
                    _EditorBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Registrar(Editor editor)
        {
            int rowsAffected = 0;
            try
            {
                if (editor != null)
                {
                    rowsAffected = _EditorBd.Insert(editor, null);
                    _EditorBd.CloseConnection();
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
                int rowsAffected = _EditorBd.Delete(idKey, null);
                _EditorBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Editor> BuscarPor(string descripcion, int? top)
        {
            try
            {
                List<Editor>  retorno = _EditorBd.SelectBy(null, descripcion, top);
                _EditorBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Editor> CargarTodos()
        {
            try
            {
                List<Editor> retorno = _EditorBd.SelectAll(null);
                _EditorBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Editor CargarPorId(string id)
        {
            try
            {
                Editor retorno = _EditorBd.Select(id);
                _EditorBd.CloseConnection();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
