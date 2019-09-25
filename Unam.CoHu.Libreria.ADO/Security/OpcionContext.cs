using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.Model.Security;

namespace Unam.CoHu.Libreria.ADO.Security
{
    public class OpcionContext: DbContext
    {
        public OpcionContext():base() { }

        public OpcionContext(string cnnName) : base(cnnName){ }

        public OpcionContext(SqlConnection connection) : base(connection) { }

        public int Insert(Opcion opcion, SqlTransaction transaccion)
        {
            string query = "INSERT INTO dbo.ctOpciones (IdOpcion, IdOpcionPadre, IdTipoOpcion, IdTipoPermiso, Opcion, Descripcion, Comando, Orden) VALUES (@idOpcion, @idPadre, @idTipoOpcion, @idTipoPermiso, @opcion, @descripcion, @comando, @orden)";            
            SqlParameter paramIdOpcion = new SqlParameter() { ParameterName = "@idOpcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = opcion.IdOpcion};
            SqlParameter paramIdPadre = new SqlParameter() { ParameterName = "@idPadre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = opcion.IdOpcionPadre};
            SqlParameter paramIdTipoOpcion = new SqlParameter() { ParameterName = "@idTipoOpcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = 2 };
            SqlParameter paramIdTipoPermiso = new SqlParameter() { ParameterName = "@idTipoPermiso", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = 0 };
            SqlParameter paramOpcion = new SqlParameter() { ParameterName = "@opcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = opcion.NombreOpcion};
            SqlParameter paramDescripcion = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = opcion.Descripcion };
            SqlParameter paramComando = new SqlParameter() { ParameterName = "@comando", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = opcion.Comando };
            SqlParameter paramOrden= new SqlParameter() { ParameterName = "@orden", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = opcion.Orden};
            List<SqlParameter> parametros = new List<SqlParameter>() { paramIdOpcion, paramIdPadre, paramIdTipoOpcion, paramIdTipoPermiso, paramOpcion, paramDescripcion, paramComando, paramOrden };
            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Update(Opcion opcion, SqlTransaction transaccion)
        {
            string query = "UPDATE dbo.ctOpciones SET IdOpcionPadre=@idPadre, IdTipoOpcion=@idTipoOpcion, IdTipoPermiso=@idTipoPermiso, Opcion=@opcion, Descripcion=@descripcion, Comando=@comando, Orden=@orden WHERE IdOpcion=@idOpcion ";            
            SqlParameter paramIdPadre = new SqlParameter() { ParameterName = "@idPadre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = opcion.IdOpcionPadre };
            SqlParameter paramIdTipoOpcion = new SqlParameter() { ParameterName = "@idTipoOpcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = 2 };
            SqlParameter paramIdTipoPermiso = new SqlParameter() { ParameterName = "@idTipoPermiso", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = 0 };
            SqlParameter paramOpcion = new SqlParameter() { ParameterName = "@opcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = opcion.NombreOpcion };
            SqlParameter paramDescripcion = new SqlParameter() { ParameterName = "@descripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = opcion.Descripcion };
            SqlParameter paramComando = new SqlParameter() { ParameterName = "@comando", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = opcion.Comando };
            SqlParameter paramOrden = new SqlParameter() { ParameterName = "@orden", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = opcion.Orden };
            SqlParameter paramIdOpcion = new SqlParameter() { ParameterName = "@idOpcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = opcion.IdOpcion };
            List<SqlParameter> parametros = new List<SqlParameter>() { paramIdPadre, paramIdTipoOpcion, paramIdTipoPermiso, paramOpcion, paramDescripcion, paramComando, paramOrden, paramIdOpcion};

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(int id, SqlTransaction transaccion)
        {
            string query = "DELETE FROM dbo.ctOpciones WHERE IdOpcion=@idOpcion ";
            SqlParameter paramIdOpcion = new SqlParameter() { ParameterName = "@idOpcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = id};
            List<SqlParameter> parametros = new List<SqlParameter>() { paramIdOpcion };
            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public Opcion Select(int id, SqlTransaction transaccion)
        {
            Opcion opcion = null;
            string query = "SELECT IdOpcion,IdOpcionPadre,IdTipoOpcion,IdTipoPermiso,Opcion,Descripcion,Comando,Orden FROM dbo.ctOpciones WHERE IdOpcion=@idOpcion; ";
            SqlParameter paramidUsuario = new SqlParameter() { ParameterName = "@idOpcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = id };
            List<SqlParameter> parametros = new List<SqlParameter>() { paramidUsuario };            
            SqlDataReader reader = this.SeleccionarReader(query, CommandType.Text, parametros, transaccion);
            int intDummy = 0;
            int? idUsuarioJefe = new int?(-1);

            if (reader.HasRows)
            {
                opcion = new Opcion();
                while (reader.Read())
                {
                    intDummy = 0;
                    Int32.TryParse(reader["IdUsuario"].ToString(), out intDummy);
                    opcion.IdOpcion = intDummy;
                    intDummy = 0;                                        
                    if (String.IsNullOrEmpty(reader["IdOpcionPadre"].ToString()))
                    {
                        opcion.IdOpcionPadre = null;                        
                    }
                    else
                    {
                        Int32.TryParse(reader["IdOpcionPadre"].ToString(), out intDummy);
                        opcion.IdOpcionPadre = new int?(intDummy);
                    }                    
                    opcion.NombreOpcion = reader["Opcion"].ToString();
                    opcion.Comando = reader["Comando"].ToString();
                    opcion.Descripcion = reader["Descripcion"].ToString();
                    intDummy = 0;
                    Int32.TryParse(reader["Orden"].ToString(), out intDummy);
                    opcion.Orden = intDummy;
                }
            }

            if(transaccion == null)
                reader.Close();
            return opcion;
        }

        public List<Opcion> SelectAll(int? top)
        {
            List<Opcion> listaOpciones = new List<Opcion>();
            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT [IdOpcion],[IdOpcionPadre],[IdTipoOpcion],[IdTipoPermiso],[Opcion],[Descripcion],[Comando],[Orden] ");
            query.AppendLine(" FROM ctOpciones ORDER BY IdOpcionPadre,Orden; ");

            SqlDataReader reader = this.SeleccionarReader(query.ToString(), CommandType.Text, null,null);            
            
            int idopcion = 0;
            int orden = 0;
            int? idOpcionPadre = new int?(-1);
            int auxNull = 0;
            while (reader.Read())
            {
                idopcion = 0;
                orden = 0;
                idOpcionPadre = null;
                auxNull = 0;
                Int32.TryParse(reader["IdOpcion"].ToString(), out idopcion);
                Int32.TryParse(reader["Orden"].ToString(), out orden);
                if (String.IsNullOrEmpty(reader["IdOpcionPadre"].ToString()))
                {
                    idOpcionPadre = null;
                }
                else
                {
                    Int32.TryParse(reader["IdOpcionPadre"].ToString(), out auxNull);
                    idOpcionPadre = auxNull;
                }

                Opcion obj = new Opcion()
                {
                    IdOpcion = idopcion,
                    IdOpcionPadre = idOpcionPadre,
                    NombreOpcion = reader["Opcion"].ToString(),
                    Comando = reader["Comando"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Orden = orden
                };

                listaOpciones.Add(obj);
            }
            reader.Close();
            return listaOpciones;
        }
    }
}
