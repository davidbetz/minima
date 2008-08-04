using System;
using System.Web;
//+
namespace Minima.Web.Routing
{
    public class UrlProcessingHttpHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            String blogPage = Themelia.Web.HttpData.GetScopedItem<String>("Minima", "BlogPage");
            System.Web.UI.PageParser.GetCompiledPageInstance(blogPage, null, context).ProcessRequest(context);
        }
    }
}