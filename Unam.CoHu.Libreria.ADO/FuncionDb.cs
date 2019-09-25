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
    public class FuncionDb : DbContext
    {
        public FuncionDb() : base() { }
        public FuncionDb(string cnnStrName) : base(cnnStrName) { }
        public FuncionDb(SqlConnection connection) : base(connection) { }

        public const string PREFIJO_CLAVE = "FUN";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;

        public int Insert(Funcion param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO Cat_func (id_funcion,Tipo_funcion) ";
            query = query + " VALUES (@idFuncion,@tipoFuncion); ";

            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_funcion) AS id_funcion FROM Cat_func ; ", System.Data.CommandType.Text, null, null);
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
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(FuncionDb.PREFIJO_CLAVE, FuncionDb.SEPARADOR_CLAVE, consecutivo, FuncionDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    SqlParameter param1 = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = nuevaClave };
                    SqlParameter param2 = new SqlParameter() { ParameterName = "@tipoFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.TipoFuncion) ? DBNull.Value : (object) param.TipoFuncion.Trim()) };

                    List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2 };
                    int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
                 
                    if (result > 0)
                        param.IdFuncion = nuevaClave;

                    return result;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Nuevo consecutivo '{1}'", maxId, nuevaClave));
                }

            }
        }

        public int Update(Funcion param, SqlTransaction transaccion)
        {
            string query = " UPDATE Cat_func SET Tipo_funcion=@tipoFuncion ";
            query = query + " WHERE id_funcion = @idFuncion ; ";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@tipoFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.TipoFuncion) ? DBNull.Value : (object) param.TipoFuncion.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdFuncion.Trim() };            

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(string idKey, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Cat_func WHERE id_funcion = @idFuncion";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = idKey.Trim() };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }

        public Funcion Select(string idKey)
        {
            Funcion objeto = null;
            List<Funcion> lista = SelectBy(idKey, null, null);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }

        public List<Funcion> SelectByNombre(string nombre, int? top)
        {
            return SelectBy(null, nombre, top);
        }

        public List<Funcion> SelectAll(int? top)
        {
            return SelectBy(null, null, top);
        }

        public List<Funcion> SelectBy(string idKey, string descripcion, int? top)
        {
            List<Funcion> lista = null;
            string query = " SELECT {0} id_funcion,Tipo_funcion ";
            query = query + " FROM [Cat_func]  ";
            query = String.Format(query, ((top != null) ? " TOP(" + top.Value.ToString() + ") " : string.Empty));
            query = query + " WHERE (id_funcion = @idFuncion OR @idFuncion IS NULL) ";
            query = query + " AND ( Tipo_funcion LIKE @tipoFuncion  OR @tipoFuncion IS NULL) ";
            query = query + " ORDER BY id_funcion ";

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = string.IsNullOrEmpty(idKey) ? DBNull.Value : (object)idKey.Trim() };
            parametros[1] = new SqlParameter() { ParameterName = "@tipoFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = string.IsNullOrEmpty(descripcion) ? DBNull.Value : (object)("%" + descripcion.Trim() + "%") };            
            
            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.Text, parametros, null);

            if (reader.HasRows)
            {
                lista = new List<Funcion>();
                while (reader.Read())
                {
                    Funcion objeto = new Funcion();
                    objeto.IdFuncion = reader["id_funcion"].ToString();
                    objeto.TipoFuncion = reader["Tipo_funcion"].ToString();
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }
        
    }
}
