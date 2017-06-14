using HanoiAPI.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace HanoiAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacWebApi.Initialize(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}