using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AzureDeployTest.Core;
using Microsoft.Owin;
using Startup = AzureDeployTest.Startup;


namespace AzureDeployTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            RolesWithIpConfig.Default.UpdateRolesWithIp();
        }
    }
}