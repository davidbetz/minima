using System;
using System.Web;
//+
namespace Minima.Web.Routing
{
    public class UrlProcessingHttpHandler : Themelia.Web.Routing.ReusableHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //- @ProcessRequest -//
        public override void ProcessRequest(HttpContext context)
        {
            String blogPage = Themelia.Web.HttpData.GetScopedItem<String>("Minima", "BlogPage");
            System.Web.UI.PageParser.GetCompiledPageInstance(blogPage, null, context).ProcessRequest(context);
        }
    }
}