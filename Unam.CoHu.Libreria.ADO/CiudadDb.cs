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
using Unam.CoHu.Libreria.Model.Views;

namespace Unam.CoHu.Libreria.ADO
{
    public class CiudadDb:DbContext
    {
        public CiudadDb() : base() { }
        public CiudadDb(string cnnStrName) : base(cnnStrName) { }
        public CiudadDb(SqlConnection connection) : base(connection) { }

        public const string PREFIJO_CLAVE = "CTY";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;

        public int Insert(Ciudad param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO Cat_Ciudad (id_ciudad,Descripcion) ";
            query = query + " VALUES (@idCiudad,@descripcion); ";

            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_ciudad) AS id_ciudad FROM Cat_Ciudad ; ", System.Data.CommandType.Text, null, null);
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
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(CiudadDb.PREFIJO_CLAVE, CiudadDb.SEPARADOR_CLAVE, consecutivo, CiudadDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    SqlParameter param1 = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = nuevaClave };
                    SqlParameter param2 = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.Descripcion) ? DBNull.Value : (object) param.Descripcion.Trim()) };

                    List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2 };
                    int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
                    
                    if (result > 0)
                        param.IdCiudad = nuevaClave;

                    return result;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Nuevo consecutivo '{1}'", maxId, nuevaClave));
                }

            }
        }

        public int Update(Ciudad param, SqlTransaction transaccion)
        {
            string query = " UPDATE Cat_Ciudad SET Descripcion=@descripcion ";
            query = query + " WHERE id_ciudad = @idCiudad ; ";            
            SqlParameter param1 = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = (String.IsNullOrEmpty(param.Descripcion) ? DBNull.Value : (object) param.Descripcion.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdCiudad.Trim() };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(string idKey, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Cat_Ciudad WHERE id_ciudad = @idCiudad";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10,  Value = idKey.Trim() };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }


        public Ciudad SelectPaginacion(string idKey, ref Paginacion paginacion)
        {
            Ciudad objeto = null;
            List<Ciudad> lista = SelectPaginacionBy(idKey, null, ref paginacion);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }

        public List<Ciudad> SelectPaginacionByDescripcion(string descripcion, ref Paginacion paginacion)
        {
            return SelectPaginacionBy(null, descripcion, ref paginacion);
        }

        public List<Ciudad> SelectPaginacionAll(ref Paginacion paginacion)
        {
            return SelectPaginacionBy(null, null, ref paginacion);
        }

        public List<Ciudad> SelectPaginacionBy(string idKey, string descripcion, ref Paginacion paginacion)
        {
            List<Ciudad> lista = null;
            SqlParameter[] parametros = new SqlParameter[6];
            string query = "spPaginacionCiudades ";
            if (paginacion == null)
            {
                paginacion = new Paginacion() { FilasPorPagina = 1, PaginaActual = 1 };
            }
            else
            {
                paginacion.PaginaActual = (paginacion.PaginaActual <= 0) ? 1 : paginacion.PaginaActual;
            }
            parametros[0] = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = string.IsNullOrEmpty(idKey) ? null : idKey.Trim(), IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(descripcion) ? descripcion : descripcion.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@PageNumber", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginaActual };
            parametros[3] = new SqlParameter() { ParameterName = "@RowspPage", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasPorPagina };
            parametros[4] = new SqlParameter() { ParameterName = "@numberPages", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginasTotales };
            parametros[5] = new SqlParameter() { ParameterName = "@rowsCount", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasTotales };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            if (reader.HasRows)
            {
                lista = new List<Ciudad>();
                while (reader.Read())
                {
                    Ciudad objeto = new Ciudad();
                    objeto.IdCiudad = reader["id_ciudad"].ToString();
                    objeto.Descripcion = reader["Descripcion"].ToString();
                    lista.Add(objeto);
                }
            }
            reader.NextResult();
            reader.Close();
            paginacion.PaginasTotales = (int)parametros[4].Value;
            paginacion.FilasTotales = (int)parametros[5].Value;
            paginacion.PaginaActual = (paginacion.PaginasTotales <= 0) ? 0 : paginacion.PaginaActual;

            return lista;
        }


        public Ciudad Select(string idKey)
        {
            Ciudad objeto = null;
            List<Ciudad> lista = SelectBy(idKey, null, null);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }

        public List<Ciudad> SelectByDescripcion(string descripcion, int? top)
        {
            return SelectBy(null, descripcion, top);
        }

        public List<Ciudad> SelectAll(int? top)
        {
            return SelectBy(null, null, top);
        }

        public List<Ciudad> SelectBy(string idKey, string descripcion, int? top)
        {
            List<Ciudad> lista = null;
            /*
            string query = "spBusquedaCiudades ";            
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = string.IsNullOrEmpty(idKey)? null: idKey, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object) descripcion, IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object)top.Value, IsNullable = true };
            */
            string query = "SELECT {0} id_ciudad, Descripcion ";
            query = query + " FROM Cat_Ciudad ";
            query = String.Format(query, ((top != null) ? " TOP(" + top.Value.ToString() + ") " : string.Empty));
            query = query + " WHERE (id_ciudad = @idCiudad OR @idCiudad IS NULL) AND (Descripcion LIKE @descripcion OR @descripcion IS NULL)  ";            
            query = query + " ORDER BY id_ciudad ";

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = string.IsNullOrEmpty(idKey) ? DBNull.Value : (object)idKey.Trim() };
            parametros[1] = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = string.IsNullOrEmpty(descripcion) ? DBNull.Value : (object)("%" + descripcion.Trim() + "%") };            

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.Text, parametros, null);

            if (reader.HasRows)
            {
                lista = new List<Ciudad>();
                while (reader.Read())
                {
                    Ciudad objeto = new Ciudad();                   
                    objeto.IdCiudad = reader["id_ciudad"].ToString();
                    objeto.Descripcion = reader["Descripcion"].ToString();                   
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }        
    }
}
