using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

namespace Unam.CoHu.Libreria.Controller
{
    public class LibreriaController
    {
        protected TitulosLibreriaDb titulosBd = null;
        protected ResponsableTituloDb responsableDetalleBd = null;

        public LibreriaController() {
            titulosBd = new TitulosLibreriaDb();
            responsableDetalleBd = new ResponsableTituloDb(titulosBd.SQLConexion);
        }

        

        public int ActualizarImagen(int id, string rutaFisica, string url, string nombreArchivo, bool createNewFile, byte[] datos, ref string newUrlPostedFile)
        {            
            int rowsAffected = 0;
            int height = 0;
            int width = 0;
            SqlTransaction transaccion = null;
            try
            {
                TituloLibreriaView titulo = this.CargarPorId(id);

                titulosBd.SQLConexion.Open();
                transaccion = titulosBd.SQLConexion.BeginTransaction();
                                
                Image img = null;
                Size finalSize = new Size(0, 0);
                Graphics ImagenGraph = null;
                Bitmap ScaledImage = null;
                if (datos != null & titulo != null) {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(datos, 0, datos.Length);
                        ms.Flush();

                        img = Bitmap.FromStream(ms);
                        height = img.Height;
                        width = img.Width;

                        if (height > 1500 || width > 1500)
                        {
                            double proporcionDivina = 0.0f;
                            // El truco era y es obtener el lado mas pequeño del contenedor sobre el cuál se deberá de obtener el tamaño final de la imagen
                            // En este caso, se obtiene la proporcion mas pequeña de dividir cada lado de la imagen con respecto al tamaño deseado
                            proporcionDivina = Math.Min(1500.0F / (float)img.Height , 1500.00F / (float)img.Width );

                            // Finalmente se obtiene el tamaño escalado del contenedor de la imagen del rompecabezas basado en la proporcion y los lados de la imagen
                            finalSize.Width = (int)(img.Width * proporcionDivina);
                            finalSize.Height = (int)(img.Height * proporcionDivina);                                                                                    
                            //finalSize = img.Size;
                        }
                        else
                        {
                            //imageFinal = new Bitmap(img);
                            finalSize = img.Size;
                        }
                        ms.Close();
                    }
                    ScaledImage = new Bitmap(finalSize.Width, finalSize.Height);
                    ScaledImage.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                    ImagenGraph = Graphics.FromImage(ScaledImage);
                    ImagenGraph.CompositingQuality = CompositingQuality.HighSpeed;
                    ImagenGraph.SmoothingMode = SmoothingMode.HighSpeed;
                    ImagenGraph.InterpolationMode = InterpolationMode.Default;
                    ImagenGraph.DrawImage(img, new Rectangle(0, 0, finalSize.Width , finalSize.Height));
                    //ImagenGraph.ScaleTransform(2.0F, 2.0F);

              
                    ImagenGraph.Save();

                    
                    
                    
                    //imageFinal.Save(ruta + nombreArchivo);
                    //imageFinal.Dispose();
                    //img.Dispose();

                    DirectoryInfo directorio = null;
                    FileInfo[] archivos = null;
                    if (Directory.Exists(rutaFisica))
                    {
                        directorio = new DirectoryInfo(rutaFisica);
                        archivos = directorio.GetFiles();
                    }
                    else {
                        directorio = Directory.CreateDirectory(rutaFisica);
                        archivos = new FileInfo[0];
                    }

                    FileInfo file = null;
                    string urlPath = titulo.RutaArchivo;
                    string realFile = titulo.NombreArchivo;
                    string archivoFisico = string.Empty;
                    if (string.IsNullOrEmpty(urlPath))
                    {
                        Guid guid = new Guid();
                        string[] files = nombreArchivo.Split(new char[] { '.' });
                        if (archivos != null)
                        {
                            do
                            {
                                guid = Guid.NewGuid();
                                file = archivos.Where(s => s.FullName.Contains(guid.ToString())).Select(s => s).FirstOrDefault();
                            } while (file != null);
                        }
                        else
                        {
                            guid = Guid.NewGuid();
                        }
                        realFile = nombreArchivo;
                        urlPath = url + "/" + guid.ToString() + "." + files[files.Length - 1];
                        archivoFisico = guid.ToString() + "." + files[files.Length - 1];
                    }
                    else {
                        string[] rutas = urlPath.Split(new char[] { '/' });

                        if (createNewFile)
                        {
                            Guid guid = new Guid();
                            string[] files = nombreArchivo.Split(new char[] { '.' });
                            if (File.Exists(rutaFisica + @"\" + rutas[rutas.Length - 1])) {
                                FileInfo archivoActual = new FileInfo(rutaFisica + @"\" + rutas[rutas.Length - 1]);
                                archivoActual.Delete();
                            }
                           
                            if (archivos != null)
                            {
                                do
                                {
                                    guid = Guid.NewGuid();
                                    file = archivos.Where(s => s.FullName.Contains(guid.ToString())).Select(s => s).FirstOrDefault();
                                } while (file != null);
                            }
                            else
                            {
                                guid = Guid.NewGuid();
                            }

                            realFile = nombreArchivo;
                            urlPath = url + "/" + guid.ToString() + "." + files[files.Length - 1];
                            archivoFisico = guid.ToString() + "." + files[files.Length - 1];
                            
                        }
                        else {
                            realFile = nombreArchivo;                            
                            archivoFisico = rutas[rutas.Length - 1];
                        }                        
                    }

                    newUrlPostedFile = urlPath;
                    rowsAffected = titulosBd.UpdateFile(id, urlPath, realFile, transaccion);
                    if (rowsAffected > 0)
                    {
                        ScaledImage.Save(rutaFisica + "\\" + archivoFisico, ImageFormat.Jpeg);                                                
                        //imageFinal.Save(rutaFisica + "\\" + archivoFisico);
                        transaccion.Commit();                        
                    }
                    else {
                        transaccion.Rollback();
                    }
                    
                }
                if (ScaledImage != null)
                    ScaledImage.Dispose();

                if (ImagenGraph != null)
                    ImagenGraph.Dispose();

                if (img != null)
                    img.Dispose();
                               
                titulosBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }
                // titulo o responsable
                titulosBd.CloseConnection();
                throw ex;
            }
        }

