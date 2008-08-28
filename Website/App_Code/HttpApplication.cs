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

        //- @GetVaryByCustomString -//
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "url")
            {
                return context.Request.Url.AbsoluteUri;
            }
            //+
            return base.GetVaryByCustomString(context, custom);
        }

        //+
        //- $HttpApplication_Error -//
        private void HttpApplication_Error(Object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            //+
            EmailReporter.SendAsHtml(typeof(Themelia.Web.Tracing.HttpContextReportCreator), "Uncaught Exception", ctx);
        }
    }
}