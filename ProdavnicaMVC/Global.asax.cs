using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProdavnicaMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //svaki put kada otvara stranicu ulazi u globalasax
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Validation";
            DefaultModelBinder.ResourceClassKey = "Validation";
        }

       
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var fallbackLanguage = "";   //defaultni, u ovom slucaju srpski (Resource.resx)
            var cookie = Request.Cookies["Jezik"];  //iz liste cookie-a trazi ovaj - Jezik
            if (cookie != null)
            {
                fallbackLanguage = cookie.Value;
                
            }
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(fallbackLanguage);

        }

    }
}
