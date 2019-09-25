using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace WebSiteLibreria
{
    // <!-- No es necesario quitar la Optimizacion y Bundle para llamadas a PageMethods del ScriptManager -->

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Se registran los scripts de Microsoft para las funciones js en los WebForms.
            // Aunque se registran, deben ser renderizados posteriormente usando el ScriptManager, por lo que se deben de volver a declarar en la página (o MasterPage)
            // Esto solamente define la forma en que los scripts serán  puestos en un solo recurso llamado "WebFormsJs", es como realizar una combinacion de todos los scripts
            // en un solo archivo.
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // "Bundling"/ "Minification" para los scripts de Microsoft ajax (los cuales sí utilizaré) en un solo recurso "MsAjaxJs"

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            // Se agrega un Bundle para los archivos del sitio, se renderizarán como 1 solo archivo en el MasterPage
            bundles.Add(new ScriptBundle("~/bundles/Pages").Include(
                "~/Scripts/Pages/jquery-ui-1.11.4.js",
                    "~/Scripts/Pages/Sitio.js",
                    "~/Scripts/Pages/validacion.js",
                    "~/Scripts/Pages/bootstrap-select.js",
                    "~/Scripts/Pages/bootstrap-submenu.js",
                    "~/Scripts/Pages/jquery.smartmenus.js",
                    "~/Scripts/Pages/jquery.smartmenus.bootstrap.js"));

            // Archivo default.js
            bundles.Add(new ScriptBundle("~/bundles/Pages-Default").Include(
                "~/Scripts/Pages/default.1.2.js"));

            // Se agrega un Bundle para los archivos del sitio, se renderizarán como 1 solo archivo en el MasterPage
            bundles.Add(new ScriptBundle("~/bundles/fileInput").Include(
                "~/Scripts/plugins/fileInput/piexif.js",
                    "~/Scripts/plugin/fileInput/sortable.js",
                    "~/Scripts/plugins/fileInput/purify.js",
                    "~/Scripts/plugins/fileInput/fileinput.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/fileInputLanguage").Include(
                     "~/Scripts/plugins/fileInput/locales/LAN.js",
                    "~/Scripts/plugins/fileInput/locales/es.js"));

            bundles.Add(new ScriptBundle("~/bundles/Pages-CarouselMultiple").Include(
                     "~/Scripts/Pages/carouselMultiple.js"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}