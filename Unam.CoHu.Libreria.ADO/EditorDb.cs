using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO.Enumerations;
using Unam.CoHu.Libreria.ADO.General;
using Unam.CoHu.Libreria.Model;

namespace Unam.CoHu.Libreria.ADO
{
    public class EditorDb : DbContext
    {
        public EditorDb() : base() { }
        public EditorDb(string cnnStrName) : base(cnnStrName) { }
        public EditorDb(SqlConnection connection) : base(connection) { }

        public const string PREFIJO_CLAVE = "EDI";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;

        public int Insert(Editor param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO CAT_EDITOR (id_editor,Nombre) ";
            query = query + " VALUES (@idEditor,@nombre); ";

            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_editor) AS id_editor FROM CAT_EDITOR ; ", System.Data.CommandType.Text, null, null);
            string nuevaClave = String.Empty;
            bool isClaveGenerada = false;
            int consecutivo = UtilidadesADO.SepararClaveEntero(maxId, '-');

            if (consecutivo <= 0)
            {
                throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Consecutivo obtenido '{1}'", maxId, consecutivo));
            }
            else
            {
                consecutivo = consecutivo + 1;
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(EditorDb.PREFIJO_CLAVE, EditorDb.SEPARADOR_CLAVE, consecutivo, EditorDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    SqlParameter param1 = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = nuevaClave };
                    SqlParameter param2 = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Nombre) ? DBNull.Value : (object) param.Nombre.Trim()) };

                    List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2 };
                    int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);                    

                    if (result > 0)
                        param.IdEditor = nuevaClave;

                    return result;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Nuevo consecutivo '{1}'", maxId, nuevaClave));
                }

            }            
        }

        public int Update(Editor param, SqlTransaction transaccion)
        {
            string query = " UPDATE CAT_EDITOR SET Nombre=@nombre ";
            query = query + " WHERE id_editor = @idEditor ; ";            
            SqlParameter param1 = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Nombre) ? DBNull.Value : (object) param.Nombre.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdEditor.Trim() };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(string idKey, SqlTransaction transaccion)
        {
            string query = "DELETE FROM CAT_EDITOR WHERE id_editor = @idEditor";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = idKey.Trim() };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }

        public Editor Select(string idKey)
        {
            Editor objeto = null;
            List<Editor> lista = SelectBy(idKey, null, null);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }

        public List<Editor> SelectByNombre(string nombre)
        {
            return SelectBy(null, nombre, null);
        }

        public List<Editor> SelectAll(int? top)
        {
            return SelectBy(null, null, top);
        }

        public List<Editor> SelectBy(string idKey, string descripcion, int? top)
        {
            List<Editor> lista = null;            
            string query = "SELECT {0} id_editor,Nombre ";
            query = query + " FROM [CAT_EDITOR] ";
            query = String.Format(query, ((top != null) ? " TOP(" + top.Value.ToString() + ") " : string.Empty));
            query = query + " WHERE (id_editor = @idEditor OR @idEditor IS NULL) ";
            query = query + " AND (Nombre LIKE @nombre  OR @nombre IS NULL ) ";                        
            query = query + " ORDER BY id_editor ";

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = string.IsNullOrEmpty(idKey) ? DBNull.Value : (object)idKey.Trim() };
            parametros[1] = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = string.IsNullOrEmpty(descripcion) ? DBNull.Value : (object)("%" + descripcion.Trim() + "%") };            

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.Text, parametros, null);

            if (reader.HasRows)
            {
                lista = new List<Editor>();
                while (reader.Read())
                {
                    Editor objeto = new Editor();
                    objeto.IdEditor = reader["id_editor"].ToString();
                    objeto.Nombre= reader["Nombre"].ToString();
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }
        
    }
}
