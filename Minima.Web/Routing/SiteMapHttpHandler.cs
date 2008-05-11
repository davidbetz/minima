using System;
using System.Web;
//+
using Minima.Configuration;
using Minima.Web.Agent;
//+
namespace Minima.Web.Routing
{
    public class SiteMapHttpHandler : IHttpHandler
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            String siteMap = BlogAgent.CreateGoogleSiteMap(MinimaConfiguration.BlogGuid);
            context.Response.ContentType = "text/xml";
            context.Response.Write(siteMap);
        }
    }
}