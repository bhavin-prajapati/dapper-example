using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DapperExample.Resources;
using DapperExample.Services;
using DapperExample.Models;
using DapperExample.Utility;

namespace DapperExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ActorSystem actorSystem;
        private static TimeSpan timeout = new TimeSpan(0, 1, 30);

        public static ActorSystem ActorSystem
        {
            get { return actorSystem; }
        }

        public static TimeSpan Timeout
        {
            get { return timeout; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            actorSystem = ActorSystem.Create(AppConstants.ActorSystem);
            ServiceBroker.RegisterServices();
        }
    }
}