using System;
using System.Web;
//+
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
            String siteMap = BlogAgent.CreateGoogleSiteMap(ContextItemSet.BlogGuid);
            //+
            context.Response.ContentType = "text/xml";
            context.Response.Write(siteMap);
        }
    }
}