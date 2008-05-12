using System;
//+
namespace Minima.Web.Routing
{
    public class BlogFallThroughProcessor : General.Web.Routing.FallThroughProcessorBase
    {
        //- @MatchHttpHandler -//
        public override System.Web.IHttpHandler MatchHttpHandler(String path)
        {
            if (!String.IsNullOrEmpty(ContextItemSet.WebSection))
            {
                return new UrlProcessingHttpHandler();
            }
            return null;
        }
    }
}