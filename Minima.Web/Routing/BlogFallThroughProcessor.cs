using System;
//+
namespace Minima.Web.Routing
{
    public class BlogFallThroughProcessor : General.Web.Routing.FallThroughProcessorBase
    {
        //- @MatchHttpHandler -//
        public override System.Web.IHttpHandler GetHandler(System.Web.HttpContext context, string requestType, string virtualPath, string path)
        {
            if (!String.IsNullOrEmpty(ContextItemSet.WebSection))
            {
                return new UrlProcessingHttpHandler();
            }
            return null;
        }
    }
}