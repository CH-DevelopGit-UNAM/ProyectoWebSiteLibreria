using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.Model.Security;

namespace Unam.CoHu.Libreria.ADO.Security
{
    public class ModuloContext:DbContext
    {
        public ModuloContext():base() { }
        public ModuloContext(string cnnName) : base(cnnName) { }
        public ModuloContext(SqlConnection connection) : base(connection) { }

        
        public List<SegModulo> SelectAll(int? top) {
            List<SegModulo> modules = null;
            StringBuilder selectQuery = new StringBuilder();
            selectQuery.AppendLine(" SELECT RAIZ.IdModulo,RAIZ.IdModuloSGU, RAIZ.Nombre, RAIZ.Descripcion, RAIZ.IdModuloPadre, RAIZ.IdModuloPadreSGU, ");
            selectQuery.AppendLine("		RAIZ.Icono, RAIZ.Pagina, RAIZ.Orden,RAIZ.Protegido, RAIZ.Clave, RAIZ.ClaveBusquedaMod ,COUNT(HIJOS.IdModulo) AS ConteoHijos");
            selectQuery.AppendLine(" FROM SegModulos AS RAIZ ");
            selectQuery.AppendLine(" LEFT JOIN SegModulos AS HIJOS ON RAIZ.IdModuloSGU = HIJOS.IdModuloPadreSGU");
            selectQuery.AppendLine(" GROUP BY RAIZ.IdModulo,RAIZ.IdModuloSGU, RAIZ.Nombre, RAIZ.Descripcion, RAIZ.IdModuloPadre, RAIZ.IdModuloPadreSGU,  ");
            selectQuery.AppendLine(" 		RAIZ.Icono, RAIZ.Pagina, RAIZ.Orden,RAIZ.Protegido, RAIZ.Clave, RAIZ.ClaveBusquedaMod ");
            int intDummy = -1;
            
            bool boolDummy = false;
            SqlDataReader reader = this.SeleccionarReader(selectQuery.ToString(), System.Data.CommandType.Text,null, null);
            if (reader.HasRows) {
                modules = new List<SegModulo>();
                while (reader.Read())
                {
                    intDummy = -1;
                    
                    boolDummy = false;
                    SegModulo modulo = new SegModulo();
                    Int32.TryParse(reader["IdModulo"].ToString(), out intDummy);
                    modulo.IdModulo = intDummy;
                    intDummy = -1;
                    Int32.TryParse(reader["IdModuloSGU"].ToString(), out intDummy);
                    modulo.IdModuloSGU = intDummy;
                    modulo.Nombre = reader["Nombre"].ToString();
                    modulo.Descripcion = reader["Descripcion"].ToString();
                    if (String.IsNullOrEmpty(reader["IdModuloPadre"].ToString())==false)
                    {
                        intDummy = -1;
                        Int32.TryParse(reader["IdModuloPadre"].ToString(), out intDummy);
                        modulo.IdModuloPadre = new int?(intDummy);
                    }

                    if (String.IsNullOrEmpty(reader["IdModuloPadreSGU"].ToString())==false)
                    {
                        intDummy = -1;
                        Int32.TryParse(reader["IdModuloPadreSGU"].ToString(), out intDummy);
                        modulo.IdModuloPadreSGU = new int?(intDummy);
                    }
                    modulo.Icono = reader["Icono"].ToString();
                    modulo.Pagina = reader["Pagina"].ToString();
                    intDummy = -1;
                    Int32.TryParse(reader["Orden"].ToString(), out intDummy);
                    modulo.Orden = intDummy;

                    Boolean.TryParse(reader["Protegido"].ToString(), out boolDummy);
                    modulo.IsProtegino = boolDummy;
                    modulo.Icono = reader["Clave"].ToString();
                    modulo.Pagina = reader["ClaveBusquedaMod"].ToString();
                    intDummy = -1;
                    Int32.TryParse(reader["ConteoHijos"].ToString(), out intDummy);
                    modulo.CountChildren = intDummy;

                    modules.Add(modulo);
                }
                reader.Close();
            }            
            return modules;
        }

    }
}
