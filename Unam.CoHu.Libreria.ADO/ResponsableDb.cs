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
    public class ResponsableDb : DbContext
    {
        public ResponsableDb() : base() { }
        public ResponsableDb(string cnnStrName) : base(cnnStrName) { }
        public ResponsableDb(SqlConnection connection) : base(connection) { }
        public const string PREFIJO_CLAVE= "RES";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;

        public int Insert(Responsable param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO Cat_Responsable (id_resp,RFC,Nombres,Ape_pat,Ape_mat,Desc_Resp) ";
            query = query + " VALUES (@idResponsable, @rfc, @nombre, @apPaterno, @apMaterno, @descripcion); ";

            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_resp) AS ID_RESP FROM Cat_Responsable ; ", System.Data.CommandType.Text, null, null);
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
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(ResponsableDb.PREFIJO_CLAVE, ResponsableDb.SEPARADOR_CLAVE, consecutivo, ResponsableDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    SqlParameter param1 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = nuevaClave };
                    SqlParameter param2 = new SqlParameter() { ParameterName = "@rfc", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.Rfc) ? DBNull.Value : (object) param.Rfc.Trim()) };
                    SqlParameter param3 = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = param.Nombre.Trim() };
                    SqlParameter param4 = new SqlParameter() { ParameterName = "@apPaterno", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.ApellidoPaterno) ? DBNull.Value : (object) param.ApellidoPaterno.Trim()) };
                    SqlParameter param5 = new SqlParameter() { ParameterName = "@apMaterno", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.ApellidoMaterno) ? DBNull.Value : (object) param.ApellidoMaterno.Trim()) };
                    SqlParameter param6 = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Descripcion) ? DBNull.Value : (object) param.Descripcion.Trim()) };                    

                    List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5, param6 };
                    int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
                    if (result > 0)
                        param.IdResponsable = nuevaClave;

                    return result;                    
                }
                else
                {
                    throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Nuevo consecutivo '{1}'", maxId, nuevaClave));
                }
            }
        }

        public int Update(Responsable param, SqlTransaction transaccion)
        {
            string query = " UPDATE Cat_Responsable SET RFC = @rfc, Nombres = @nombre, Ape_pat = @apPaterno, Ape_mat = @apMaterno , Desc_Resp = @descripcion ";
            query = query + " WHERE id_resp = @idResponsable  ; ";            
            SqlParameter param1 = new SqlParameter() { ParameterName = "@rfc", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.Rfc) ? DBNull.Value : (object) param.Rfc.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = param.Nombre.Trim() };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@apPaterno", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.ApellidoPaterno) ? DBNull.Value : (object) param.ApellidoPaterno.Trim()) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@apMaterno", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.ApellidoMaterno) ? DBNull.Value : (object) param.ApellidoMaterno.Trim()) };
            SqlParameter param5 = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Descripcion) ? DBNull.Value : (object) param.Descripcion.Trim()) };            
            SqlParameter param6 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdResponsable };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5, param6 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(string idKey, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Cat_Responsable WHERE id_resp = @idResponsable";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = idKey.Trim() };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }

        public List<Responsable> SelectByNombre(string nombre, int? top)
        {
            return SelectBy(null, null, nombre, top);
        }

        public List<Responsable> SelectByRfc(string rfc, int? top)
        {
            return SelectBy(null, rfc, null, top);
        }

        public List<Responsable> SelectAll(int? top)
        {
            return SelectBy(null, null, null, top);
        }

        public List<Responsable> SelectBy(string idKey, string rfc, string nombre, int? top)
        {
            List<Responsable> lista = null;
            SqlParameter[] parametros = new SqlParameter[4];
            string query = "spBusquedaResponsables";
            parametros[0] = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = (idKey == null) ? null : idKey, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@rfc", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10, Value = (rfc == null) ? null : rfc.Trim(), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (nombre == null) ? null : nombre.Trim(), IsNullable = true };            
            parametros[3] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object)top.Value, IsNullable = true };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);            

            if (reader.HasRows)
            {
                lista = new List<Responsable>();
                while (reader.Read())
                {
                    Responsable objeto = new Responsable();
                    objeto.IdResponsable = reader["id_resp"].ToString();
                    objeto.Rfc = reader["RFC"].ToString();
                    objeto.Nombre = reader["Nombres"].ToString();
                    objeto.ApellidoPaterno = reader["Ape_pat"].ToString();
                    objeto.ApellidoMaterno = reader["Ape_mat"].ToString();
                    objeto.Descripcion = reader["Desc_Resp"].ToString();
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }        

    }
}
