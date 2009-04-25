#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Web;
//+
namespace Minima.Web.Routing
{
    public class UrlProcessingHttpHandler : Themelia.Web.Routing.ReusableSessionHttpHandler
    {
        //- @ProcessRequest -//
        public override void ProcessRequest(HttpContext context)
        {
            String blogPage = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogPage);
            context.Response.ContentType = "text/html";
            System.Web.UI.PageParser.GetCompiledPageInstance(blogPage, null, context).ProcessRequest(context);
        }
    }
}