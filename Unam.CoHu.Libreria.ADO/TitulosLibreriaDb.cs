using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO.Enumerations;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

namespace Unam.CoHu.Libreria.ADO
{
    public class TitulosLibreriaDb : DbContext
    {        

        public TitulosLibreriaDb() : base() { }
        public TitulosLibreriaDb(string cnnStrName) : base(cnnStrName) { }
        public TitulosLibreriaDb(SqlConnection connection) : base(connection) { }

        public int Insert(TituloLibreria param, SqlTransaction transaccion)
        {
            string query = " INSERT INTO Titulos_Trans (id_isbn,id_ciudad,id_editor,id_responsable,id_autores,titulo_original,titulo,edicion,alo_pub,paginas,medidas,_500_serie,contenido,cualidades,colofon,tema,secundarias,U_FFYL,U_IIFL,obs_res,id_serie, IsNovedad, UrlLibroPdf, UrlLibroVirtual, UrlLibroOnline, NumeroEdicion, NumeroReimpresion, IsGriego, IsLatin) ";
            query = query + " VALUES (@idIsbn,@idCiudad,@idEditor,@idResponsable,@idAutor,@tituloOriginal,@titulo,@edicion,@aloPub,@paginas,@medidas, @500serie,@contenido,@cualidades,@colofon,@tema,@secundarias,@U_FFYL,@U_IIFL,@obs_res,@id_serie, @isNovedad, @urlPdf,@urlVirtual,@urlOnline,@numEdicion,@numReimpresion,@isGriego,@isLatin); ";
            query = query + " SELECT CAST(ISNULL(SCOPE_IDENTITY(),-1) AS INT) AS CAMPO;  ";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdIsbn) ? DBNull.Value : (object)param.IdIsbn.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdCiudad) ? DBNull.Value : (object)param.IdCiudad.Trim()) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdEditor };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdResponsableDetalle };
            SqlParameter param5 = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdAutor };
            SqlParameter param6 = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = param.TituloOriginal.Trim() };
            SqlParameter param7 = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable=true, Value = (String.IsNullOrEmpty(param.Titulo) ? DBNull.Value : (object) param.Titulo.Trim()) };
            SqlParameter param8 = new SqlParameter() { ParameterName = "@edicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Edicion) ? DBNull.Value : (object)param.Edicion.Trim()) };
            SqlParameter param9 = new SqlParameter() { ParameterName = "@aloPub", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = param.AnioPublicacion};
            SqlParameter param10 = new SqlParameter() { ParameterName = "@paginas", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 100, IsNullable = true , Value = (String.IsNullOrEmpty(param.Paginas) ? DBNull.Value : (object) param.Paginas.Trim()) };
            SqlParameter param11 = new SqlParameter() { ParameterName = "@medidas", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.Medidas) ? DBNull.Value : (object)param.Medidas.Trim()) };
            SqlParameter param12 = new SqlParameter() { ParameterName = "@500serie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Serie500) ? DBNull.Value : (object)param.Serie500.Trim()) };
            SqlParameter param13 = new SqlParameter() { ParameterName = "@contenido", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Contenido) ? DBNull.Value : (object)param.Contenido.Trim()) };
            SqlParameter param14 = new SqlParameter() { ParameterName = "@cualidades", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Cualidades) ? DBNull.Value : (object)param.Cualidades.Trim()) };
            SqlParameter param15 = new SqlParameter() { ParameterName = "@colofon", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Colofon) ? DBNull.Value : (object)param.Colofon.Trim()) };
            SqlParameter param16 = new SqlParameter() { ParameterName = "@tema", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Tema) ? DBNull.Value : (object)param.Tema.Trim()) };
            SqlParameter param17 = new SqlParameter() { ParameterName = "@secundarias", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Secundarias) ? DBNull.Value : (object)param.Secundarias.Trim()) };
            SqlParameter param18 = new SqlParameter() { ParameterName = "@U_FFYL", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size=100 ,IsNullable = true, Value = (String.IsNullOrEmpty(param.UffYL) ? DBNull.Value : (object)param.UffYL.Trim()) };
            SqlParameter param19 = new SqlParameter() { ParameterName = "@U_IIFL", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.UiiFL) ? DBNull.Value : (object)param.UiiFL.Trim()) };
            SqlParameter param20 = new SqlParameter() { ParameterName = "@obs_res", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Observaciones) ? DBNull.Value : (object)param.Observaciones.Trim()) };
            SqlParameter param21 = new SqlParameter() { ParameterName = "@id_serie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdSerie };
            SqlParameter param22 = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit , Value = param.IsNovedad};
            SqlParameter param23 = new SqlParameter() { ParameterName = "@urlPdf", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size= 500, IsNullable = true, Value = (String.IsNullOrEmpty(param.UrlPdf) ? DBNull.Value : (object)param.UrlPdf.Trim()) };
            SqlParameter param24 = new SqlParameter() { ParameterName = "@urlVirtual", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 500, IsNullable = true, Value = (String.IsNullOrEmpty(param.UrlVirtual) ? DBNull.Value : (object)param.UrlVirtual.Trim()) };
            SqlParameter param25 = new SqlParameter() { ParameterName = "@urlOnline", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 500, IsNullable = true, Value = (String.IsNullOrEmpty(param.UrlOnline) ? DBNull.Value : (object)param.UrlOnline.Trim()) };
            SqlParameter param26 = new SqlParameter() { ParameterName = "@numEdicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (param.NumeroEdicion <= 0 ? DBNull.Value : (object) param.NumeroEdicion) };
            SqlParameter param27 = new SqlParameter() { ParameterName = "@numReimpresion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (param.NumeroReimpresion <= 0 ? DBNull.Value : (object)param.NumeroReimpresion) };
            SqlParameter param28 = new SqlParameter() { ParameterName = "@isGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, IsNullable = true, Value = ((param.IsLatin | param.IsGriego) ==false ? DBNull.Value : (object)param.IsGriego) };
            SqlParameter param29 = new SqlParameter() { ParameterName = "@isLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, IsNullable = true, Value = ((param.IsLatin | param.IsGriego) == false ? DBNull.Value : (object)param.IsLatin) };

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, param18,param19,param20, param21, param22, param23, param24 , param25 , param26 , param27, param28, param29 };

            int result = this.SeleccionarEscalar<int>(query, CommandType.Text, parametros, transaccion);
            if (result > 0)
                param.IdTitulo = result;

            return result;
        }

        public int Update(TituloLibreria param, SqlTransaction transaccion)
        {
            string query = " UPDATE Titulos_Trans SET id_isbn=@idIsbn, id_ciudad=@idCiudad ,id_editor=@idEditor, id_responsable=@idResponsable, id_autores=@idAutor, titulo_original=@tituloOriginal, titulo=@titulo ";
            query = query + ",edicion=@edicion,alo_pub=@aloPub,paginas=@paginas,medidas=@medidas,_500_serie=@500serie,contenido=@contenido,cualidades=@cualidades,colofon=@colofon,tema=@tema ";
            query = query + ",secundarias=@secundarias,U_FFYL=@U_FFYL,U_IIFL=@U_IIFL,obs_res=@obs_res,id_serie=@id_serie, IsNovedad = @isNovedad, UrlLibroPdf=@urlPdf, UrlLibroVirtual=@urlVirtual, UrlLibroOnline=@urlOnline, NumeroEdicion=@numEdicion,NumeroReimpresion=@numReimpresion ";
            query = query + ",IsGriego = @isGriego, IsLatin = @isLatin ";
            query = query + " WHERE Id_Unico = @idUnico";            
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdIsbn) ? DBNull.Value : (object)param.IdIsbn.Trim()) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, IsNullable = true, Value = (String.IsNullOrEmpty(param.IdCiudad) ? DBNull.Value : (object)param.IdCiudad.Trim()) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdEditor };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@idResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdResponsableDetalle };
            SqlParameter param5 = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdAutor };
            SqlParameter param6 = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = param.TituloOriginal.Trim() };
            SqlParameter param7 = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Titulo) ? DBNull.Value : (object)param.Titulo.Trim()) };
            SqlParameter param8 = new SqlParameter() { ParameterName = "@edicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Edicion) ? DBNull.Value : (object)param.Edicion.Trim()) };
            SqlParameter param9 = new SqlParameter() { ParameterName = "@aloPub", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = param.AnioPublicacion };
            SqlParameter param10 = new SqlParameter() { ParameterName = "@paginas", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.Paginas) ? DBNull.Value : (object)param.Paginas.Trim()) };
            SqlParameter param11 = new SqlParameter() { ParameterName = "@medidas", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.Medidas) ? DBNull.Value : (object)param.Medidas.Trim()) };
            SqlParameter param12 = new SqlParameter() { ParameterName = "@500serie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Serie500) ? DBNull.Value : (object)param.Serie500.Trim()) };
            SqlParameter param13 = new SqlParameter() { ParameterName = "@contenido", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Contenido) ? DBNull.Value : (object)param.Contenido.Trim()) };
            SqlParameter param14 = new SqlParameter() { ParameterName = "@cualidades", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Cualidades) ? DBNull.Value : (object)param.Cualidades.Trim()) };
            SqlParameter param15 = new SqlParameter() { ParameterName = "@colofon", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Colofon) ? DBNull.Value : (object)param.Colofon.Trim()) };
            SqlParameter param16 = new SqlParameter() { ParameterName = "@tema", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Tema) ? DBNull.Value : (object)param.Tema.Trim()) };
            SqlParameter param17 = new SqlParameter() { ParameterName = "@secundarias", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Secundarias) ? DBNull.Value : (object)param.Secundarias.Trim()) };
            SqlParameter param18 = new SqlParameter() { ParameterName = "@U_FFYL", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.UffYL) ? DBNull.Value : (object)param.UffYL.Trim()) };
            SqlParameter param19 = new SqlParameter() { ParameterName = "@U_IIFL", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(param.UiiFL) ? DBNull.Value : (object)param.UiiFL.Trim()) };
            SqlParameter param20 = new SqlParameter() { ParameterName = "@obs_res", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, IsNullable = true, Value = (String.IsNullOrEmpty(param.Observaciones) ? DBNull.Value : (object) param.Observaciones.Trim()) };
            SqlParameter param21 = new SqlParameter() { ParameterName = "@id_serie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = param.IdSerie };
            SqlParameter param22 = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = param.IsNovedad};
            SqlParameter param23 = new SqlParameter() { ParameterName = "@urlPdf", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 500, IsNullable = true, Value = (String.IsNullOrEmpty(param.UrlPdf) ? DBNull.Value : (object)param.UrlPdf.Trim()) };
            SqlParameter param24 = new SqlParameter() { ParameterName = "@urlVirtual", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 500, IsNullable = true, Value = (String.IsNullOrEmpty(param.UrlVirtual) ? DBNull.Value : (object)param.UrlVirtual.Trim()) };
            SqlParameter param25 = new SqlParameter() { ParameterName = "@urlOnline", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 500, IsNullable = true, Value = (String.IsNullOrEmpty(param.UrlOnline) ? DBNull.Value : (object)param.UrlOnline.Trim()) };
            SqlParameter param26 = new SqlParameter() { ParameterName = "@numEdicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (param.NumeroEdicion <= 0 ? DBNull.Value : (object)param.NumeroEdicion) };
            SqlParameter param27 = new SqlParameter() { ParameterName = "@numReimpresion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (param.NumeroReimpresion <= 0 ? DBNull.Value : (object)param.NumeroReimpresion) };
            SqlParameter param28 = new SqlParameter() { ParameterName = "@isGriego", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, IsNullable = true, Value = ((param.IsLatin | param.IsGriego) == false ? DBNull.Value : (object)param.IsGriego) };
            SqlParameter param29 = new SqlParameter() { ParameterName = "@isLatin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, IsNullable = true, Value = ((param.IsLatin | param.IsGriego) == false ? DBNull.Value : (object)param.IsLatin) };

            SqlParameter param30 = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = param.IdTitulo };            

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, param18, param19, param20, param21, param22, param23, param24, param25, param26, param27, param28,param29,param30};

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int UpdateFile(int id, string path, string nameFile, SqlTransaction transaccion)
        {
            string query = " UPDATE Titulos_Trans SET RutaArchivo=@path, NombreArchivo=@name ";
            query = query + " WHERE Id_Unico = @idUnico";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@path", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, IsNullable = true, Value = (String.IsNullOrEmpty(path) ? DBNull.Value : (object)path) };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@name", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, IsNullable = true, Value = (String.IsNullOrEmpty(nameFile) ? DBNull.Value : (object)nameFile) };                        
            SqlParameter param3 = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = id };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3 };

            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }

        public int Delete(int id, SqlTransaction transaccion)
        {
            string query = "DELETE FROM Titulos_Trans WHERE Id_Unico =@idUnico";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = id };
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            int result = this.Ejecutar(query, CommandType.Text, parametros, transaccion);
            return result;
        }


        public int InsertDetalleIsbn(Isbn detalleIsbn, SqlTransaction transaccion)
        {
            string query = " INSERT INTO TitulosIsbn(IdTitulo,IdDescripcion,NumeroReimpresion,NumeroReedicion,NumeroEdicion,ISBN) ";
            query = query + " VALUES (@idTitulo,@idDescripcion,@numeroReimpresion,@numeroReedicion,@numeroEdicion,@isbn); ";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idTitulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = detalleIsbn.IdTitulo };
            SqlParameter param2 = new SqlParameter() { ParameterName = "@idDescripcion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (detalleIsbn.IdDescripcion <= 0 ? DBNull.Value : (object) detalleIsbn.IdDescripcion) };
            SqlParameter param3 = new SqlParameter() { ParameterName = "@numeroReimpresion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true , Value = (detalleIsbn.Reimpresion<=0? DBNull.Value: (object) detalleIsbn.Reimpresion) };
            SqlParameter param4 = new SqlParameter() { ParameterName = "@numeroReedicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (detalleIsbn.Reedicion <= 0 ? DBNull.Value : (object)detalleIsbn.Reedicion) };
            SqlParameter param5 = new SqlParameter() { ParameterName = "@numeroEdicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, IsNullable = true, Value = (detalleIsbn.Edicion <= 0 ? DBNull.Value : (object)detalleIsbn.Edicion) };
            SqlParameter param6 = new SqlParameter() { ParameterName = "@isbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = detalleIsbn.ClaveIsbn };            

            List<SqlParameter> parametros = new List<SqlParameter>() { param1, param2, param3, param4, param5 , param6};
            return this.Ejecutar(query, CommandType.Text, parametros, transaccion);
        }

        public int DeleteIsbnByTitulo(int id, SqlTransaction transaccion)
        {
            string query = " DELETE FROM TitulosIsbn ";
            query = query + " WHERE IdTitulo = @idTitulo ; ";
            SqlParameter param1 = new SqlParameter() { ParameterName = "@idTitulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = id};            
            List<SqlParameter> parametros = new List<SqlParameter>() { param1 };
            return this.Ejecutar(query, CommandType.Text, parametros, transaccion);
        }


        public TituloLibreriaDetail SelectPaginacion(int id, ref Paginacion paginacion)
        {
            TituloLibreriaDetail objeto = null;
            List<TituloLibreriaDetail> lista =  SelectPaginacionBy(new int?(id), null, null, null, null, null, null, null, null, null, null, null, null, false , ref paginacion);
            if (lista != null && lista.Count > 0)
            {
                objeto = lista[0];
            }
            return objeto;
        }

        public List<TituloLibreriaDetail> SelectPaginacionByTitulos(string titulo, bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            return SelectPaginacionBy(null, null, null, null, null, null, titulo, titulo, null, null, null, isNovedad, ordenColumna, isOrderDescending ,ref paginacion);
        }        

        public List<TituloLibreriaDetail> SelectPaginacionAll(bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {            
            return SelectPaginacionBy(null, null, null, null, null,null,null,null,null,null, null, isNovedad, ordenColumna, isOrderDescending, ref paginacion);
        }

        public List<TituloLibreriaDetail> SelectPaginacionBy(int? idKey, string idIsbn, string idCiudad, string idEditor, string idResponsable,string idAutor, string tituloOriginal, 
                                                    string titulo, string tema, string idSerie, string idResponsableUnico, bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
            SqlParameter[] parametros = new SqlParameter[17];
            if (paginacion == null)
            {
                paginacion = new Paginacion() { FilasPorPagina = 1, PaginaActual = 1 };
            }
            else {
                paginacion.PaginaActual = (paginacion.PaginaActual <= 0) ? 1 : paginacion.PaginaActual;
            }
            string query = "spPaginacionTitulosLibreria";
            parametros[0] = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (idKey == null) ? null : (object)idKey.Value, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10, Value = (object)(string.IsNullOrEmpty(idIsbn) ? idIsbn : idIsbn.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10, Value = (object)(string.IsNullOrEmpty(idCiudad) ? idCiudad : idCiudad.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10, Value = (object)(string.IsNullOrEmpty(idEditor) ? idEditor : idEditor.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10, Value = (object)(string.IsNullOrEmpty(idResponsable) ? idResponsable : idResponsable.Trim()), IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10, Value = (object)(string.IsNullOrEmpty(idAutor) ? idAutor : idAutor.Trim()), IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(tituloOriginal) ? tituloOriginal : tituloOriginal.Trim()), IsNullable = true };
            parametros[7] = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(titulo) ? titulo : titulo.Trim()), IsNullable = true };
            parametros[8] = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10, Value = (object)(string.IsNullOrEmpty(idSerie) ? idSerie : idSerie.Trim()), IsNullable = true };
            parametros[9] = new SqlParameter() { ParameterName = "@idResponsableUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)idResponsableUnico, IsNullable = true };
            parametros[10] = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (isNovedad == null) ? null : (object)isNovedad.Value, IsNullable = true };
            parametros[11] = new SqlParameter() { ParameterName = "@orderColumn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)ordenColumna, IsNullable = true };
            parametros[12] = new SqlParameter() { ParameterName = "@isAscendingDirection", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (object)(isOrderDescending == false ? true : false), IsNullable = true };
            parametros[13] = new SqlParameter() { ParameterName = "@PageNumber", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginaActual };
            parametros[14] = new SqlParameter() { ParameterName = "@RowspPage", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasPorPagina };
            parametros[15] = new SqlParameter() { ParameterName = "@numberPages", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginasTotales };
            parametros[16] = new SqlParameter() { ParameterName = "@rowsCount", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasTotales };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);
           
            if (reader.HasRows)
            {                
                while (reader.Read())
                {
                    TituloLibreriaDetail objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }                
            }
            reader.NextResult();
            reader.Close();
            paginacion.PaginasTotales = (int)parametros[15].Value;
            paginacion.FilasTotales = (int)parametros[16].Value;
            paginacion.PaginaActual = (paginacion.PaginasTotales <= 0)? 0: paginacion.PaginaActual;
            return lista;
        }

        public List<TituloLibreriaDetail> SelectPaginacionLikeBy(int? idKey, string idIsbn, string idCiudad, string idEditor, string idResponsable, string idAutor, string tituloOriginal,
                                                    string titulo, string tema, string idSerie, string idResponsableUnico, bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
            SqlParameter[] parametros = new SqlParameter[17];
            if (paginacion == null)
            {
                paginacion = new Paginacion() { FilasPorPagina = 1, PaginaActual = 1 };
            }
            else
            {
                paginacion.PaginaActual = (paginacion.PaginaActual <= 0) ? 1 : paginacion.PaginaActual;
            }
            string query = "spPaginacionLikeTitulosLibreria";
            parametros[0] = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (idKey == null) ? null : (object)idKey.Value, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idIsbn) ? idIsbn : idIsbn.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idCiudad) ? idCiudad : idCiudad.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idEditor) ? idEditor : idEditor.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idResponsable) ? idResponsable : idResponsable.Trim()), IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idAutor) ? idAutor : idAutor.Trim()), IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(tituloOriginal) ? tituloOriginal : tituloOriginal.Trim()), IsNullable = true };
            parametros[7] = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(titulo) ? titulo : titulo.Trim()), IsNullable = true };
            parametros[8] = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idSerie) ? idSerie : idSerie.Trim()), IsNullable = true };
            parametros[9] = new SqlParameter() { ParameterName = "@idResponsableUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)idResponsableUnico, IsNullable = true };
            parametros[10] = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (isNovedad == null) ? null : (object)isNovedad.Value, IsNullable = true };
            parametros[11] = new SqlParameter() { ParameterName = "@orderColumn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)ordenColumna, IsNullable = true };
            parametros[12] = new SqlParameter() { ParameterName = "@isAscendingDirection", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (object)(isOrderDescending == false ? true : false), IsNullable = true };
            parametros[13] = new SqlParameter() { ParameterName = "@PageNumber", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginaActual };
            parametros[14] = new SqlParameter() { ParameterName = "@RowspPage", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasPorPagina };
            parametros[15] = new SqlParameter() { ParameterName = "@numberPages", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginasTotales };
            parametros[16] = new SqlParameter() { ParameterName = "@rowsCount", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasTotales };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TituloLibreriaDetail objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }
            }
            reader.NextResult();
            reader.Close();
            paginacion.PaginasTotales = (int)parametros[15].Value;
            paginacion.FilasTotales = (int)parametros[16].Value;
            paginacion.PaginaActual = (paginacion.PaginasTotales <= 0) ? 0 : paginacion.PaginaActual;
            return lista;
        }

        public List<TituloLibreriaDetail> SelectCatalogoPaginacion(string responsable, string autor, string tituloOriginal, string titulo,  bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
            SqlParameter[] parametros = new SqlParameter[11];
            if (paginacion == null)
            {
                paginacion = new Paginacion() { FilasPorPagina = 1, PaginaActual = 1 };
            }
            else
            {
                paginacion.PaginaActual = (paginacion.PaginaActual <= 0) ? 1 : paginacion.PaginaActual;
            }
            string query = "spPaginacionCatalogoTitulos";            
            parametros[0] = new SqlParameter() { ParameterName = "@responsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)(string.IsNullOrEmpty(responsable) ? responsable : responsable.Trim()), IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@autor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)(string.IsNullOrEmpty(autor) ? autor : autor.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(tituloOriginal) ? tituloOriginal : tituloOriginal.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(titulo) ? titulo : titulo.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (isNovedad == null) ? null : (object)isNovedad.Value, IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@orderColumn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)ordenColumna, IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@isAscendingDirection", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (object)(isOrderDescending == false ? true : false), IsNullable = true };
            parametros[7] = new SqlParameter() { ParameterName = "@PageNumber", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginaActual };
            parametros[8] = new SqlParameter() { ParameterName = "@RowspPage", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasPorPagina };
            parametros[9] = new SqlParameter() { ParameterName = "@numberPages", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.PaginasTotales };
            parametros[10] = new SqlParameter() { ParameterName = "@rowsCount", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Value = paginacion.FilasTotales };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TituloLibreriaDetail objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }
            }
            reader.NextResult();
            reader.Close();
            paginacion.PaginasTotales = (int)parametros[9].Value;
            paginacion.FilasTotales = (int)parametros[10].Value;
            paginacion.PaginaActual = (paginacion.PaginasTotales <= 0) ? 0 : paginacion.PaginaActual;
            return lista;
        }

        public List<TituloLibreriaDetail> SelectCatalogoBusqueda(string responsable, string autor, string tituloOriginal, string titulo, bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
            SqlParameter[] parametros = new SqlParameter[8];
            string query = "spBusquedaCatalogoTitulos";
            parametros[0] = new SqlParameter() { ParameterName = "@responsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)(string.IsNullOrEmpty(responsable) ? responsable : responsable.Trim()), IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@autor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)(string.IsNullOrEmpty(autor) ? autor : autor.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(tituloOriginal) ? tituloOriginal : tituloOriginal.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(titulo) ? titulo : titulo.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (isNovedad == null) ? null : (object)isNovedad.Value, IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@orderColumn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)ordenColumna, IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@isAscendingDirection", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (object)(isOrderDescending == false ? true : false), IsNullable = true };
            parametros[7] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object)top.Value, IsNullable = true };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TituloLibreriaDetail objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }
            }
            reader.NextResult();
            reader.Close();
          
            return lista;
        }

        public List<TituloLibreriaDetail> SelectByTitulos(string titulo, bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            return SelectBy(null, null, null, null, null, null, titulo, titulo, null, null, null,null, ordenColumna, isOrderDescending ,top);
        }

        public List<TituloLibreriaDetail> SelectAll(bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            return SelectBy(null, null, null, null, null, null, null, null, null, null, null, isNovedad, ordenColumna, isOrderDescending, top);
        }

        public List<TituloLibreriaDetail> SelectBy(int? idKey, string idIsbn, string idCiudad, string idEditor, string idResponsable, string idAutor, string tituloOriginal,
                                                    string titulo, string tema, string idSerie, string idResponsableUnico, bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
            SqlParameter[] parametros = new SqlParameter[14];            
            string query = "spBusquedaTitulosLibreria";
            parametros[0] = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (idKey == null) ? null : (object)idKey.Value, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idIsbn) ? idIsbn : idIsbn.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idCiudad) ? idCiudad : idCiudad.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idEditor) ? idEditor : idEditor.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idResponsable) ? idResponsable : idResponsable.Trim()), IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idAutor) ? idAutor : idAutor.Trim()), IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(tituloOriginal) ? tituloOriginal : tituloOriginal.Trim()), IsNullable = true };
            parametros[7] = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(titulo) ? titulo : titulo.Trim()), IsNullable = true };
            parametros[8] = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idSerie) ? idSerie : idSerie.Trim()), IsNullable = true };
            parametros[9] = new SqlParameter() { ParameterName = "@idResponsableUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idResponsableUnico) ? idResponsableUnico : idResponsableUnico.Trim()), IsNullable = true };
            parametros[10] = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (isNovedad == null) ? null : (object)isNovedad.Value, IsNullable = true };
            parametros[11] = new SqlParameter() { ParameterName = "@orderColumn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)ordenColumna, IsNullable = true };
            parametros[12] = new SqlParameter() { ParameterName = "@isAscendingDirection", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (object)(isOrderDescending == false ? true : false), IsNullable = true };
            parametros[13] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object) top.Value, IsNullable = true };
            
            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TituloLibreriaDetail objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }
            }            
            reader.Close();
            return lista;
        }

        public List<TituloLibreriaDetail> SelectLikeBy(int? idKey, string idIsbn, string idCiudad, string idEditor, string idResponsable, string idAutor, string tituloOriginal,
                                                    string titulo, string tema, string idSerie, string idResponsableUnico, bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
            SqlParameter[] parametros = new SqlParameter[14];
            string query = "spBusquedaLikeTitulosLibreria";
            parametros[0] = new SqlParameter() { ParameterName = "@idUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (idKey == null) ? null : (object)idKey.Value, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@idIsbn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idIsbn) ? idIsbn : idIsbn.Trim()), IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@idCiudad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idCiudad) ? idCiudad : idCiudad.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@idEditor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idEditor) ? idEditor : idEditor.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@idRespDetalle", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idResponsable) ? idResponsable : idResponsable.Trim()), IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@idAutor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idAutor) ? idAutor : idAutor.Trim()), IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@tituloOriginal", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(tituloOriginal) ? tituloOriginal : tituloOriginal.Trim()), IsNullable = true };
            parametros[7] = new SqlParameter() { ParameterName = "@titulo", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Value = (object)(string.IsNullOrEmpty(titulo) ? titulo : titulo.Trim()), IsNullable = true };
            parametros[8] = new SqlParameter() { ParameterName = "@idSerie", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50, Value = (object)(string.IsNullOrEmpty(idSerie) ? idSerie : idSerie.Trim()), IsNullable = true };
            parametros[9] = new SqlParameter() { ParameterName = "@idResponsableUnico", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Char, Size = 10, Value = (object)(string.IsNullOrEmpty(idResponsableUnico) ? idResponsableUnico : idResponsableUnico.Trim()), IsNullable = true };
            parametros[10] = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (isNovedad == null) ? null : (object)isNovedad.Value, IsNullable = true };
            parametros[11] = new SqlParameter() { ParameterName = "@orderColumn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)ordenColumna, IsNullable = true };
            parametros[12] = new SqlParameter() { ParameterName = "@isAscendingDirection", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit,  Value = (object) ( isOrderDescending == false ? true : false ), IsNullable = true };
            parametros[13] = new SqlParameter() { ParameterName = "@top", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (top == null) ? null : (object)top.Value, IsNullable = true };

            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TituloLibreriaDetail objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }


        public List<TituloLibreriaDetail> SelectReporteBy(int? anioInicio, int? anioFin, string autor, string tipoTexto, string idFuncionResponsable, string nombreResponsable, string editor, int? edicion, bool? isNovedad, string ordenColumna, bool isOrderDescending)
        {
            List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
            SqlParameter[] parametros = new SqlParameter[11];
            string query = "spReportePublicaciones";
            //parametros[0] = new SqlParameter() { ParameterName = "@anio", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (anio == null) ? null : (object)anio.Value, IsNullable = true };
            parametros[0] = new SqlParameter() { ParameterName = "@anioIni", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (anioInicio == null) ? null : (object)anioInicio.Value, IsNullable = true };
            parametros[1] = new SqlParameter() { ParameterName = "@anioFin", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (anioFin == null) ? null : (object)anioFin.Value, IsNullable = true };
            parametros[2] = new SqlParameter() { ParameterName = "@autor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Value = (object)(string.IsNullOrEmpty(autor) ? autor : autor.Trim()), IsNullable = true };
            parametros[3] = new SqlParameter() { ParameterName = "@tipoTexto", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)(string.IsNullOrEmpty(tipoTexto) ? tipoTexto : tipoTexto.Trim()), IsNullable = true };
            parametros[4] = new SqlParameter() { ParameterName = "@funcionResponsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NChar, Size = 10, Value = (object)idFuncionResponsable, IsNullable = true };
            parametros[5] = new SqlParameter() { ParameterName = "@responsable", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)(string.IsNullOrEmpty(nombreResponsable) ? nombreResponsable : nombreResponsable.Trim()), IsNullable = true };
            parametros[6] = new SqlParameter() { ParameterName = "@editor", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)(string.IsNullOrEmpty(editor) ? editor: editor.Trim()), IsNullable = true };
            parametros[7] = new SqlParameter() { ParameterName = "@numEdicion", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Int, Value = (object) (edicion == null ? edicion : edicion.Value ), IsNullable = true };
            parametros[8] = new SqlParameter() { ParameterName = "@isNovedad", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (isNovedad == null) ? null : (object)isNovedad.Value, IsNullable = true };
            parametros[9] = new SqlParameter() { ParameterName = "@orderColumn", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 200, Value = (object)ordenColumna, IsNullable = true };
            parametros[10] = new SqlParameter() { ParameterName = "@isAscendingDirection", Direction = System.Data.ParameterDirection.Input, SqlDbType = System.Data.SqlDbType.Bit, Value = (object)(isOrderDescending == false ? true : false), IsNullable = true };
            
            SqlDataReader reader = this.SeleccionarReaderArray(query, CommandType.StoredProcedure, parametros, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TituloLibreriaDetail objeto = this.ConvertReaderToTitulo(reader);
                    lista.Add(objeto);
                }
            }
            reader.Close();
            return lista;
        }

        protected TituloLibreriaDetail ConvertReaderToTitulo(SqlDataReader reader) {
            TituloLibreriaDetail objeto = null;
            int intDummy = 0;
            int? intNullable = null;
            string stringDummy = string.Empty;

            if (reader != null) {
                objeto = new TituloLibreriaDetail();
                intDummy = 0;
                intNullable = null;                
                Int32.TryParse(reader["id_unico"].ToString(), out intDummy);
                objeto.IdTitulo = intDummy;
                //objeto.IdIsbn = reader["id_isbn"].ToString();
                intDummy = 0;
                Int32.TryParse((reader["IdTituloIsbn"] != null ? reader["IdTituloIsbn"].ToString():string.Empty), out intDummy);
                objeto.IdTituloIsbn = intDummy;

                intDummy = 0;
                Int32.TryParse((reader["IdTituloIsbn_2"] != null ? reader["IdTituloIsbn_2"].ToString() : string.Empty), out intDummy);
                objeto.IdFkTituloIsbn = intDummy;

                intDummy = 0;
                Int32.TryParse((reader["IdDescripcionIsbn"] != null ? reader["IdDescripcionIsbn"].ToString() : string.Empty), out intDummy);
                objeto.IdDescripcionIsbn = intDummy;
                intDummy = 0;
                Int32.TryParse((reader["ReimpresionIsbn"] != null ? reader["ReimpresionIsbn"].ToString() : string.Empty), out intDummy);
                objeto.ReImpresionIsbn = intDummy;
                intDummy = 0;
                Int32.TryParse((reader["ReedicionIsbn"] != null ? reader["ReedicionIsbn"].ToString() : string.Empty), out intDummy);
                objeto.ReedicionIsbn = intDummy;
                intDummy = 0;
                Int32.TryParse((reader["EdicionIsbn"] != null ? reader["EdicionIsbn"].ToString() : string.Empty), out intDummy);
                objeto.EdicionIsbn = intDummy;
                objeto.DescripcionIsbn = (reader["DescripcionIsbn"] != null ? reader["DescripcionIsbn"].ToString() : string.Empty);
                objeto.ISBN = (reader["ISBN"] != null ? reader["ISBN"].ToString() : string.Empty);                

                objeto.IdCiudad = reader["id_ciudad"].ToString();
                objeto.CiudadDescripcion = reader["CiudadDescripcion"].ToString();
                objeto.IdEditor = reader["id_editor"].ToString();
                objeto.NombreEditor = reader["NombreEditor"].ToString();
                objeto.IdResponsableDetalle = reader["id_responsable"].ToString();
                objeto.IdFuncionResponsable = reader["IdFuncionResponsable"].ToString();
                objeto.DescripcionFuncionResponsable = reader["DescripcionFuncionResponsable"].ToString();
                objeto.OrdenFuncionResponsable = (reader["OrdenFuncionResponsable"] != null ? new int?(Convert.ToInt32(reader["OrdenFuncionResponsable"].ToString())) : null);
                objeto.IdResponsableUnico = reader["IdResponsableUnico"].ToString();
                objeto.RfcResponsable = reader["RfcResponsable"].ToString();
                objeto.NombreResponsable = reader["NombreResponsable"].ToString();
                objeto.ApPaternoResponsable = reader["ApPaternoResponsable"].ToString();
                objeto.ApMaternoResponsable = reader["ApMaternoResponsable"].ToString();
                //objeto.IdFuncionResponsable2 = reader["IdFuncionResponsable2"].ToString();
                //objeto.FuncionResponsable2 = reader["FuncionResponsable2"].ToString();
                objeto.TipoFuncionResponsable = reader["TipoFuncionResponsable"].ToString();
                objeto.IdAutor = reader["id_autores"].ToString();
                objeto.AutorLatin = reader["AutorLatin"].ToString();
                objeto.AutorGriego = reader["AutorGriego"].ToString();
                objeto.AutorEspanol = (reader["AutorEspanol"] != null ? reader["AutorEspanol"].ToString().Trim() : string.Empty);
                objeto.TituloOriginal = reader["titulo_original"].ToString();
                objeto.Titulo = reader["titulo"].ToString();
                objeto.Edicion = reader["edicion"].ToString();
                intDummy = 0;
                Int32.TryParse(reader["alo_pub"].ToString(), out intDummy);
                objeto.AnioPublicacion = intDummy;
                objeto.Paginas = (reader["paginas"] != null ? reader["paginas"].ToString().Trim() : string.Empty); 
                objeto.Medidas = (reader["medidas"] != null ? reader["medidas"].ToString().Trim() : string.Empty);
                objeto.Serie500 = reader["_500_serie"].ToString();
                objeto.Contenido = reader["contenido"].ToString();
                objeto.Cualidades = reader["cualidades"].ToString();
                objeto.Colofon = reader["colofon"].ToString();
                objeto.Tema = reader["tema"].ToString();
                objeto.Secundarias = reader["secundarias"].ToString();
                objeto.UffYL = (reader["U_FFYL"] != null ? reader["U_FFYL"].ToString().Trim() : string.Empty);
                objeto.UiiFL = (reader["U_IIFL"] != null ? reader["U_IIFL"].ToString().Trim() : string.Empty);
                objeto.Observaciones = reader["obs_res"].ToString();
                objeto.IdSerie = reader["id_serie"].ToString();
                objeto.SerieLatin = reader["SerieLatin"].ToString();
                objeto.SerieGriego = reader["SerieGriego"].ToString();
                objeto.RutaArchivo = reader["RutaArchivo"].ToString();
                objeto.NombreArchivo = reader["NombreArchivo"].ToString();
                objeto.IsNovedad = (reader["IsNovedad"] != null ? Convert.ToBoolean(reader["IsNovedad"].ToString()) : false );
                objeto.UrlPdf = reader["UrlLibroPdf"].ToString();
                objeto.UrlVirtual= reader["UrlLibroVirtual"].ToString();
                objeto.UrlOnline= reader["UrlLibroOnline"].ToString();
                objeto.NumeroEdicion = (reader["NumeroEdicion"] != null ? Convert.ToInt32(reader["NumeroEdicion"].ToString()) : 0);
                objeto.NumeroReimpresion = (reader["NumeroReimpresion"] != null ? Convert.ToInt32(reader["NumeroReimpresion"].ToString()) : 0);
                objeto.IsGriego = (reader["IsGriego"] != null ? Convert.ToBoolean(reader["IsGriego"].ToString()) : false);
                objeto.IsLatin = (reader["IsLatin"] != null ? Convert.ToBoolean(reader["IsLatin"].ToString()) : false);

            }
            return objeto;
        }

    }
}
