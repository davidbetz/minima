using System;
using System.Web;
//+
using Themelia.ExceptionHandling;
//+
namespace Minima.Web
{
    public class HttpApplication : System.Web.HttpApplication
    {
        //- @Ctor -//
        public HttpApplication()
        {
            this.Error += new EventHandler(HttpApplication_Error);
        }

        //+
        //- $HttpApplication_Error -//
        private void HttpApplication_Error(Object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            //+
            ExceptionManager.Report("Uncaught Exception", ctx);
        }
    }
}