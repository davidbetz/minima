using System;
using System.Web;
//+
using Minima.Web.Api.MetaWeblog;
//+
namespace Minima.Web.Routing
{
    public class HttpHandlerDelegationFactory : General.Web.Routing.HttpHandlerDelegationFactoryBase
    {
        //- @GetHttpHandler -//
        public override IHttpHandler GetHttpHandler(String text)
        {
            switch (text)
            {
                case "base":
                    return new General.Web.Routing.BaseHttpHandler();
                case "defaulthttphandler":
                    return new DefaultHttpHandler();
                case "blogentryimagehttphandler":
                    return new BlogEntryImageHttpHandler();
                case "metaweblogapi":
                    return new MetaWeblogApi();
                case "fileprocessorhttphandler":
                    return new FileProcessorHttpHandler();
                case "sitemaphttphandler":
                    return new SiteMapHttpHandler();
                case "urlrewritehttphandler":
                    return new General.Web.Routing.UrlRewriteHttpHandler();
                case "urlprocessinghttphandler":
                    return new UrlProcessingHttpHandler();
                default:
                    throw new ArgumentOutOfRangeException("Unknown HttpHandler in HttpHandlerMatchText. Did you register the HttpHandler in the MinimaHttpHandlerFactoryRouter?");
            }
        }
    }
}