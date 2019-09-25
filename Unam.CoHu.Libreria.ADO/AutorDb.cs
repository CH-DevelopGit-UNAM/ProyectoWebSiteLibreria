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
    public class AutorDb:DbContext
    {
        public AutorDb() : base() { }
        public AutorDb(string cnnStrName) : base(cnnStrName) { }
        public AutorDb(SqlConnection connection) : base(connection) { }

        public const string PREFIJO_CLAVE = "AUT";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;

        public int Insert(Autor param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO Cat_Autor (id_Autor,Nombre_latin,Nombre_greek,Nombre_esp) ";
            query = query + " VALUES (@idAutor,@nombreLatin,@nombreGriego,@nombreEspanol); ";

            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_Autor) AS id_Autor FROM Cat_Autor ; ", System.Data.CommandType.Text, null, null);
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
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(AutorDb.PREFIJO_CLAVE, AutorDb.SEPARADOR_CLAVE, consecutivo, AutorDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    SqlParameter param1 = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = nuevaClave };
                    SqlParameter param2 = new SqlParameter() { ParameterName = "@nombreLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = (string.IsNullOrEmpty(param.NombreLatin) ? DBNull.Value : (object) param.NombreLatin.Trim()) };
                    SqlParameter param3 = new SqlParameter() { ParameterName = "@nombreGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = (string.IsNullOrEmpty(param.NombreGriego) ? DBNull.Value : (object)param.NombreGriego.Trim()) };
                    SqlParameter param4 = new SqlParameter() { ParameterName = "@nombreEspanol", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 100, IsNullable = true, Value = (string.IsNullOrEmpty(param.NombreEspanol) ? DBNull.Value : (object) param.NombreEspanol.Trim()) };

                    List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4 };
                    int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
                                        
                    if (result > 0)
                        param.IdAutor = nuevaClave;

                    return result;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Nuevo consecutivo '{1}'", maxId, nuevaClave));
                }

            }            
        }

        public int Update(Autor param, SqlTransaction transaccion)
        {
            string query = " UPDATE Cat_Autor SET Nombre_latin=@nombreLatin , Nombre_greek = @nombreGriego, Nombre_esp = @nombreEspanol ";            
            query = query + " WHERE id_Autor = @idAutor ; ";            
            SqlParameter param1 = new SqlParameter() { ParameterName = "@nombreLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.NombreLatin) ? DBNull.Value : (object) param.NombreLatin.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@nombreGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.NombreGriego) ? DBNull.Value : (object) param.NombreGriego.Trim()) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@nombreEspanol", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.NombreEspanol) ? DBNull.Value : (object) param.NombreEspanol.Trim()) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdAutor };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(string idKey, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Cat_Autor WHERE id_Autor =@idAutor";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = idKey.Trim() };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }


        public Autor SelectPaginacion(string idKey, ref Paginacion paginacion)
        {
            Autor objeto = null;
            List<Autor> lista = SelectPaginacionBy(idKey, null, null,null, ref paginacion);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }
        
        public List<Autor> SelectPaginacionAll(ref Paginacion paginacion)
        {
            return SelectPaginacionBy(null, null, null, null, ref paginacion);
        }

        public List<Autor> SelectPaginacionBy(string idKey, string nombreLatin, string nombreGriego, string nombreEsp, ref Paginacion paginacion)
        {
            List<Autor> lista = null;
            SqlParameter[] parametros = new SqlParameter[8];
            string query = "spPaginacionAutores";
            if (paginacion == null)
            {
                paginacion = new Paginacion() { FilasPorPagina = 1, PaginaActual = 1 };
            }
            else
            {
                paginacion.PaginaActual = (paginacion.PaginaActual <= 0) ? 1 : paginacion.PaginaActual;
            }
            parametros[0] = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = string.IsNullOrEmpty(idKey) ? null : idKey.Trim(), IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@nombreLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Value = (object)(string.IsNullOrEmpty(nombreLatin) ? nombreLatin : nombreLatin.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@nombreGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Value = (object)(string.IsNullOrEmpty(nombreGriego) ? nombreGriego : nombreGriego.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@nombreEspanol", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Value = (object)(string.IsNullOrEmpty(nombreEsp) ? nombreEsp : nombreEsp.Trim()), IsNullable = true };            
            parametros[4] = new SqlParameter() { ParameterName = "@PageNumber", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginaActual };
            parametros[5] = new SqlParameter() { ParameterName = "@RowspPage", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasPorPagina };
            parametros[6] = new SqlParameter() { ParameterName = "@numberPages", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginasTotales };
            parametros[7] = new SqlParameter() { ParameterName = "@rowsCount", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasTotales };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            string stringDummy = string.Empty;
            if (reader.HasRows)
            {
                lista = new List<Autor>();
                while (reader.Read())
                {
                    Autor objeto = new Autor();
                    objeto.IdAutor = reader["id_Autor"].ToString();
                    objeto.NombreLatin = reader["Nombre_latin"].ToString();
                    objeto.NombreGriego = reader["Nombre_greek"].ToString();
                    objeto.NombreEspanol = (reader["Nombre_esp"] != null ? reader["Nombre_esp"].ToString().Trim(): string.Empty);
                    lista.Add(objeto);
                }
            }
            reader.NextResult();
            reader.Close();
            paginacion.PaginasTotales = (int)parametros[6].Value;
            paginacion.FilasTotales = (int)parametros[7].Value;
            paginacion.PaginaActual = (paginacion.PaginasTotales <= 0) ? 0 : paginacion.PaginaActual;

            return lista;
        }



        public Autor Select(string idKey)
        {
            Autor objeto = null;
            List<Autor> lista = SelectBy(idKey, null, null, null,null);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }        

        public List<Autor> SelectAll(int? top)
        {
            return SelectBy(null, null, null, null, top);
        }

        public List<Autor> SelectBy(string idKey, string nombreLatin, string nombreGriego, string nombreEsp, int? top)
        {
            List<Autor> lista = null;
            SqlParameter[] parametros = new SqlParameter[5];
            string query = "spBusquedaAutores";
            parametros[0] = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = string.IsNullOrEmpty(idKey) ? null : idKey, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@nombreLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Value = (object)(string.IsNullOrEmpty(nombreLatin) ? nombreLatin : nombreLatin.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@nombreGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Value = (object)(string.IsNullOrEmpty(nombreGriego) ? nombreGriego : nombreGriego.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@nombreEspanol", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Value = (object)(string.IsNullOrEmpty(nombreEsp) ? nombreEsp : nombreEsp.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object)top.Value, IsNullable = true };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            string stringDummy = string.Empty;
            if (reader.HasRows)
            {
                lista = new List<Autor>();
                while (reader.Read())
                {                    
                    Autor objeto = new Autor();                    
                    objeto.IdAutor = reader["id_Autor"].ToString();
                    objeto.NombreLatin = reader["Nombre_latin"].ToString();
                    objeto.NombreGriego = reader["Nombre_greek"].ToString();
                    objeto.NombreEspanol = (reader["Nombre_esp"] != null ? reader["Nombre_esp"].ToString().Trim() : string.Empty);
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }       
    }
}
