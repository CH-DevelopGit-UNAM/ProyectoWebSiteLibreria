<%@ WebHandler Language="C#" Class="GenerarReportePublicaciones" %>

using System;
using System.IO;
using System.Web;
using System.Collections.Generic;

using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;
using Unam.CoHu.Libreria.Controller;
using Unam.CoHu.Libreria.Controller.Catalogos;
using Unam.CoHu.Libreria.Controller.Enums;
using Unam.CoHu.Libreria.Controller.Web;

public class GenerarReportePublicaciones : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        try
        {
            if (!context.Request.IsAuthenticated)
            {
                WriteErrorOnResponse(context.Response, "Debe iniciar sesión para descargar el archivo");
                context.ApplicationInstance.CompleteRequest();
            }
            else {
                ReportesController reportes = new ReportesController();
                string busqueda = (context.Request["param1"] != null  ? context.Request["param1"].ToString(): string.Empty) ;
                string rangoInicio = (context.Request["param2"] != null  ? context.Request["param2"].ToString(): string.Empty) ;
                string rangoFin = (context.Request["param3"] != null  ? context.Request["param3"].ToString(): string.Empty) ;
                string funcion = (context.Request["param4"] != null  ? context.Request["param4"].ToString(): string.Empty) ;
                string orden = (context.Request["ord"] != null ? context.Request["ord"].ToString() : string.Empty);
                string tipoReporte = context.Request.QueryString["tipoRpt"].ToString();
                string tipoarchivo = context.Request.QueryString["tipo"].ToString();
                bool isLoadedAll = false;
                int intDummy = (int) TipoReportePublicacion.Autor;
                Int32.TryParse(tipoReporte, out intDummy);


                List<TituloLibreriaView> listaDatos = reportes.ReportePublicacionesPor((TipoReportePublicacion)intDummy, busqueda, rangoInicio, rangoFin, funcion,null,null, false, ref isLoadedAll);
                //if ((string.IsNullOrEmpty(busqueda) & tipoReporte != "año") && (string.IsNullOrEmpty(busqueda) & string.IsNullOrEmpty(rangoFin) & tipoReporte == "año"))
                //{
                //    listaDatos = reportes.ReportePublicacionesPor(null, null, null, null, null, null, null, null, orden, false);
                //}
                //else
                //{
                //    switch (tipoReporte)
                //    {
                //        case "año":
                //            int intDummy= -1;
                //            int? anioInicio = null;
                //            int? anioFin = null;

                //            Int32.TryParse(busqueda, out intDummy);
                //            if (intDummy == -1)
                //            {
                //                anioInicio = new int?(intDummy);
                //            }
                //            else {
                //                anioInicio = (intDummy == 0 ? null : new int?(intDummy));
                //            }


                //            intDummy = -1;
                //            Int32.TryParse(rangoFin, out intDummy);

                //            if (intDummy == -1)
                //            {
                //                anioFin = new int?(intDummy);
                //            }
                //            else {
                //                anioFin = (intDummy == 0 ? null : new int?(intDummy));
                //            }

                //            listaDatos = reportes.ReportePublicacionesPor(anioInicio, anioFin, null, null, null, null, null, null, orden, false);
                //            break;
                //        case "autor":
                //            listaDatos = reportes.ReportePublicacionesPor(null, null, busqueda, null, null, null, null, null, orden, false);
                //            break;
                //        case "texto":
                //            listaDatos = reportes.ReportePublicacionesPor(null, null, null, busqueda, null, null, null, null, orden, false);
                //            break;
                //        case "traductor":
                //            FuncionesController funcDb = new FuncionesController();
                //            List<Funcion> funciones = funcDb.BuscarPor(busqueda, new int?(1));
                //            Funcion func = null;
                //            if (funciones != null && funciones.Count > 0)
                //                func = funciones[0];

                //            if (func != null)
                //                listaDatos = reportes.ReportePublicacionesPor(null, null, null, null, func.IdFuncion, null, null, null, orden, false);

                //            break;
                //        case "editorial":
                //            listaDatos = reportes.ReportePublicacionesPor(null, null, null, null, null, busqueda, null, null, orden, false);
                //            break;
                //        case "edicion":
                //            listaDatos = reportes.ReportePublicacionesPor(null, null, null, null, null, null, busqueda, null, orden, false);
                //            break;
                //        default:
                //            listaDatos = null;
                //            break;
                //    }
                //}

                string mensajeRetorno = string.Empty;
                bool realizado = false;
                MemoryStream stream = null;
                string nameFile = string.Empty;
                DateTime fechaActual = DateTime.Now;
                string formatoSalida = "text/plain";
                System.Drawing.Image imagen = System.Drawing.Image.FromFile(context.Server.MapPath("~/images/Escudo_UNAM.jpg"));

                if (!string.IsNullOrEmpty(tipoReporte))
                {
                    if (tipoarchivo.Equals("excel", StringComparison.InvariantCultureIgnoreCase))
                    {
                        realizado =  reportes.GenerateExcel((TipoReportePublicacion)intDummy, ref stream, imagen, listaDatos, ref mensajeRetorno);
                        nameFile = string.Format("ReportePublicaciones-{0}{1}{2}{3}{4}{5}.xlsx", fechaActual.Year.ToString("0000"), fechaActual.Month.ToString("00"), fechaActual.Day.ToString("00"), fechaActual.Hour.ToString("00"),fechaActual.Minute.ToString("00"), fechaActual.Second.ToString("00"));
                        formatoSalida = "application/vnd.ms-excel";
                    }
                    else if (tipoReporte.Equals("pdf", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //mensajeRetorno = GeneratePDF(stream, imagen, listaDatos);
                        //nameFile = string.Format("ReporteRelacionCuentaClabe-{0}{1}{2}.pdf", fechaActual.Year.ToString("0000"), fechaActual.Month.ToString("00"), fechaActual.Day.ToString("00"));
                        formatoSalida = "application/pdf";
                        mensajeRetorno = "No se puede generar el tipo de documento solicitado, indique de nuevo el tipo (excel).";
                    }
                    else
                    {
                        mensajeRetorno = "No se puede generar el tipo de documento solicitado, indique de nuevo el tipo (excel).";
                    }
                }
                else
                {
                    mensajeRetorno = "No se ha especificado el tipo de documento a generar. Indique de nuevo el tipo (excel).";
                }

                if (stream != null)
                {
                    context.Response.Clear();
                    context.Response.ContentType = formatoSalida;
                    if (formatoSalida.Contains("pdf"))
                        context.Response.AddHeader("Content-Disposition", "filename=" + nameFile + "");
                    else
                        context.Response.AddHeader("Content-Disposition", "attachment;filename=" + nameFile + "");
                    context.Response.OutputStream.Write(stream.GetBuffer(), 0, stream.GetBuffer().Length);
                    context.Response.OutputStream.Flush();
                    stream.Close();
                    context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    context.Response.Clear();
                    context.Response.ContentType = formatoSalida;
                    context.Response.Write(mensajeRetorno);
                    context.Response.Flush();
                    context.ApplicationInstance.CompleteRequest();
                }
            }

        }
        catch (Exception ex)
        {
            string mensaje = ex.Message;
            mensaje = mensaje + @":\n\r" + ex.StackTrace;
            WriteErrorOnResponse(context.Response, "Ocurrió un error desconocido al descargar el archivo.");
            context.ApplicationInstance.CompleteRequest();
        }
    }

    private void WriteErrorOnResponse(HttpResponse responseToWrite, string messageToWrite)
    {
        responseToWrite.Headers.Remove("Content-Disposition");
        responseToWrite.ContentType = "text/html";
        responseToWrite.Write(messageToWrite);
        //responseToWrite.Write("Ocurrió un error desconocido al descargar el archivo.");
        responseToWrite.Flush();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}