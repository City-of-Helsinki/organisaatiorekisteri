using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Affecto.Logging;
using Affecto.Logging.Log4Net;

namespace OrganizationRegister.AngularApplication
{
    public class MvcApplication : HttpApplication
    {
        private const string ErrorAction = "/Home/Error";

        public static Version AppVersion { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AppVersion = Assembly.GetExecutingAssembly().GetName().Version;
        }

        protected void Application_Error(Object sender, EventArgs arguments)
        {
            if (!WasErrorActionExecuted())
            {
                LogError();
                ClearError();
                Response.Redirect(ErrorAction);                
            }
        }

        private bool WasErrorActionExecuted()
        {
            try
            {
                return Request.CurrentExecutionFilePath.Equals(ErrorAction);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ClearError()
        {
            Server.ClearError();
            Response.Clear();
        }

        private void LogError()
        {
            ILogger logger = new Log4NetLoggerFactory().CreateLogger(this);
            logger.LogCritical(Server.GetLastError(), "Error occured in ASP.NET MVC application.");
        }
    }
}
