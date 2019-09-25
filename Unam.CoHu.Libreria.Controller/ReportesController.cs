using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;
using EPPlus;
using System.IO;
using System.Drawing;
using EPPlus.Style;
using Unam.CoHu.Libreria.Controller.Enums;
using Unam.CoHu.Libreria.Controller.Catalogos;

namespace Unam.CoHu.Libreria.Controller
{
    public class ReportesController
    {
        protected TitulosLibreriaDb titulosBd = null;
        private Color colorAzulPar = Color.FromArgb(250, 247, 247);
        private Color colorAzulImpar = Color.FromArgb(250, 247, 247);
        private Color colorAzul = Color.FromArgb(8, 75, 138);
        private Color colorFondoBlanco = Color.White;
        private Color colorFondoNegro = Color.Black;

        public ReportesController()
        {
            titulosBd = new TitulosLibreriaDb();
        }


        public List<TituloLibreriaView> ReportePublicacionesPor(int? anioInicio, int? anioFin, string autor, string tipoTexto, string idFuncionResponsable, string editor, int? edicion, bool? isNovedad, string ordenColumna, bool isOrderDescending)
        {
            try
            {
                List<TituloLibreriaDetail> lista = titulosBd.SelectReporteBy(anioInicio, anioFin, autor, tipoTexto,idFuncionResponsable, null, editor, edicion, isNovedad, ordenColumna, isOrderDescending);
                titulosBd.CloseConnection();
                return LibreriaController.ResultadosTitulosGroupBy(lista, ordenColumna, isOrderDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TituloLibreriaView> ReportePublicacionesPor(TipoReportePublicacion tipoReporte, string campoBusqueda, string rangoinicio, string rangoFin, string nombreFuncion,bool? isNovedad, string ordenColumna, bool isOrderDescending, ref bool isLoadedAll)
        {
            List<TituloLibreriaDetail> lista = null;
            string campoOrdenacion = (string.IsNullOrEmpty(ordenColumna) ? ReportesController.GetCampoOrdenacionReporte(tipoReporte): ordenColumna);
            nombreFuncion = (string.IsNullOrEmpty(nombreFuncion) ? string.Empty : nombreFuncion.ToLower());
            try
            {
                if ((string.IsNullOrEmpty(campoBusqueda) &  (tipoReporte != TipoReportePublicacion.Anio & tipoReporte != TipoReportePublicacion.Traductor )) || (string.IsNullOrEmpty(rangoinicio) & string.IsNullOrEmpty(rangoFin) & tipoReporte == TipoReportePublicacion.Anio))
                {
                    lista = titulosBd.SelectReporteBy(null, null, null, null, null,null, null, null, isNovedad, campoOrdenacion, isOrderDescending);
                    isLoadedAll = true;
                }
                else {
                    isLoadedAll = false;
                    switch (tipoReporte)
                    {
                        case TipoReportePublicacion.Anio:
                            int intDummy = -1;
                            int? anioInicio = null;
                            int? anioFin = null;

                            Int32.TryParse(rangoinicio, out intDummy);
                            if (intDummy == -1)
                            {
                                anioInicio = new int?(intDummy);
                            }
                            else
                            {
                                anioInicio = (intDummy == 0 ? null : new int?(intDummy));
                            }


                            intDummy = -1;
                            Int32.TryParse(rangoFin, out intDummy);

                            if (intDummy == -1)
                            {
                                anioFin = new int?(intDummy);
                            }
                            else
                            {
                                anioFin = (intDummy == 0 ? null : new int?(intDummy));
                            }

                            lista = titulosBd.SelectReporteBy(anioInicio, anioFin, null, null, null, null, null, null, isNovedad, campoOrdenacion, isOrderDescending);
                            break;
                        case TipoReportePublicacion.Autor:
                            lista = titulosBd.SelectReporteBy(null, null, campoBusqueda, null, null,null, null, null, isNovedad, campoOrdenacion, isOrderDescending);
                            break;
                        case TipoReportePublicacion.TipoTexto:
                            lista = titulosBd.SelectReporteBy(null, null, null, campoBusqueda, null, null, null, null, isNovedad, campoOrdenacion, isOrderDescending);
                            break;
                        case TipoReportePublicacion.Traductor:
                            if (string.IsNullOrEmpty(nombreFuncion))
                            {
                                lista = titulosBd.SelectReporteBy(null, null, null, null, null, campoBusqueda, null, null, isNovedad, campoOrdenacion, isOrderDescending);
                            }
                            else {
                                FuncionesController funcDb = new FuncionesController();
                                Funcion func = funcDb.CargarPorId(nombreFuncion);                                
                                if (func != null)
                                    lista = titulosBd.SelectReporteBy(null, null, null, null, func.IdFuncion, campoBusqueda, null, null, isNovedad, campoOrdenacion, isOrderDescending);
                            }
                                                        
                            break;
                        case TipoReportePublicacion.Editorial:
                            lista = titulosBd.SelectReporteBy(null, null, null, null, null, null, campoBusqueda, null, isNovedad, campoOrdenacion, isOrderDescending);
                            break;
                        case TipoReportePublicacion.Edicion:                            
                            int numero = 0;
                            Int32.TryParse(campoBusqueda, out numero);
                            int? edicion = (numero <= 0 ? null: new int?(numero));
                            lista = titulosBd.SelectReporteBy(null, null, null, null, null, null, null, edicion, isNovedad, campoOrdenacion, isOrderDescending);
                            break;
                        default:
                            lista = null;
                            break;
                    }
                }
                                
                titulosBd.CloseConnection();
                return LibreriaController.ResultadosTitulosGroupBy(lista, campoOrdenacion, isOrderDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public bool GenerateExcel(TipoReportePublicacion tipoReporte, ref MemoryStream streamReturn, System.Drawing.Image imagen, List<TituloLibreriaView> listaDatos, ref string mensajeRetorno)
        {
            DateTime fechaActual = DateTime.Now;
            int rowActual = 5;
            int initialRow = rowActual;
            mensajeRetorno = string.Empty;
            bool retorno = false;
            try
            {
                using (var xlsPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = xlsPackage.Workbook.Worksheets.Add("Publicaciones ");
                    // Accediendo a las celdas. Al parecer el acceso a las celdas es a partir del indice = 1 => (1,1), no (0,0)
                    worksheet.Cells.Style.Font.Size = 10;
                    // (1, 3)
                    worksheet.Cells[1, 1].Value = "Catálogo "+ GetNombreTipoReporte(tipoReporte) ;
                    // (2, 3)
                    worksheet.Cells[2, 1].Value = "Fecha: " + fechaActual.ToString("dd/MM/yyyy");

                    // ' IMAGEN - MERGE
                    // Dim imagenExcel As EPPlus.Drawing.ExcelPicture = worksheet.Drawings.AddPicture("Image", imagen)
                    // imagenExcel.SetSize(imagen.Width / 1.5, imagen.Height / 1.5)
                    // ' Aparentemente, aquí si se puede asignar las columnas 0 y filas 0
                    // imagenExcel.From.Column = 0
                    // imagenExcel.From.Row = 0
                    // imagenExcel.To.Column = 2
                    // imagenExcel.To.Row = 2                
                    // ' Asignando un Merge a un rango (FromRow, FromCol, ToRow, ToCol)                 
                    worksheet.Cells[1, 1, 1, 3].Merge = true;
                    worksheet.Cells[2, 1, 2, 3].Merge = true;

                    // Estilo a la cabecera del Titulo Reporte
                    using (var rangeTitle = worksheet.Cells[1, 1, 2, 3])
                    {
                        rangeTitle.Style.Fill.PatternType = ExcelFillStyle.None;
                        rangeTitle.Style.Font.Color.SetColor(colorFondoNegro);
                        rangeTitle.Style.Font.Size = 10;
                        rangeTitle.Style.Font.Bold = true;
                    }

                    // Cabeceras de la tabla
                    AddCellXLSHeader(worksheet, rowActual);

                    if (listaDatos != null && listaDatos.Count > 0)
                    {
                        string nombre = string.Empty;
                        foreach (TituloLibreriaView item in listaDatos)
                        {                            
                            worksheet.Cells[rowActual, 1].Value = item.Titulo;
                            worksheet.Cells[rowActual, 2].Value = item.TituloOriginal;
                            int isbns = 1;
                            int responsables = 1;

                            if (item.Autor != null) {
                                nombre = item.Autor.NombreEspanol;
                                //if (!string.IsNullOrEmpty(item.Autor.NombreLatin))
                                //{
                                //    nombre = nombre + "(" + item.Autor.NombreLatin + ")";
                                //}
                                //else if (!string.IsNullOrEmpty(item.Autor.NombreGriego))
                                //{
                                //    nombre = nombre + "(" + item.Autor.NombreGriego + ")";
                                //}
                            } else {
                                nombre = string.Empty;
                            }
                            worksheet.Cells[rowActual, 3].Value = nombre;
                            worksheet.Cells[rowActual, 4].Value = item.AnioPublicacion;

                            string cadenaResp = string.Empty;

                            if (item.DetalleResponsables != null && item.DetalleResponsables.Count > 0)
                            {                                
                                var detalle = item.DetalleResponsables[0];
                                if (detalle.Responsables != null && detalle.Responsables.Count > 0) {
                                    responsables = detalle.Responsables.Count;

                                    for (int i = 0; i < detalle.Responsables.Count; i++)
                                    {
                                        cadenaResp = cadenaResp + string.Format("{0} ({1}) ",detalle.Responsables[i].NombreCompletoResponsable, detalle.Responsables[i].TipoFuncion) + "\n";
                                    }
                                    worksheet.Cells[rowActual, 5].Value = cadenaResp;
                                    worksheet.Cells[rowActual, 5].Style.WrapText = true;
                                }                                
                            }

                            worksheet.Cells[rowActual, 6].Value = (item.Editor != null ? item.Editor.Nombre : string.Empty);
                            worksheet.Cells[rowActual, 7].Value = item.Edicion;
                            worksheet.Cells[rowActual, 8].Value = item.NumeroEdicion;
                            worksheet.Cells[rowActual, 9].Value = item.NumeroReimpresion;
                            worksheet.Cells[rowActual, 10].Value = (item.Ciudad != null ? item.Ciudad.Descripcion : string.Empty);

                            string cadenaIsbn = string.Empty;

                            if (item.DetalleIsbn != null && item.DetalleIsbn.Count > 0) {                                
                                for (int i = 0; i < item.DetalleIsbn.Count; i++)
                                {
                                    string edicion = (item.DetalleIsbn[i].Edicion > 0) ? ", " + item.DetalleIsbn[i].Edicion + "a." + " ed." : "";
                                    string reedicion = (item.DetalleIsbn[i].Reedicion > 0) ? ", " + item.DetalleIsbn[i].Reedicion + "a." + " reed." : "";
                                    string reimp = (item.DetalleIsbn[i].Reimpresion > 0) ? ", " + item.DetalleIsbn[i].Reimpresion + "a." + " reimpr." : "";
                                    string ediciones = edicion + reedicion + reimp;

                                    if (item.DetalleIsbn[i].IdDescripcion > 0)
                                    {
                                        cadenaIsbn += item.DetalleIsbn[i].ClaveIsbn + " (" + item.DetalleIsbn[i].DescripcionVersion + ediciones + ")" + "\n";
                                    }
                                    else
                                    {
                                        cadenaIsbn += item.DetalleIsbn[i].ClaveIsbn + (ediciones.Length > 0 ? "(" + ediciones + ")" : "") + "\n";
                                    }
                                }
                                isbns = item.DetalleIsbn.Count;
                                worksheet.Cells[rowActual, 11].Value = cadenaIsbn;
                                worksheet.Cells[rowActual, 11].Style.WrapText = true;
                            }
                                                                                                                
                            worksheet.Cells[rowActual, 12].Value = item.Medidas;
                            worksheet.Cells[rowActual, 13].Value = item.Paginas;
                            worksheet.Cells[rowActual, 14].Value = item.Observaciones; //string.Format("{0:dd/MM/yyyy}", item.FechaAlta);
                            worksheet.Cells[rowActual, 15].Value = item.Secundarias;
                            if (item.Serie != null)
                            {
                                nombre = (string.IsNullOrEmpty(item.Serie.NombreLatin) ? item.Serie.NombreLatin : item.Serie.NombreGriego);
                            }
                            else
                            {
                                nombre = string.Empty;
                            }

                            worksheet.Cells[rowActual, 16].Value = nombre;
                            worksheet.Cells[rowActual, 17].Value = item.Serie500;
                            worksheet.Cells[rowActual, 18].Value = item.Tema;
                            worksheet.Cells[rowActual, 19].Value = item.Colofon;
                            worksheet.Cells[rowActual, 20].Value = item.Cualidades;
                            worksheet.Cells[rowActual, 21].Value = item.Contenido;
                            worksheet.Cells[rowActual, 22].Value = item.UffYL;
                            worksheet.Cells[rowActual, 23].Value = item.UiiFL;
                            worksheet.Cells[rowActual, 24].Value = (item.IsNovedad? "SI":"NO");

                            for (int i = 1; i < 25; i++)
                            {
                                SetStyleCellXLS(worksheet.Cells[rowActual, i], rowActual);
                            }

                            int maxValue = Math.Max(isbns, responsables);
                            worksheet.Row(rowActual).Height = 18.0 * maxValue;                            
                            rowActual = rowActual + 1;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < 25; i++)
                        {
                            worksheet.Cells[rowActual, i].Value = "EMPTY";
                        }
                    }

                    worksheet.Column(1).Width = 35;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 35;
                    worksheet.Column(4).Width = 10;
                    worksheet.Column(5).Width = 35;
                    worksheet.Column(6).Width = 35;
                    worksheet.Column(7).Width = 20;
                    worksheet.Column(8).Width = 20;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(11).Width = 23;
                    worksheet.Column(12).Width = 20;
                    worksheet.Column(13).Width = 20;
                    worksheet.Column(14).Width = 20;
                    worksheet.Column(15).Width = 20;
                    worksheet.Column(16).Width = 20;
                    worksheet.Column(17).Width = 20;
                    worksheet.Column(18).Width = 35;
                    worksheet.Column(19).Width = 35;
                    worksheet.Column(20).Width = 35;
                    worksheet.Column(21).Width = 35;
                    worksheet.Column(22).Width = 20;
                    worksheet.Column(23).Width = 20;
                    worksheet.Column(24).Width = 10;

                    //worksheet.Cells.AutoFitColumns();
                    xlsPackage.Workbook.Properties.Title = GetNombreTipoReporte(tipoReporte);
                    xlsPackage.Workbook.Properties.Author = "Coordinación de Humanidades";
                    xlsPackage.Workbook.Properties.Company = "Universidad Nacional Autónoma de México";
                    xlsPackage.Stream.Flush();
                    // No debe guardarse, ,debido a que se usará para leer los bytes y escribirlos en el Stream de Response
                    // xlsPackage.Save()  
                    streamReturn = new MemoryStream();
                    byte[] bytes = xlsPackage.GetAsByteArray();
                    streamReturn.Write(bytes, 0, bytes.Length);
                    // No es necesario reiniciar la posicion
                    // streamReturn.Position = 0                
                    xlsPackage.Stream.Close();
                    mensajeRetorno = "Se ha generado el documento solicitado.";
                }
                retorno = true;
                mensajeRetorno = "Archivo generado";
            }
            catch (Exception ex)
            {
                streamReturn = null;
                mensajeRetorno = ex.Message;
                mensajeRetorno = mensajeRetorno + @":\n\r" + ex.StackTrace;
                retorno = false;
            }
            return retorno;
        }

        protected void AddCellXLSHeader(ExcelWorksheet worksheet, int rowActual)
        {
            // Cabeceras
            worksheet.Cells[rowActual - 1, 1].Value = "TITULO";
            worksheet.Cells[rowActual - 1, 2].Value = "TITULO ORIGINAL";
            worksheet.Cells[rowActual - 1, 3].Value = "AUTOR";            
            worksheet.Cells[rowActual - 1, 4].Value = "AÑO";
            worksheet.Cells[rowActual - 1, 5].Value = "RESPONSABLES";
            worksheet.Cells[rowActual - 1, 6].Value = "EDITOR";
            worksheet.Cells[rowActual - 1, 7].Value = "EDICION";
            worksheet.Cells[rowActual - 1, 8].Value = "NO. EDICIÓN";
            worksheet.Cells[rowActual - 1, 9].Value = "NO. REIMPRESIÓN";
            worksheet.Cells[rowActual - 1, 10].Value = "CIUDAD";
            worksheet.Cells[rowActual - 1, 11].Value = "ISBN";            
            worksheet.Cells[rowActual - 1, 12].Value = "MEDIDAS";
            worksheet.Cells[rowActual - 1, 13].Value = "PAGINAS";
            worksheet.Cells[rowActual - 1, 14].Value = "OBSERVACIONES";
            worksheet.Cells[rowActual - 1, 15].Value = "sECUNDARIAS";
            worksheet.Cells[rowActual - 1, 16].Value = "SERIE";
            worksheet.Cells[rowActual - 1, 17].Value = "SERIE 500";
            worksheet.Cells[rowActual - 1, 18].Value = "TEMA";
            worksheet.Cells[rowActual - 1, 19].Value = "COLOFON";
            worksheet.Cells[rowActual - 1, 20].Value = "CUALIDADES";
            worksheet.Cells[rowActual - 1, 21].Value = "CONTENIDO";
            worksheet.Cells[rowActual - 1, 22].Value = "U_YFFL";
            worksheet.Cells[rowActual - 1, 23].Value = "U_IIFL";
            worksheet.Cells[rowActual - 1, 24].Value = "NOVEDAD?";

            // Cabecera estilo
            using (var rangeHeader = worksheet.Cells[rowActual - 1, 1, rowActual - 1, 24])
            {
                rangeHeader.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rangeHeader.Style.Fill.BackgroundColor.SetColor(colorAzul);
                rangeHeader.Style.Font.Color.SetColor(colorFondoBlanco);
                rangeHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rangeHeader.Style.Font.Bold = true;
            }            
        }

        protected void SetStyleCellXLS(ExcelRange celda, int indice)
        {
            celda.Style.Fill.PatternType = ExcelFillStyle.None;
            // If indice Mod 2 = 0 Then                
            celda.Style.Font.Bold = false;
            celda.Style.ShrinkToFit = false;
            celda.Style.Numberformat = null;
            if (indice != 5 & indice != 9 & indice != 12 & indice != 17 & indice != 18 & indice != 19 )
            {
                celda.AutoFitColumns();
            }
        }

        protected void SetStyleStatusCellXLS(ExcelRange celda)
        {
            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //celda.Style.Fill.BackgroundColor.SetColor(GetColorBaseFromStatus(estatus));
            celda.Style.Font.Bold = false;
            celda.Style.ShrinkToFit = false;
            celda.Style.Numberformat = null;
        }

        public static TipoReportePublicacion GetTipoReporte(string tipoReporte)
        {
            TipoReportePublicacion tipo = TipoReportePublicacion.Autor;
            tipoReporte = (string.IsNullOrEmpty(tipoReporte) ? string.Empty : tipoReporte.ToLower());
            switch (tipoReporte)
            {
                case "year":
                    tipo = TipoReportePublicacion.Anio;
                    break;
                case "autor":
                    tipo = TipoReportePublicacion.Autor;
                    break;
                case "tipotexto":
                    tipo = TipoReportePublicacion.TipoTexto;
                    break;
                case "traductor":
                    tipo = TipoReportePublicacion.Traductor;
                    break;
                case "edicion":
                    tipo = TipoReportePublicacion.Edicion;
                    break;
                case "editorial":
                    tipo = TipoReportePublicacion.Editorial;
                    break;
                default:
                    tipo = TipoReportePublicacion.Autor;
                    break;
            }
            return tipo;
        }

        public static string GetNombreTipoReporte(TipoReportePublicacion tipoReporte)
        {
            string nombreReporte = string.Empty;
            switch (tipoReporte)
            {
                case TipoReportePublicacion.Anio:
                    nombreReporte = "Pulicaciones por Año";
                    break;
                case TipoReportePublicacion.Autor:
                    nombreReporte = "Pulicaciones por Autor";
                    break;
                case TipoReportePublicacion.TipoTexto:
                    nombreReporte = "Pulicaciones por Tipo Texto";
                    break;
                case TipoReportePublicacion.Traductor:
                    nombreReporte = "Pulicaciones por Traductor" ;
                    break;
                case TipoReportePublicacion.Edicion:
                    nombreReporte = "Pulicaciones por Edición";
                    break;
                case TipoReportePublicacion.Editorial:
                    nombreReporte = "Pulicaciones por Editorial";
                    break;
                default:
                    nombreReporte = "SIN NOMBRE";
                    break;
            }
            return nombreReporte;
        }

        public static string GetCampoOrdenacionReporte(TipoReportePublicacion tipoReporte)
        {
            string campoOrdenacion = "autor";
            switch (tipoReporte)
            {
                case TipoReportePublicacion.Anio:
                    campoOrdenacion = "año";
                    break;
                case TipoReportePublicacion.Autor:
                    campoOrdenacion = "autor";
                    break;
                case TipoReportePublicacion.TipoTexto:
                    campoOrdenacion = "titulo";
                    break;
                case TipoReportePublicacion.Traductor:
                    campoOrdenacion = "año";
                    break;
                case TipoReportePublicacion.Edicion:
                    campoOrdenacion = "edicion";
                    break;
                case TipoReportePublicacion.Editorial:
                    campoOrdenacion = "editorial";
                    break;
                default:
                    campoOrdenacion = "autor";
                    break;
            }
            return campoOrdenacion;
        }

    }

}
