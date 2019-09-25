using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO.Enumerations;
using Unam.CoHu.Libreria.Model.Security;

namespace Unam.CoHu.Libreria.ADO.Security
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext() : base() { }
        public UsuarioContext(string cnnStrName) : base(cnnStrName) { }
        public UsuarioContext(SqlConnection connection) : base(connection) { }

        public int Insert(Usuario usuario, SqlTransaction transaccion)
        {
            string sp = "spInsertUsuario";
            int idInsertado = -1;
            SqlParameter paramUsuario = new SqlParameter() { ParameterName = "@usuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = usuario.UsuarioNombre };
            SqlParameter paramNombre = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = usuario.Nombre };            
            SqlParameter paramContrasena = new SqlParameter() { ParameterName = "@contrasena", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = usuario.Password};
            SqlParameter paramIdJefeUsuario = new SqlParameter() { ParameterName = "@idJefeUsuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = usuario.IdUsuarioJefe};
            SqlParameter paramidUsuarioOUT = new SqlParameter() { ParameterName = "@idUsuarioOUT", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int };
            List<SqlParameter> parametros =new List<SqlParameter>(){ paramUsuario, paramNombre, paramContrasena, paramIdJefeUsuario, paramidUsuarioOUT };
            int result = this.Ejecutar(sp, CommandType.StoredProcedure, parametros, transaccion);
            Int32.TryParse( paramidUsuarioOUT.Value.ToString(), out idInsertado);
            usuario.IdUsuario = idInsertado;
            return result;            
        }

        public int Update(Usuario usuario, SqlTransaction transaccion)
        {
            string sp = "spUpdateUsuario";
            SqlParameter paramidUsuario = new SqlParameter() { ParameterName = "@idUsuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = usuario.IdUsuario };
            SqlParameter paramUsuario = new SqlParameter() { ParameterName = "@usuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = usuario.UsuarioNombre };
            SqlParameter paramNombre = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = usuario.Nombre };
            SqlParameter paramContrasena = new SqlParameter() { ParameterName = "@contrasena", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = usuario.Password};
            SqlParameter paramIdJefeUsuario = new SqlParameter() { ParameterName = "@idJefeUsuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = usuario.IdUsuarioJefe };
            List<SqlParameter> parametros = new List<SqlParameter>() { paramidUsuario,paramUsuario, paramNombre, paramContrasena, paramIdJefeUsuario };            
            int result = this.Ejecutar(sp, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }

        public int Delete(int id, SqlTransaction transaccion)
        {
            string sp = "SpDeleteUsuario";
            SqlParameter paramidUsuario = new SqlParameter() { ParameterName = "@idUsuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = id};
            List<SqlParameter> parametros = new List<SqlParameter>() { paramidUsuario };
            int result = this.Ejecutar(sp, CommandType.StoredProcedure, parametros, transaccion);
            return result;
        }

        public Usuario Select(int id)
        {
            Usuario objeto = null;
            List<Usuario> lista = SelectBy(new int?(id), null, null, null, null, null, "busqueda");
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }

        public List<Usuario> SelectByNombre(string nombre, bool? activos, int? top)
        {            
            return SelectBy(null, nombre, null, null, activos, top, "busqueda");
        }

        public List<Usuario> SelectByNombrePassword(string nombre, string pass)
        {            
            return SelectBy(null, nombre, null, pass, null, null, "autenticar");
        }

        public List<Usuario> SelectAll(bool? activos)
        {
            return SelectBy(null, null, null, null, null, null, "busqueda");
        }

        public List<Usuario> SelectBy(int? idKey, string usuario, string nombre, string contrasena, bool? activos, int? top, string action)
        {
            List<Usuario> lista = null;
            SqlParameter[] parametros = new SqlParameter[7];            
            string query = "spUsuariosSistema";
            parametros[0] = new SqlParameter() { ParameterName = "@idUsuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (idKey == null ) ? null : (object) idKey.Value, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@usuario", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)usuario, IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@nombre", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = nombre, IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@contrasena", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = contrasena, IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@activo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (activos == null) ? null : (object)activos.Value, IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object)top.Value, IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@action", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = action };
            
            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);
            int intDummy = 0;
            if (reader.HasRows)
            {
                lista = new List<Usuario>();
                while (reader.Read())
                {
                    intDummy = 0;
                    Usuario objeto = new Usuario();
                    Int32.TryParse(reader["IdUsuario"].ToString(), out intDummy);
                    objeto.IdUsuario = intDummy;
                    objeto.UsuarioNombre = reader["Usuario"].ToString();
                    var b = reader["Nombre"];
                    objeto.Nombre = (reader["Nombre"] == null)? "" : reader["Nombre"].ToString();
                    objeto.Password = reader["Password"].ToString();
                    var a = reader["IdJefeUsuario"];
                    objeto.IdUsuarioJefe = (reader["IdJefeUsuario"] == null) ? null: new int?(Convert.ToInt32(reader["IdJefeUsuario"]));
                    objeto.Activo = Convert.ToBoolean( reader["Activo"]);
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }        

    }
}