        public int EliminarImagen(int id, string rutaFisica, bool deleteFile)
        {
            int rowsAffected = 0;
            SqlTransaction transaccion = null;
            DirectoryInfo directorio = null;
            
            try
            {
                TituloLibreriaView titulo = this.CargarPorId(id);

                titulosBd.SQLConexion.Open();
                transaccion = titulosBd.SQLConexion.BeginTransaction();

                if (titulo != null){
                    if (deleteFile)
                    {
                        if (!string.IsNullOrEmpty(rutaFisica)) {
                            if (Directory.Exists(rutaFisica))
                            {
                                directorio = new DirectoryInfo(rutaFisica);
                                FileInfo[] files = directorio.GetFiles();
                                if (files != null)
                                {
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(titulo.RutaArchivo))
                                        {
                                            if (titulo.RutaArchivo.Contains(files[i].Name))
                                            {
                                                files[i].Delete();
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }                        
                    }
                    rowsAffected = titulosBd.UpdateFile(titulo.IdTitulo, null, null, transaccion);
                    if (rowsAffected > 0)
                    {
                        transaccion.Commit();
                    }
                    else
                    {
                        transaccion.Rollback();
                    }                    
                }                                               
                titulosBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }
                titulosBd.CloseConnection();
                throw ex;
            }
        }

        public int Actualizar(TituloLibreria param, List<ResponsableTitulo> listaResponsables, List<Isbn> listaIsbn) {
            SqlTransaction transaccion = null;
            int rowsAffected = 0;
            int rowsResponsables = 0;
            int rowsIsbn = 0;
            try
            {
                titulosBd.OpenConnection();
                transaccion = titulosBd.SQLConexion.BeginTransaction();
                if (listaResponsables== null || listaResponsables.Count <= 0 )
                {
                    throw new ArgumentException("La lista de responsables no puede estar vacía");
                }

                if (listaResponsables[0].IdResponsableDetalle != param.IdResponsableDetalle)
                {
                    throw new ArgumentException("El identificador de detalle responsable no coincide con la lista");
                }

                if (param.IdTitulo<=0)
                {
                    throw new ArgumentException(string.Format("El identificador del Titulo no es válido. '{0}'", param.IdTitulo));
                }

                rowsAffected = responsableDetalleBd.DeleteByIdDetalle(param.IdResponsableDetalle, transaccion);
                if (rowsAffected > 0) {
                    rowsAffected = 0;
                    foreach (ResponsableTitulo item in listaResponsables)
                    {
                        rowsResponsables += responsableDetalleBd.Insert(item, transaccion);
                    }

                    titulosBd.DeleteIsbnByTitulo(param.IdTitulo, transaccion);                    
                    if (listaIsbn != null && listaIsbn.Count > 0)
                    {
                        foreach (Isbn item in listaIsbn)
                        {
                            rowsIsbn += titulosBd.InsertDetalleIsbn(new Isbn(item.IdIsbn, item.IdTitulo, item.ClaveIsbn, item.IdDescripcion, item.DescripcionVersion, item.Reimpresion, item.Reedicion, item.Edicion), transaccion);
                        }
                    }

                    if (rowsResponsables > 0) {
                        rowsAffected= titulosBd.Update(param, transaccion);
                    }

                }


                if (rowsAffected>0) {
                    transaccion.Commit();                    
                }
                else
                {
                    transaccion.Rollback();
                }
                // titulo o responsable
                titulosBd.CloseConnection();
                return rowsAffected;                
            }
            catch (Exception ex)
            {
                if(transaccion!=null)
                    transaccion.Rollback();
                // titulo o responsable
                titulosBd.CloseConnection();
                throw ex;
            }
        }

        public int Registrar(TituloLibreria param, List<ResponsableTitulo> listaResponsables, List<Isbn> listaIsbn)
        {
            SqlTransaction transaccion = null;
            int rowsAffected = 0;
            int rowsIsbn = 0;
            string nuevaClave = string.Empty;
            string mensajeClave = string.Empty;
            try
            {
                titulosBd.OpenConnection();
                transaccion = titulosBd.SQLConexion.BeginTransaction();
                if (listaResponsables == null || listaResponsables.Count <= 0)
                {
                    throw new ArgumentException("La lista de responsables no puede estar vacía");
                }

                bool claveGenerada = responsableDetalleBd.GenerarNuevaClave(ref nuevaClave, ref mensajeClave, transaccion);
                if (!claveGenerada)
                {
                    throw new InvalidOperationException(mensajeClave);
                }

                foreach (ResponsableTitulo item in listaResponsables)
                {
                    item.IdResponsableDetalle = nuevaClave;
                    rowsAffected += responsableDetalleBd.Insert(item, transaccion);
                }

                if (rowsAffected > 0)
                {
                    rowsAffected = 0;
                    param.IdResponsableDetalle = nuevaClave;
                    rowsAffected = titulosBd.Insert(param, transaccion);
                    
                    if (listaIsbn != null && listaIsbn.Count > 0)
                    {
                        foreach (Isbn item in listaIsbn)
                        {
                            rowsIsbn += titulosBd.InsertDetalleIsbn(new Isbn(item.IdIsbn, item.IdTitulo,item.ClaveIsbn,item.IdDescripcion, item.DescripcionVersion, item.Reimpresion, item.Reedicion, item.Edicion), transaccion);
                        }
                    }

                }
                if (rowsAffected > 0)
                {
                    transaccion.Commit();
                }
                else {
                    transaccion.Rollback();
                }
                titulosBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                titulosBd.CloseConnection();
                throw ex;
            }
        }

        public int Borrar(int idKey)
        {
            SqlTransaction transaccion = null;
            int rowsAffected = 0;
            int rowsIsbn = 0;
            try
            {
                TituloLibreriaView titulo = this.CargarPorId(idKey);
                titulosBd.OpenConnection();
                transaccion = titulosBd.SQLConexion.BeginTransaction();
                if (titulo == null)
                {                    
                    return 0;
                }
                rowsAffected =  responsableDetalleBd.DeleteByIdDetalle(titulo.DetalleResponsables[0].IdResponsableDetalle, transaccion);
                if (rowsAffected > 0) {
                    rowsIsbn = titulosBd.DeleteIsbnByTitulo(titulo.IdTitulo, transaccion);
                    rowsAffected = titulosBd.Delete(titulo.IdTitulo, transaccion);
                }

                if (rowsAffected > 0)
                {
                    transaccion.Commit();
                }
                else
                {
                    transaccion.Rollback();
                }
                titulosBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();
                titulosBd.CloseConnection();
                throw ex;
            }
        }


        public List<TituloLibreriaView> PaginarTitulosPor(string isbn, string ciudad, string editor, string responsable, string autor, string serie, bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectPaginacionLikeBy(null, isbn, ciudad, editor, responsable, autor, null, null,null, serie, null, isNovedad, ordenColumna, isOrderDescending, ref paginacion);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TituloLibreriaView> PaginarTitulosCatalogo(string busqueda, bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectCatalogoPaginacion(busqueda, busqueda, busqueda, busqueda, isNovedad, ordenColumna, isOrderDescending, ref paginacion);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TituloLibreriaView> PaginarTitulosPorNombre(string titulo, bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            try
            {               
                List<TituloLibreriaDetail> lista = titulosBd.SelectPaginacionByTitulos(titulo, isNovedad, ordenColumna, isOrderDescending, ref paginacion);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TituloLibreriaView> PaginarTitulosPorTema(string titulo, bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectPaginacionByTitulos(titulo, isNovedad, ordenColumna, isOrderDescending, ref paginacion);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TituloLibreriaView> PaginarTitulos(bool? isNovedad, string ordenColumna, bool isOrderDescending, ref Paginacion paginacion) {
            try
            {                
                List<TituloLibreriaDetail> lista = titulosBd.SelectPaginacionAll(isNovedad, ordenColumna, isOrderDescending, ref paginacion);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public TituloLibreriaView PaginarTituloPorId(int idTitulo, ref Paginacion paginacion)
        {
            try
            {               
                TituloLibreriaDetail resultado = titulosBd.SelectPaginacion(idTitulo, ref paginacion);
                titulosBd.CloseConnection();
                List<TituloLibreriaDetail> lista = new List<TituloLibreriaDetail>();
                if (resultado != null)
                    lista.Add(resultado);
                List<TituloLibreriaView> listaAgrupada = ResultadosTitulosGroupBy(lista, null, false);
                if (listaAgrupada != null && listaAgrupada.Count > 0)
                {
                    return listaAgrupada[0];
                }
                else {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TituloLibreriaView> BuscarTitulosPor(string isbn, string ciudad, string editor, string responsable, string autor, string serie, bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectLikeBy(null, isbn,ciudad, editor, responsable,autor,null,null,null,serie,null, isNovedad, ordenColumna, isOrderDescending, top);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TituloLibreriaView> BuscarTitulosPorNombre(string titulo, bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectByTitulos(titulo, isNovedad, ordenColumna, isOrderDescending, top);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TituloLibreriaView> BuscarTitulosCatalogo(string busqueda, bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectCatalogoBusqueda(busqueda, busqueda, busqueda, busqueda, isNovedad, ordenColumna, isOrderDescending, top);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TituloLibreriaView> CargarTodos(bool? isNovedad, string ordenColumna, bool isOrderDescending, int? top)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectAll(isNovedad, ordenColumna, isOrderDescending, top);
                titulosBd.CloseConnection();
                return ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TituloLibreriaView CargarPorId(int idTitulo)
        {
            try
            {
                List<TituloLibreriaDetail> resultado = titulosBd.SelectBy(idTitulo, null, null, null, null, null, null, null, null, null, null, null, null, false, null);
                titulosBd.CloseConnection();
                List<TituloLibreriaView> listaAgrupada = ResultadosTitulosGroupBy(resultado, null, false);
                if (listaAgrupada != null && listaAgrupada.Count > 0)
                {
                    return listaAgrupada[0];
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<TituloLibreriaView> ResultadosTitulosGroupBy(List<TituloLibreriaDetail> lista, string campoOrdenacion, bool isDescending)
        {
            List<TituloLibreriaView> listaReturn = new List<TituloLibreriaView>();
            string defaultImage = string.Format("{0}/{1}", ConfiguracionSitio.ObtenerVariableConfig("VirtualPathImages"), ConfiguracionSitio.ObtenerVariableConfig("DefaultImage"));
            if (lista != null)
            {                
                IEnumerable<TituloLibreriaView> query = null;
                if (lista != null)
                {
                    query = from q in lista
                            group q by new
                            {
                                q.IdTitulo,
                                //q.IdIsbn,
                                //q.ISBN,
                                q.IdCiudad,
                                q.CiudadDescripcion,
                                q.IdEditor,
                                q.NombreEditor,
                                q.IdAutor,
                                q.AutorLatin,
                                q.AutorGriego,
                                q.AutorEspanol,
                                q.TituloOriginal,
                                q.Titulo,
                                q.Edicion,
                                q.AnioPublicacion,
                                q.Paginas,
                                q.Medidas,
                                q.Serie500,
                                q.Contenido,
                                q.Cualidades,
                                q.Colofon,
                                q.Tema,
                                q.Secundarias,
                                q.UffYL,
                                q.UiiFL,
                                q.Observaciones,
                                q.IdSerie,
                                q.SerieGriego,
                                q.SerieLatin,
                                RutaArchivo = (string.IsNullOrEmpty(q.RutaArchivo)? defaultImage.Trim() : q.RutaArchivo),
                                q.NombreArchivo,
                                q.IsNovedad,
                                q.UrlOnline,
                                q.UrlPdf,
                                q.UrlVirtual,
                                q.NumeroEdicion,
                                q.NumeroReimpresion,
                                q.IsGriego,
                                q.IsLatin
                            }
                               into g
                            select new TituloLibreriaView(g.Key.IdTitulo, g.Key.IdEditor, String.Empty, g.Key.IdAutor, g.Key.TituloOriginal, g.Key.AnioPublicacion, g.Key.IdSerie)
                            {
                                Titulo = g.Key.Titulo,
                                Edicion = g.Key.Edicion,
                                Paginas = g.Key.Paginas,
                                Medidas = g.Key.Medidas,
                                Serie500 = g.Key.Serie500,
                                Contenido = g.Key.Contenido,
                                Cualidades = g.Key.Cualidades,
                                Colofon = g.Key.Colofon,
                                Tema = g.Key.Tema,
                                Secundarias = g.Key.Secundarias,
                                UffYL = g.Key.UffYL,
                                UiiFL = g.Key.UiiFL,
                                Observaciones = g.Key.Observaciones,
                                IdSerie = g.Key.IdSerie,
                                //IdIsbn = g.Key.IdIsbn,
                                IdAutor = g.Key.IdAutor,
                                IdCiudad = g.Key.IdCiudad,
                                RutaArchivo = g.Key.RutaArchivo,
                                NombreArchivo = g.Key.NombreArchivo,
                                IsNovedad = g.Key.IsNovedad,
                                UrlOnline= g.Key.UrlOnline,
                                UrlPdf = g.Key.UrlPdf,
                                UrlVirtual = g.Key.UrlVirtual,
                                NumeroEdicion = g.Key.NumeroEdicion,
                                NumeroReimpresion = g.Key.NumeroReimpresion,
                                IsGriego = g.Key.IsGriego,
                                IsLatin = g.Key.IsLatin,
                                // CatalogoIsbn = (string.IsNullOrEmpty(g.Key.IdIsbn) ? null : new Isbn(g.Key.IdIsbn, g.Key.ISBN)),
                                // El total sin agrupar
                                // DetalleIsbn = g.Select( n => new Isbn(n.IdIsbn, n.ISBN) { DescripcionVersion = n.IdTitulo.ToString() }).ToList(),

                                // Metodos extension: El group by debe tener forzosamente un tipo anonimo y no un "Isbn", para que devuelva la lista de ISBN agrupada
                                /* DetalleIsbn =  Utilidades.LimpiarNulosLista(g.Select( w => new Isbn(w.IdIsbn, w.ISBN)).ToList()
                                                .GroupBy( m => new { m.IdIsbn, m.ClaveIsbn }) // NO debe ser un tipo "new Isbn()"
                                                .Select( r => (string.IsNullOrEmpty(r.Key.IdIsbn) ? null : new Isbn( r.Key.IdIsbn, r.Key.ClaveIsbn)))
                                                .ToList(), false ),*/

                                // Sentencia LINQ: El group by debe tener forzosamente un tipo anonimo y no un "Isbn", para que devuelva la lista de ISBN agrupada
                                /* DetalleIsbn = Utilidades.LimpiarNulosLista(
                                                (from grupoInterno in g
                                                    group grupoInterno by new { grupoInterno.IdIsbn, grupoInterno.ISBN } // NO debe ser un tipo "new Isbn()"
                                                    into g2
                                                    select (string.IsNullOrEmpty(g2.Key.IdIsbn) ? null : new Isbn(g2.Key.IdIsbn, g2.Key.ISBN))).ToList()
                                                , false),
                                */
                                // Metodo Aparte: 
                                DetalleIsbn = ResultadosIsbnGroupBy(g.Select( n=> (n.IdFkTituloIsbn == 0 ? null : new Isbn(n.IdTituloIsbn, n.IdFkTituloIsbn, n.ISBN, n.IdDescripcionIsbn, n.DescripcionIsbn, n.ReImpresionIsbn, n.ReedicionIsbn, n.EdicionIsbn)) ).ToList()),
                                Ciudad = (string.IsNullOrEmpty(g.Key.IdCiudad) ? null : new Ciudad(g.Key.IdCiudad, g.Key.CiudadDescripcion)),
                                Autor = (string.IsNullOrEmpty(g.Key.IdAutor) ? null : new Autor(g.Key.IdAutor, g.Key.AutorLatin) { NombreGriego = g.Key.AutorGriego, NombreEspanol = g.Key.AutorEspanol }),
                                Editor = (string.IsNullOrEmpty(g.Key.IdEditor) ? null : new Editor(g.Key.IdEditor, g.Key.NombreEditor)),
                                Serie = (string.IsNullOrEmpty(g.Key.IdSerie) ? null : new Serie(g.Key.IdSerie, g.Key.SerieLatin, g.Key.SerieGriego)),
                                DetalleResponsables =  ResultadosResponsablesGroupBy(
                                                        Utilidades.LimpiarNulosLista(
                                                            g.Select(n => new ResponsableTituloDetail(n.IdResponsableDetalle, n.IdResponsableUnico, n.OrdenFuncionResponsable) {
                                                                            RfcResponsable = n.RfcResponsable,
                                                                            NombreResponsable = n.NombreResponsable,
                                                                            ApPaternoResponsable = n.ApPaternoResponsable,
                                                                            ApMaternoResponsable = n.ApMaternoResponsable,
                                                                            DescripcionFuncion = n.DescripcionFuncionResponsable,
                                                                            IdFuncion = n.IdFuncionResponsable,
                                                                            OrdenFuncion = n.OrdenFuncionResponsable,
                                                                            TipoFuncion = n.TipoFuncionResponsable
                                                            }).ToList(), false)
                                                )

                                //Responsables = Utilidades.LimpiarNulosLista<ResponsableView>(g.Select(n => new ResponsableView() {  = n.IdResponsableDetalle, IdFuncion = n.IdFuncionResponsable,  }).ToList(), false)
                            };
                    //listaReturn = query.ToList();
                    listaReturn = TitulosOrderBy(query.ToList(), campoOrdenacion, isDescending);
                }
            }            
            return listaReturn;
        }

        public static List<TituloLibreriaView> TitulosOrderBy(List<TituloLibreriaView> listaTitulos, string campoOrdenacion, bool isDescending) {
            List<TituloLibreriaView> listaReturn = null;
            if (listaTitulos != null)
            {
                campoOrdenacion = (string.IsNullOrEmpty(campoOrdenacion) ? string.Empty : campoOrdenacion);
                switch (campoOrdenacion)
                {
                    case "ciudad":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => (n.Ciudad != null ? n.Ciudad.Descripcion : string.Empty)).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => (n.Ciudad != null ? n.Ciudad.Descripcion : string.Empty)).ToList();
                        }
                        break;
                    case "editor":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => (n.Editor != null ? n.Editor.Nombre : string.Empty)).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => (n.Editor != null ? n.Editor.Nombre : string.Empty)).ToList();
                        }
                        break;
                    case "edicion":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => n.Edicion).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => n.Edicion).ToList();
                        }
                        break;
                    case "autor":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => (n.Autor != null ? n.Autor.NombreEspanol : string.Empty)).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => (n.Autor != null ? n.Autor.NombreEspanol : string.Empty)).ToList();
                        }
                        break;
                    case "tituloOriginal":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderBy(n => n.TituloOriginal).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => n.TituloOriginal).ToList();
                        }
                        break;
                    case "titulo":
                        if (isDescending)
                        {
                            //listaReturn = listaTitulos.OrderByDescending(n => (string.IsNullOrEmpty(n.Titulo)? string.Empty:n.Titulo.ToLower())).ToList();
                            listaReturn = listaTitulos.OrderByDescending(n => n.Titulo).ToList();
                        }
                        else
                        {
                            //listaReturn = listaTitulos.OrderBy(n => (string.IsNullOrEmpty(n.Titulo) ? string.Empty : n.Titulo.ToLower())).ToList();
                            listaReturn = listaTitulos.OrderBy(n => n.Titulo).ToList();
                        }
                        break;
                    case "año":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => n.AnioPublicacion).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => n.AnioPublicacion).ToList();
                        }
                        break;
                    case "tema":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => n.Tema).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => n.Tema).ToList();
                        }
                        break;
                    case "u_ffyl":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderBy(n => n.UffYL).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => n.UffYL).ToList();
                        }
                        break;
                    case "u_iifl":
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => n.UiiFL).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => n.UiiFL).ToList();
                        }
                        break;
                    default:
                        if (isDescending)
                        {
                            listaReturn = listaTitulos.OrderByDescending(n => n.IdTitulo).ToList();
                        }
                        else
                        {
                            listaReturn = listaTitulos.OrderBy(n => n.IdTitulo).ToList();
                        }
                        break;
                }
            }
            return listaReturn;
        }

        public List<ResponsableTituloDetail> CargarResponsablesTitulo(int idTitulo) {
            TituloLibreriaView titulo = this.CargarPorId(idTitulo);
            List<ResponsableTituloDetail> detalle = new List<ResponsableTituloDetail>();            

            if (titulo != null) {                
                if (titulo.DetalleResponsables != null && titulo.DetalleResponsables.Count > 0)
                {
                    foreach (ResponsableTituloView item in titulo.DetalleResponsables)
                    {
                        foreach (ResponsableTituloDetail Subitem in item.Responsables)
                        {
                            detalle.Add(Subitem);
                        }
                    }
                }

            }
            return detalle;
        }

        public List<ResponsableTituloView> CargarResponsables()
        {
            try
            {
                List<ResponsableTituloDetail> lista = responsableDetalleBd.SelectAll(null);
                responsableDetalleBd.CloseConnection();
                return ResultadosResponsablesGroupBy(lista);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResponsableTituloView CargarResponsableDetallePorId(string idResponsable)
        {
            ResponsableTituloView responsable = null;
            try
            {
                List<ResponsableTituloDetail> lista = responsableDetalleBd.Select(idResponsable);
                responsableDetalleBd.CloseConnection();
                List<ResponsableTituloView> listaAgrupada = ResultadosResponsablesGroupBy(lista);
                if (listaAgrupada != null && listaAgrupada.Count > 0)
                {
                    responsable = listaAgrupada[0];
                }
                return responsable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public static List<ResponsableTituloView> ResultadosResponsablesGroupBy(List<ResponsableTituloDetail> lista)
        {
            List<ResponsableTituloView> listaReturn = new List<ResponsableTituloView>();
            if (lista != null)
            {
                if (lista != null)
                {
                    var query = from q in lista
                                group q by new
                                {
                                    q.IdResponsableDetalle
                                }
                               into g
                                select new ResponsableTituloView()
                                {
                                    IdResponsableDetalle = g.Key.IdResponsableDetalle,
                                    Responsables = Utilidades.LimpiarNulosLista<ResponsableTituloDetail>(
                                         g.GroupBy(n => new { n.IdResponsableDetalle, n.IdResponsable, n.IdFuncion, n.DescripcionFuncion, n.OrdenFuncion, n.NombreResponsable, n.ApPaternoResponsable, n.ApMaternoResponsable, n.RfcResponsable, n.TipoFuncion})
                                        .Select(n => {
                                            return (String.IsNullOrEmpty(n.Key.IdResponsable) ? null : new ResponsableTituloDetail(n.Key.IdResponsableDetalle, n.Key.IdResponsable, n.Key.IdFuncion, n.Key.DescripcionFuncion, n.Key.OrdenFuncion)
                                            {
                                                NombreResponsable = n.Key.NombreResponsable,
                                                ApPaternoResponsable = n.Key.ApPaternoResponsable,
                                                ApMaternoResponsable = n.Key.ApMaternoResponsable,
                                                RfcResponsable = n.Key.RfcResponsable,
                                                TipoFuncion = n.Key.TipoFuncion
                                            });
                                        }).ToList(), false)
                                };
                    listaReturn = query.ToList();
                }
            }

            return listaReturn;
        }

        public static List<Isbn> ResultadosIsbnGroupBy(List<Isbn> lista)
        {
            List<Isbn> listaReturn = new List<Isbn>();
            if (lista != null && lista.Count > 0)
            {
                // Sentencia LINQ: El group by debe tener forzosamente un tipo anonimo y no un "Isbn", para que devuelva la lista de ISBN agrupada
                lista = Utilidades.LimpiarNulosLista(lista, false);
                if (lista.Count > 0) {
                    /*var query = from q in lista
                            group q by new { q.IdIsbn , q.ClaveIsbn } // NO debe ser un tipo "new Isbn()"
                           into g
                            select (string.IsNullOrEmpty(g.Key.IdIsbn) ?  null : new Isbn(g.Key.IdIsbn, g.Key.ClaveIsbn) );
                    listaReturn = Utilidades.LimpiarNulosLista(query.ToList(), false);
                    */

                    // Metodos de Extension: El group by debe tener forzosamente un tipo anonimo y no un "Isbn", para que devuelva la lista de ISBN agrupada
                    listaReturn = lista.GroupBy(m => new { m.IdIsbn, m.IdTitulo, m.IdDescripcion, m.DescripcionVersion, m.Reedicion, m.Reimpresion, m.Edicion , m.ClaveIsbn })
                                                     .Select(r => new Isbn(r.Key.IdIsbn, r.Key.IdTitulo, r.Key.ClaveIsbn, r.Key.IdDescripcion, r.Key.DescripcionVersion, r.Key.Reimpresion, r.Key.Reedicion, r.Key.Edicion)).ToList();
                }
            }

            return listaReturn;
        }

        public static string ObtenerNumeroReimpresion(int numeroImpresion) {
            string unidad = string.Empty;
            string decena = string.Empty;
            string resultado = "{0} Reimpresión";

            if (numeroImpresion <= 0)
            {
                resultado = string.Empty;
            }
            else if (numeroImpresion >= 1 & numeroImpresion < 10)
            {
                unidad = Unidades(numeroImpresion);
                resultado = string.Format("{0} Reimpresión", unidad);
            }
            else if (numeroImpresion >= 10 & numeroImpresion < 100)
            {
                double numero = ((double)numeroImpresion / 100.00D);
                string charNumero = (numero * 10.00D).ToString();
                string[] numeros = charNumero.Split(new char[] { '.' });
                string cadena = string.Empty;
                int enteroUnidad = 0;
                int enteroDecena = 0;
                if (numeros.Length > 1)
                {
                    cadena = numeros[1];
                    Int32.TryParse(cadena, out enteroUnidad);
                }
                unidad = Unidades(enteroUnidad);
                enteroDecena = (int)Math.Floor(numero * 10) * 10;
                decena = Decenas(enteroDecena);

                resultado = string.Format("{0} {1} Reimpresión", decena, unidad.ToLower());
            }
            else
            {
                resultado = string.Format("Reimpresión # {0}", numeroImpresion);
            }
            return resultado;
        }

        public static string ObtenerNumeroEdicion(int numeroEdicion)
        {
            string unidad = string.Empty;
            string decena = string.Empty;
            string resultado = "{0} Edición";

            if (numeroEdicion <= 0)
            {
                resultado = string.Empty;
            }
            else if (numeroEdicion >= 1 & numeroEdicion < 10)
            {
                unidad = Unidades(numeroEdicion);
                resultado = string.Format("{0} Edición", unidad);
            }
            else if (numeroEdicion >= 10 & numeroEdicion < 100)
            {
                double numero = ((double)numeroEdicion / 100.00D);
                string charNumero = (numero * 10.00D).ToString();
                string[] numeros = charNumero.Split(new char[] { '.' });
                string cadena = string.Empty;
                int enteroUnidad = 0;
                int enteroDecena = 0;
                if (numeros.Length > 1)
                {
                    cadena = numeros[1];
                    Int32.TryParse(cadena, out enteroUnidad);                    
                }
                unidad = Unidades(enteroUnidad);
                enteroDecena = (int) Math.Floor(numero * 10) * 10 ;
                decena = Decenas(enteroDecena);

                resultado = string.Format("{0} {1} Edición", decena, unidad.ToLower());
            }
            else
            {
                resultado = string.Format("Edición # {0}", numeroEdicion);
            }
            return resultado;
        }

        private static string Unidades(int idUnidad) {
            string unidad = string.Empty;
            switch (idUnidad)
            {
                case 1:
                    unidad = "Primera";
                    break;
                case 2:
                    unidad = "Segunda";
                    break;
                case 3:
                    unidad = "Tercera";
                    break;
                case 4:
                    unidad = "Cuarta";
                    break;
                case 5:
                    unidad = "Quinta";
                    break;
                case 6:
                    unidad = "Sexta";
                    break;
                case 7:
                    unidad = "Séptima";
                    break;
                case 8:
                    unidad = "Octava";
                    break;
                case 9:
                    unidad = "Novena";
                    break;
                default:
                    break;
            }
            return unidad;
        }

        private static string Decenas(int idUnidad)
        {
            string decena = string.Empty;
            switch (idUnidad)
            {
                case 10:
                    decena = "Décima";
                    break;
                case 20:
                    decena = "Vigésima";
                    break;
                case 30:
                    decena = "Trigésima";
                    break;
                case 40:
                    decena = "Cuadragésima";
                    break;
                case 50:
                    decena = "Quincuagésima";
                    break;
                case 60:
                    decena = "Sexagésima";
                    break;
                case 70:
                    decena = "Septuagésima";
                    break;
                case 80:
                    decena = "Octogésima";
                    break;
                case 90:
                    decena = "Nonagésima";
                    break;
                default:
                    break;
            }
            return decena;
        }       


        //  TABLA TRANSACCIONAL RELACION N TITULOS  - N RESPONSABLES

        private int ActualizarResponsableDetalle(ResponsableTitulo anterior, ResponsableTitulo nuevo)
        {
            try
            {
                int rowsAffected = responsableDetalleBd.Update(anterior, nuevo, null);
                responsableDetalleBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CrearResponsableDetalle(ResponsableTitulo param)
        {
            try
            {
                int rowsAffected = responsableDetalleBd.Insert(param, null);
                responsableDetalleBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int BorrarResponsableDetalle(ResponsableTitulo param)
        {
            try
            {
                int rowsAffected = responsableDetalleBd.Delete(param, null);
                responsableDetalleBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
