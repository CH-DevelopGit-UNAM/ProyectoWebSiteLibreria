
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.Cohu.Libreria.WinForm.Model;

namespace Unam.Cohu.Libreria.WinForm.ADO
{
    public class DbContext : IDisposable
    {
        protected string _cnnString = Configuracion.ObtenerCnnString("DbConnection");
        protected SqlConnection _SQLConexion;
        protected bool _ToUpper = false;

        public SqlConnection SQLConexion
        {
            set { this._SQLConexion = value; }
            get { return _SQLConexion; }
        }

        public DbContext()
        {
            this._SQLConexion = new SqlConnection(_cnnString);
        }

        public DbContext(string nameCnnConfiguration)
        {
            _cnnString = Configuracion.ObtenerCnnString(nameCnnConfiguration);
            this._SQLConexion = new SqlConnection(_cnnString);
        }


        public DbContext(SqlConnection connection)
        {
            this._SQLConexion = connection;
        }

        protected DataTable LimpiarDataTableDbNUll(DataTable dt)
        {
            DataTable dtReturn = null;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < row.ItemArray.Length - 1; i++)
                    {
                        Type tipo = dt.Columns[i].DataType;
                        object value = row[i];
                        row[i] = SetEmptyToNullValue(tipo, value);
                    }
                }
            }
            return dtReturn;
        }

        protected object SetEmptyToNullValue(Type type, object value)
        {
            object retorno = "";
            // Microsoft.VisualBasic
            if (value == null)
            {
                if (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Decimal) || type == typeof(Double))
                {
                    retorno = 0;
                }
                else
                {
                    retorno = "";
                }
            }
            return retorno;
        }

        public DataTable SeleccionarDataTable(string strQuery, CommandType commandType, List<SqlParameter> listaParametros)
        {
            DataTable dt = SqlHelper.ExecuteDataTable(_cnnString, commandType, strQuery, (listaParametros != null ? listaParametros.ToArray() : null));
            return this.LimpiarDataTableDbNUll(dt);
        }

        public SqlDataReader SeleccionarReader(string strQuery, CommandType commandType, List<SqlParameter> listaParametros, SqlTransaction transaccion)
        {
            SqlDataReader reader = null;
            if (transaccion == null)
            {
                reader = SqlHelper.ExecuteReader(_SQLConexion, commandType, strQuery, (listaParametros != null ? listaParametros.ToArray() : null));
            }
            else
            {
                reader = SqlHelper.ExecuteReader(transaccion, commandType, strQuery, (listaParametros != null ? listaParametros.ToArray() : null));
            }

            return reader;
        }

        public SqlDataReader SeleccionarReaderArray(string strQuery, CommandType commandType, SqlParameter[] listaParametros, SqlTransaction transaccion)
        {
            SqlDataReader reader = null;
            if (transaccion == null)
            {
                reader = SqlHelper.ExecuteReader(_SQLConexion, commandType, strQuery, (listaParametros != null ? listaParametros : null));
            }
            else
            {
                reader = SqlHelper.ExecuteReader(transaccion, commandType, strQuery, (listaParametros != null ? listaParametros : null));
            }

            return reader;
        }

        public T SeleccionarEscalar<T>(string strQuery, CommandType commandType, List<SqlParameter> listaParametros, SqlTransaction transaccion)
        {
            T objRegreso = default(T);
            if (transaccion == null)
            {
                objRegreso = (T)SqlHelper.ExecuteScalar(_SQLConexion, commandType, strQuery, (listaParametros != null ? listaParametros.ToArray() : null));
            }
            else
            {
                objRegreso = (T)SqlHelper.ExecuteScalar(transaccion, commandType, strQuery, (listaParametros != null ? listaParametros.ToArray() : null));
            }
            return objRegreso;
        }

        public int Ejecutar(string strQuery, CommandType commandType, List<SqlParameter> listaParametros, SqlTransaction transaccion)
        {
            int numRegistrosAfectados = -1;
            if (this._ToUpper)
                strQuery = String.IsNullOrEmpty(strQuery) ? strQuery : strQuery.ToUpper();

            if (transaccion == null)
            {
                numRegistrosAfectados = SqlHelper.ExecuteNonQuery(_SQLConexion, commandType, strQuery, (listaParametros != null ? listaParametros.ToArray() : null));
            }
            else
            {
                numRegistrosAfectados = SqlHelper.ExecuteNonQuery(transaccion, commandType, strQuery, (listaParametros != null ? listaParametros.ToArray() : null));
            }

            return numRegistrosAfectados;
        }



        public void OpenConnection()
        {
            if (this.SQLConexion != null)
            {
                if (this.SQLConexion.State != System.Data.ConnectionState.Open)
                {
                    this.SQLConexion.Open();
                }
            }
        }

        public void CloseConnection()
        {
            if (this.SQLConexion != null)
            {
                if (this.SQLConexion.State != System.Data.ConnectionState.Closed)
                {
                    this.SQLConexion.Close();
                }
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.CloseConnection();
                    if (this._SQLConexion != null)
                    {
                        this.SQLConexion.Dispose();
                        this.SQLConexion = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.                
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DbContext()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
