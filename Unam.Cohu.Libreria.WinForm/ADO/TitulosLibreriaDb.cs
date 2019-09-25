using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unam.Cohu.Libreria.WinForm.ADO;
using Unam.Cohu.Libreria.WinForm.Model;

namespace WinFormScriptorum.ADO
{
    public class TitulosLibreriaDb : DbContext
    {        
        private string _QueryUpdate = " UPDATE Titulos_Trans SET UrlLibroPdf = {0} WHERE Id_Unico = {1};";
        public TitulosLibreriaDb() : base() { }
        public TitulosLibreriaDb(string cnnStrName) : base(cnnStrName) { }
        public TitulosLibreriaDb(SqlConnection connection) : base(connection) { }


        public void UpdateCnnString(string cnnStr)
        {
            this._cnnString = cnnStr;
            this.CloseConnection();
            if (this._SQLConexion != null) {
                this._SQLConexion.ConnectionString = this._cnnString;
            }            
        }

        public bool PingServer(ref string mensaje) {
            bool retorno = false;
            int timeout = 30;
            SqlConnectionStringBuilder cnnBuilder = null;
            try
            {
                cnnBuilder = new SqlConnectionStringBuilder(this._cnnString);
                timeout = cnnBuilder.ConnectTimeout;
                cnnBuilder.ConnectTimeout = 10;

                UpdateCnnString(cnnBuilder.ConnectionString);

                var data = this.SelectBy(new int?(0), null, null);
                cnnBuilder.ConnectTimeout = timeout;

                UpdateCnnString(cnnBuilder.ConnectionString);
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
                mensaje = ex.Message;
                this.CloseConnection();
                if (cnnBuilder != null) {
                    cnnBuilder.ConnectTimeout = timeout;
                    UpdateCnnString(cnnBuilder.ConnectionString);
                }                
            }
            return retorno;
        }

        public string GetQueryUpdate(Titulo param)
        {
            return string.Format(_QueryUpdate, string.Format("'{0}'", param.UrlPdf), string.Format("{0}",param.IdTitulo));
        }

        public int Update(Titulo param, SqlTransaction transaccion)
        {
            string query = string.Format(_QueryUpdate, "@path", "@idUnico");
            SqlParameter param1 = new SqlParameter() { ParameterName = "@path", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, IsNullable = true, Value = (String.IsNullOrEmpty(param.UrlPdf) ? DBNull.Value : (object)param.UrlPdf) };
            //SqlParameter param2 = new SqlParameter() { ParameterName = "@name", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.NombreArchivo) ? DBNull.Value : (object)param.NombreArchivo) };
            //SqlParameter param3 = new SqlParameter() { ParameterName = "@numEdicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (param.NumeroEdicion <= 0 ? DBNull.Value : (object)param.NumeroEdicion) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = param.IdTitulo };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param4};
            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public List<Titulo> SelectBy(int? id, string guidArchivo, int? top)
        {
            List<Titulo> lista = new List<Titulo>();
            string query = "SELECT {0} id_unico,titulo,edicion,RutaArchivo,NombreArchivo,NumeroEdicion, UrlLibroPdf ";
            query = query + " FROM Titulos_Trans ";
            query = String.Format(query, ((top != null) ? " TOP(" + top.Value.ToString() + ") " : string.Empty));
            query = query + " WHERE (id_unico = @idUnico OR @idUnico IS NULL) AND (RutaArchivo LIKE @path OR @path IS NULL)  ";
            query = query + " ORDER BY id_ciudad ";

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (id==null) ? DBNull.Value : (object)id.Value };
            parametros[1] = new SqlParameter() { ParameterName = "@path", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, IsNullable = true, Value = string.IsNullOrEmpty(guidArchivo) ? DBNull.Value : (object)("%" + guidArchivo.Trim() + "%") };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.Text, parametros, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Titulo objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }
            }            
            reader.Close();
            return lista;
        }

        protected Titulo ConvertReaderToTitulo(SqlDataReader reader)
        {
            Titulo objeto = null;
            int intDummy = 0;
            int? intNullable = null;
            string stringDummy = string.Empty;

            if (reader != null)
            {
                objeto = new Archivos();
                intDummy = 0;
                intNullable = null;
                Int32.TryParse(reader["id_unico"].ToString(), out intDummy);
                objeto.IdTitulo = intDummy;
                                
                intDummy = 0;
                Int32.TryParse((reader["edicion"] != null ? reader["edicion"].ToString() : string.Empty), out intDummy);
                objeto.Edicion = intDummy;
                
                objeto.DescripcionTitulo = (reader["titulo"] != null ? reader["titulo"].ToString().Trim() : string.Empty);
                //objeto.RutaArchivo = (reader["RutaArchivo"] != null ? reader["RutaArchivo"].ToString().Trim() : string.Empty);
                //objeto.NombreArchivo = (reader["NombreArchivo"] != null ? reader["NombreArchivo"].ToString().Trim() : string.Empty);
                objeto.UrlPdf = HttpUtility.UrlDecode((reader["UrlLibroPdf"] != null ? reader["UrlLibroPdf"].ToString().Trim() : string.Empty));
                
            }
            return objeto;
        }


    }
}
