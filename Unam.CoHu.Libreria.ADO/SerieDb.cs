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
    public class SerieDb : DbContext
    {
        public SerieDb() : base() { }
        public SerieDb(string cnnStrName) : base(cnnStrName) { }
        public SerieDb(SqlConnection connection) : base(connection) { }

        public const string PREFIJO_CLAVE = "SER";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;

        public int Insert(Serie param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO CAT_SERIE (id_serie,Nom_serie_lat,Nom_serie_gre) ";
            query = query + " VALUES (@idSerie,@serieLatin,@serieGriego); ";

            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_serie) AS ID_RESP FROM CAT_SERIE ; ", System.Data.CommandType.Text, null, null);
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
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(SerieDb.PREFIJO_CLAVE, SerieDb.SEPARADOR_CLAVE, consecutivo, SerieDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    SqlParameter param1 = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = nuevaClave };
                    SqlParameter param2 = new SqlParameter() { ParameterName = "@serieLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = param.NombreLatin.Trim() };
                    SqlParameter param3 = new SqlParameter() { ParameterName = "@serieGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.NombreGriego) ? DBNull.Value : (object) param.NombreGriego.Trim()) };

                    List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3 };
                    int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);

                    if (result > 0)
                        param.IdSerie = nuevaClave;

                    return result;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Nuevo consecutivo '{1}'", maxId, nuevaClave));
                }

            }            
        }

        public int Update(Serie param, SqlTransaction transaccion)
        {
            string query = " UPDATE CAT_SERIE SET Nom_serie_lat = @serieLatin, Nom_serie_gre = @serieGriego ";
            query = query + " WHERE id_serie = @idSerie ; ";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@serieLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = param.NombreLatin.Trim() };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@serieGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.NombreGriego) ? DBNull.Value : (object) param.NombreGriego.Trim()) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdSerie };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(string idKey, SqlTransaction transaccion)
        {
            string query = "DELETE FROM CAT_SERIE WHERE id_serie = @idSerie";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = idKey };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }

        public Serie Select(string idKey)
        {
            Serie objeto = null;
            List<Serie> lista = SelectBy(idKey, null, null, null);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }
        
        public List<Serie> SelectAll(int? top)
        {
            return SelectBy(null, null, null, top);
        }

        public List<Serie> SelectBy(string idKey, string nombreLatin, string nombreGriego, int? top)
        {
            List<Serie> lista = null;            
            string query = "SELECT {0} id_serie,Nom_serie_lat,Nom_serie_gre ";
            query = query + " FROM [CAT_SERIE] ";
            query = String.Format(query, ((top != null) ? " TOP(" + top.Value.ToString() + ") " : string.Empty));            
            query = query + " WHERE (id_serie = @idSerie OR @idserie IS NULL) ";
            query = query + " AND ( (Nom_serie_lat LIKE @serieLatin  OR @serieLatin IS NULL) OR (Nom_serie_gre LIKE @serieGriego OR @serieGriego IS NULL) ) ";
            query = query + " ORDER BY id_serie ";

            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = string.IsNullOrEmpty(idKey) ? DBNull.Value : (object)idKey };
            parametros[1] = new SqlParameter() { ParameterName = "@serieLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = string.IsNullOrEmpty(nombreLatin) ? DBNull.Value : (object)("%" + nombreLatin.Trim() + "%") };
            parametros[2] = new SqlParameter() { ParameterName = "@serieGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = string.IsNullOrEmpty(nombreGriego) ? DBNull.Value : (object)("%" + nombreGriego.Trim() + "%") };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.Text, parametros, null);

            if (reader.HasRows)
            {
                lista = new List<Serie>();
                while (reader.Read())
                {
                    Serie objeto = new Serie();
                    objeto.IdSerie = reader["id_serie"].ToString();
                    objeto.NombreLatin = reader["Nom_serie_lat"].ToString();
                    objeto.NombreGriego = reader["Nom_serie_gre"].ToString();                    
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }
        
    }
}
