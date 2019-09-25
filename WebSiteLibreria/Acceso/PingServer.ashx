<%@ WebHandler Language="C#" Class="PingServer" %>

using System;
using System.Web;
using System.Web.SessionState;

public class PingServer : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}