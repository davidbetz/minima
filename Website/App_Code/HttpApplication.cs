using System;
using System.Web;
//+
using Themelia.Reporting;
//+
namespace Minima.Web
{
    public class HttpApplication : Themelia.Web.Application
    {
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
    }
}