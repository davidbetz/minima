using System;
using System.Collections.Generic;
using System.Web;
using Minima.Web.Agent;
using Minima.Service;
using Minima.Configuration;
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