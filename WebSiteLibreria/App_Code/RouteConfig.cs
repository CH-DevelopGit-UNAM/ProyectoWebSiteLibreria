using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WebSiteLibreria
{
    // <!-- No es necesario quitar para llamadas a PageMethods del ScriptManager -->
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {            
            var settings = new FriendlyUrlSettings();
            // En este caso se cambia el tipo de "FriendlyUrl" a usar (VS lo asigna como "Permanent")
            // De estar en "Permanent", al llamar al método de página, la respuesta ajax será de "Error de Autenticacion" :
            //      Sys.Net.WebServiceError {_timedOut: false, _message: "Error de autenticación.", _stackTrace: null, _exceptionType: "System.InvalidOperationException", _errorObject: {…}, …}
            settings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(settings);
        }
    }
}