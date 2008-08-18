using System;
//+
namespace Minima.Web.Routing
{
    public class BlogFallThroughProcessor : Themelia.Web.Routing.FallThroughProcessorBase
    {
        //- @MatchHttpHandler -//
        public override System.Web.IHttpHandler GetHandler(System.Web.HttpContext context, string requestType, string virtualPath, string path)
        {
            if (!String.IsNullOrEmpty(Themelia.Web.WebDomain.Current))
            {
                return new UrlProcessingHttpHandler();
            }
            return null;
        }
    }
}