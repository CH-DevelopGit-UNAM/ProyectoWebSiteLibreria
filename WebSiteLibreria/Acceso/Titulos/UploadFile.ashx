<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using System.IO;
using Unam.CoHu.Libreria.Model.WebServices;
using Unam.CoHu.Libreria.Controller.Security;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Controller;
using System.Web.Script.Serialization;
using System.Net;

public class UploadFile : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        WebResult result = new WebResult();
        try
        {
            if (!context.Request.IsAuthenticated)
            {
                result.MensajeResultado = "Debe iniciar sesión para usar esta funcion";
                result.HasException = false;
                result.IsProcesado = false;
            }
            else
            {            
                string pathUpload = ConfiguracionSitio.ObtenerVariableConfig("FolderSavePostedFiles");
                string urlImage = ConfiguracionSitio.ObtenerVariableConfig("PathPostedFiles");
                string mismoServidor = ConfiguracionSitio.ObtenerVariableConfig("IsSameServerPostedFiles");
                bool isMismoServidor = true;
                bool.TryParse(mismoServidor, out isMismoServidor);
                string urlPostedFile = string.Empty;
                int idTitulo = 0;


                if (!string.IsNullOrEmpty(pathUpload))
                {
                    if (isMismoServidor)
                    {
                        pathUpload = context.Server.MapPath(pathUpload);
                    }

                    HttpPostedFile file= context.Request.Files[0];
                    Stream ms = file.InputStream;
                    byte[] datos = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(datos, 0, datos.Length);
                    ms.Flush();

                    if (context.Request.Form["IdTitulo"] != null) {
                        string value = context.Request.Form["IdTitulo"].ToString();
                        Int32.TryParse(value, out idTitulo );
                    }
                    LibreriaController libreria = new LibreriaController();
                    int rowsAffected = libreria.ActualizarImagen(idTitulo, pathUpload, urlImage, file.FileName, true ,datos, ref urlPostedFile);
                    if (rowsAffected > 0)
                    {
                        result.Resultado = urlPostedFile;
                        result.MensajeResultado = "Archivo subido";
                        result.HasException = false;
                        result.IsProcesado = true;
                    }
                    else {
                        result.Resultado = urlPostedFile;
                        result.MensajeResultado = "No se actualizó la ruta del archivo";
                        result.HasException = false;
                        result.IsProcesado = false;
                    }
                }
                else {
                    result.MensajeResultado = "No se ha configurado la ruta de subida";
                    result.HasException = false;
                    result.IsProcesado = false;
                }
            }
        }
        catch (Exception ex)
        {
            result.MensajeResultado = "Error al subir el archivo";
            result.HasException = true;
            result.IsProcesado = false;
        }


        if (!result.IsProcesado)
        {
            context.Response.Write(result.MensajeResultado);
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }
        else {
            context.Response.ContentType = "json";
            JavaScriptSerializer serializador = new JavaScriptSerializer();
            string resultado = serializador.Serialize(result);
            context.Response.Write(resultado );
            context.Response.StatusCode = (int) System.Net.HttpStatusCode.OK;
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}