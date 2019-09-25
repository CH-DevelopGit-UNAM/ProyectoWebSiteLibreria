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
    public class IsbnDb : DbContext
    {
        public IsbnDb() : base() { }
        public IsbnDb(string cnnStrName) : base(cnnStrName) { }
        public IsbnDb(SqlConnection connection) : base(connection) { }

        public const string PREFIJO_CLAVE = "TIT";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;

        public int Insert(Isbn param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO Cat_ISBN (id_tit_isbn,ISBN,desc_ver,reimpresion,reedicion) ";
            query = query + " VALUES (@idIsbn,@isbn,@descripcion,@reimpresion,@reedicion); ";

            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_tit_isbn) AS ID_RESP FROM Cat_ISBN ; ", System.Data.CommandType.Text, null, null);
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
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(IsbnDb.PREFIJO_CLAVE, IsbnDb.SEPARADOR_CLAVE, consecutivo, IsbnDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    SqlParameter param1 = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = nuevaClave };
                    SqlParameter param2 = new SqlParameter() { ParameterName = "@isbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = param.ClaveIsbn.Trim() };
                    SqlParameter param3 = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.DescripcionVersion) ? DBNull.Value : (object) param.DescripcionVersion.Trim()) };
                    SqlParameter param4 = new SqlParameter() { ParameterName = "@reimpresion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.Reimpresion) ? DBNull.Value : (object) param.Reimpresion.Trim()) };
                    SqlParameter param5 = new SqlParameter() { ParameterName = "@reedicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.Reedicion) ? DBNull.Value : (object) param.Reedicion.Trim()) };

                    List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5 };
                    int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);                    

                    if (result > 0)
                        param.IdIsbn = nuevaClave;

                    return result;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Nuevo consecutivo '{1}'", maxId, nuevaClave));
                }

            }            
        }

        public int Update(Isbn param, SqlTransaction transaccion)
        {
            string query = " UPDATE Cat_ISBN SET ISBN = @isbn, desc_ver = @descripcion, reimpresion = @reimpresion, reedicion = @reedicion ";
            query = query + " WHERE id_tit_isbn = @idIsbn ; ";            
            SqlParameter param1 = new SqlParameter() { ParameterName = "@isbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = param.ClaveIsbn.Trim() };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.DescripcionVersion) ? DBNull.Value : (object) param.DescripcionVersion.Trim()) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@reimpresion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.Reimpresion) ? DBNull.Value : (object)param.Reimpresion.Trim()) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@reedicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.Reedicion) ? DBNull.Value : (object) param.Reedicion.Trim()) };
            SqlParameter param5 = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdIsbn.Trim() };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(string idKey, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Cat_ISBN WHERE id_tit_isbn = @idIsbn";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = idKey.Trim() };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }

        public Isbn Select(string idKey)
        {
            Isbn objeto = null;
            List<Isbn> lista = SelectBy(idKey, null, null, null, null, null);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }

        public List<Isbn> SelectByIsbn(string isbn)
        {
            return SelectBy(null,isbn,null,null,null, null);
        }

        public List<Isbn> SelectAll(int? top)
        {
            return SelectBy(null, null, null,null,null, top);
        }

        public List<Isbn> SelectBy(string idKey, string isbn, string descripcion, string reimpresion, string reedicion, int? top)
        {
            List<Isbn> lista = null;
            string filterDescription = (string.IsNullOrEmpty(descripcion) ? "AND" : "OR");
            StringBuilder query = new StringBuilder(string.Format("SELECT {0} id_tit_isbn,ISBN,desc_ver,reimpresion,reedicion ", (top != null)? " TOP(" + top.Value.ToString() + ") " : string.Empty));
            query.AppendLine (" FROM [Cat_ISBN] ");
            query.AppendLine(" WHERE (id_tit_isbn = @idIsbn OR @idIsbn IS NULL)  ");
            query.AppendLine(" 	    AND (ISBN LIKE @isbn OR @isbn IS NULL) ");
            query.AppendLine(" 	    AND (desc_ver LIKE @descripcion OR @descripcion IS NULL) ");
            //query.AppendLine(" 	    "+ filterDescription + " ( ");
            //query.AppendLine(" 	        (desc_ver LIKE @descripcion OR @descripcion IS NULL) ");
            //query.AppendLine("          AND (reimpresion LIKE @reimpresion OR @reimpresion IS NULL) ");
            //query.AppendLine("          AND (reedicion LIKE @reedicion OR @reedicion IS NULL) ");
            //query.AppendLine(" 	    ) ");
            query.AppendLine(" ORDER BY id_tit_isbn ");            

            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = string.IsNullOrEmpty(idKey) ? DBNull.Value : (object)idKey };
            parametros[1] = new SqlParameter() { ParameterName = "@isbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = string.IsNullOrEmpty(isbn) ? DBNull.Value : (object)("%" + isbn.Trim() + "%") };
            parametros[2] = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = string.IsNullOrEmpty(descripcion) ? DBNull.Value : (object)("%" + descripcion.Trim() + "%") };
            //parametros[3] = new SqlParameter() { ParameterName = "@reimpresion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = string.IsNullOrEmpty(reimpresion) ? DBNull.Value : (object)("%" + reimpresion + "%") };
            //parametros[4] = new SqlParameter() { ParameterName = "@reedicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10 , IsNullable = true, Value = string.IsNullOrEmpty(reedicion) ? DBNull.Value : (object)("%" + reedicion + "%") };            

            SqlDataReader reader = this.SeleccionarReaderArray(query.ToString(), CommandType.Text, parametros, null);

            if (reader.HasRows)
            {
                lista = new List<Isbn>();
                while (reader.Read())
                {
                    Isbn objeto = new Isbn();
                    objeto.IdIsbn = reader["id_tit_isbn"].ToString();
                    objeto.ClaveIsbn = reader["ISBN"].ToString();
                    objeto.DescripcionVersion = reader["desc_ver"].ToString();
                    objeto.Reimpresion = reader["reimpresion"].ToString();
                    objeto.Reedicion = reader["reedicion"].ToString();
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }
        
    }
}
