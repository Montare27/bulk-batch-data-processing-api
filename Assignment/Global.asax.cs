namespace Assignment
{
    using System.Web.Http;
    using System;
    using System.Web;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var currentPath = HttpContext.Current.Request.Path.ToLower();
            if (currentPath == "/")
            {
                HttpContext.Current.Response.Redirect("http://localhost:64626/swagger/");
            }
        }
        
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
        }
    }
}
