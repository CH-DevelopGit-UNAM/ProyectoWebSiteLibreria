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
    public class ResponsableTituloDb : DbContext
    {
        public ResponsableTituloDb() : base() { }
        public ResponsableTituloDb(string cnnStrName) : base(cnnStrName) { }
        public ResponsableTituloDb(SqlConnection connection) : base(connection) { }
        public const string PREFIJO_CLAVE = "TIT";
        public const string SEPARADOR_CLAVE = "-";
        public const int LONGITUD_CLAVE = 10;


        public bool GenerarNuevaClave(ref string nuevaClave, ref string mensaje, SqlTransaction transaccion)
        {
            string maxId = this.SeleccionarEscalar<string>("SELECT MAX(id_titulo_resp) AS id_titulo_resp FROM Resp_detalle ; ", System.Data.CommandType.Text, null, transaccion);
            bool isClaveGenerada = false;
            int consecutivo = UtilidadesADO.SepararClaveEntero(maxId, '-');

            if (consecutivo <= 0)
            {
                mensaje = string.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Consecutivo obtenido '{1}'", maxId, consecutivo);
                isClaveGenerada = false;
            }
            else
            {
                consecutivo = consecutivo + 1;
                isClaveGenerada = UtilidadesADO.GenerarClaveConsecutiva(ResponsableTituloDb.PREFIJO_CLAVE, ResponsableTituloDb.SEPARADOR_CLAVE, consecutivo, ResponsableTituloDb.LONGITUD_CLAVE, ref nuevaClave);

                if (isClaveGenerada)
                {
                    mensaje = "Clave Generada";
                    isClaveGenerada = true;
                }
                else
                {
                    mensaje = string.Format("No se pudo generar el nuevo consecutivo para el registro. Clave Maxima '{0}', Consecutivo obtenido '{1}'", maxId, consecutivo);
                    isClaveGenerada = false;
                }
            }

            return isClaveGenerada;
        }

        public int Insert(ResponsableTitulo param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO Resp_detalle (id_titulo_resp,resp,func,desc_func,orden_func) ";
            query = query + " VALUES (@idRespDetalle, @idResponsable, @idFuncion, @descripcionFunc, @ordenFuncion); ";

            SqlParameter param1 = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdResponsableDetalle };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdResponsable) ? DBNull.Value : (object) param.IdResponsable) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdFuncion) ? DBNull.Value : (object) param.IdFuncion) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@descripcionFunc", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.DescripcionFuncion) ? DBNull.Value : (object) param.DescripcionFuncion.Trim()) };
            SqlParameter param5 = new SqlParameter() { ParameterName = "@ordenFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = ( param.OrdenFuncion == null ? DBNull.Value : (object) param.OrdenFuncion.Value ) };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5 };
            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Update(ResponsableTitulo paramPrevio, ResponsableTitulo paramNuevo, SqlTransaction transaccion)
        {
            string query = " UPDATE Resp_detalle SET resp = @idResponsable, func = @idFuncion, desc_func = @descripcionFunc, orden_func = @ordenFuncion ";
            query = query + " WHERE id_titulo_resp = @idRespDetalle AND resp = @idResponsable2 AND  func = @idFuncion2 AND desc_func = @descripcionFunc2  ; ";

            SqlParameter param1 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(paramNuevo.IdResponsable) ? DBNull.Value : (object) paramNuevo.IdResponsable.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(paramNuevo.IdFuncion) ? DBNull.Value : (object)paramNuevo.IdFuncion) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@descripcionFunc", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(paramNuevo.DescripcionFuncion) ? DBNull.Value : (object)paramNuevo.DescripcionFuncion.Trim()) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@ordenFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (paramNuevo.OrdenFuncion == null ? DBNull.Value : (object)paramNuevo.OrdenFuncion.Value) };

            SqlParameter param5 = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = paramPrevio.IdResponsableDetalle };
            SqlParameter param6 = new SqlParameter() { ParameterName = "@idResponsable2", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(paramPrevio.IdResponsable) ? DBNull.Value : (object) paramPrevio.IdResponsable.Trim()) };
            SqlParameter param7 = new SqlParameter() { ParameterName = "@idFuncion2", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(paramPrevio.IdFuncion) ? DBNull.Value : (object) paramPrevio.IdFuncion.Trim()) };
            SqlParameter param8 = new SqlParameter() { ParameterName = "@descripcionFunc2", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(paramPrevio.DescripcionFuncion) ? DBNull.Value : (object) paramPrevio.DescripcionFuncion.Trim()) };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5, param6, param7, param8 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(ResponsableTitulo param, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Resp_detalle WHERE id_titulo_resp = @idRespDetalle  AND resp = @idResponsable AND  func = @idFuncion AND desc_func = @descripcionFunc ";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = param.IdResponsableDetalle };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdResponsable) ? DBNull.Value : (object) param.IdResponsable) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdFuncion) ? DBNull.Value : (object) param.IdFuncion) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@descripcionFunc", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.DescripcionFuncion) ? DBNull.Value : (object) param.DescripcionFuncion.Trim()) };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4 };
            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int DeleteByIdDetalle(string idResponsableDetalle, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Resp_detalle WHERE id_titulo_resp = @idRespDetalle   ";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = idResponsableDetalle };            

            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public List<ResponsableTituloDetail> Select(string idKey)
        {
            return SelectBy(idKey, null, null, null, null, null);
        }

        public List<ResponsableTituloDetail> SelectByFuncion(string idFuncion, int? top)
        {
            return SelectBy(null, null, idFuncion, null, null, top);
        }

        public List<ResponsableTituloDetail> SelectByResponsable(string idResponsable, int? top)
        {
            return SelectBy(null, idResponsable, null, null, null, top);
        }

        public List<ResponsableTituloDetail> SelectAll(int? top)
        {
            return SelectBy(null, null, null, null, null, top);
        }

        public List<ResponsableTituloDetail> SelectBy(string idKey, string idResponsable, string idFuncion,  string rfc, string nombre, int? top)
        {            
            List<ResponsableTituloDetail> lista = new List<ResponsableTituloDetail>();
            SqlParameter[] parametros = new SqlParameter[6];
            string query = " spBusquedaResponsablesTitulos";
            parametros[0] = new SqlParameter() { ParameterName = "@idTituloResp", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = (idKey == null) ? null : idKey, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = (idKey == null) ? null : idKey, IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@idFuncion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = (idKey == null) ? null : idKey, IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@rfc", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(rfc) ? rfc : rfc.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(nombre) ? nombre : nombre.Trim()), IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object)top.Value, IsNullable = true };
            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.Text, parametros, null);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ResponsableTituloDetail objeto = new ResponsableTituloDetail();                    
                    objeto.IdResponsableDetalle = reader["id_titulo_resp"].ToString();
                    objeto.IdResponsable = reader["resp"].ToString();
                    objeto.RfcResponsable = reader["RFC"].ToString();
                    objeto.NombreResponsable = reader["Nombres"].ToString();
                    objeto.ApPaternoResponsable = reader["Ape_Pat"].ToString();
                    objeto.ApMaternoResponsable = reader["Ape_Mat"].ToString();
                    objeto.IdFuncion= reader["func"].ToString();
                    objeto.TipoFuncion = reader["Tipo_Funcion"].ToString();
                    objeto.DescripcionFuncion = reader["desc_func"].ToString();
                    objeto.OrdenFuncion = (reader["orden_func"] != null ? new int?(Convert.ToInt32(reader["orden_func"].ToString())): null);
                    lista.Add(objeto);
                }
            }
            reader.Close();            
            return lista;
        }

       
       
    }
}
