using System;
//+
namespace Minima.Web.Routing
{
    public class BlogFallThroughProcessor : Themelia.Web.Routing.FallThroughProcessorBase
    {
        //- @MatchHttpHandler -//
        public override System.Web.IHttpHandler GetHandler(System.Web.HttpContext context, String requestType, String virtualPath, String path)
        {
            if (!String.IsNullOrEmpty(Themelia.Web.WebDomain.Current))
            {
                return new UrlProcessingHttpHandler();
            }
            return null;
        }
    }
}