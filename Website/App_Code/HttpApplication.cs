using System;
using System.Web;
//+
using Themelia.Tracing;
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
            EmailReporter.Send("Uncaught Exception", ctx);
        }
    }
}