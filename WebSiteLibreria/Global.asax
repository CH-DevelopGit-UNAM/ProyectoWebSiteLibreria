<%@ Application Language="C#" %>
<%@ Import Namespace="WebSiteLibreria" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    void Session_End(object sender, EventArgs e)
    {
        //FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage();
    }

    void Application_BeginRequest(object sender, EventArgs e) {
        HttpApplication app = sender as HttpApplication;        
        //System.Diagnostics.Debug.WriteLine("-->>>>>>>>> Global.asax -- BeginEquest");
    }

</script>
