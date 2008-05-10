using System;
using System.Web;
//+
using Minima.Web.Api.MetaWeblog;
//+
namespace Minima.Web.Routing
{
    internal static class HttpHandlerFactoryRouter
    {
        //- #MatchHttpHandler -//
        internal static IHttpHandler MatchHttpHandler(String url, String name, Char type, String text) {
            text = text.Replace("~", WebConfiguration.Domain);
            type = type.ToString().ToLower()[0];
            IHttpHandler h = null;
            switch (type) {
                case 'c':
                    h = MatchContains(url, name, text);
                    break;
                case 's':
                    h = MatchStartsWith(url, name, text);
                    break;
                case 'e':
                    h = MatchEndsWith(url, name, text);
                    break;
                case 'd':
                    h = MatchDefault(name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid HttpHandlerType");
            }
            //+
            return h;
        }

        //- #MatchContains -//
        internal static IHttpHandler MatchContains(String url, String name, String text)
        {
            if (url.Contains(text)) {
                return GetHttpHandler(name);
            }
            //+
            return null;
        }

        //- #MatchStartsWith -//
        internal static IHttpHandler MatchStartsWith(String url, String name, String text)
        {
            if (url.StartsWith(text)) {
                return GetHttpHandler(name);
            }
            //+
            return null;
        }

        //- #MatchEndsWith -//
        internal static IHttpHandler MatchEndsWith(String url, String name, String text)
        {
            if (url.EndsWith(text)) {
                return GetHttpHandler(name);
            }
            //+
            return null;
        }

        //- #MatchDefault -//
        internal static IHttpHandler MatchDefault(String name)
        {
            return GetHttpHandler(name);
        }

        //- #GetHttpHandler -//
        internal static IHttpHandler GetHttpHandler(String text)
        {
            switch (text) {
                case "base":
                    return new MinimaBaseHttpHandler( );
                case "defaulthttphandler":
                    return new DefaultHttpHandler( );
                case "blogentryimagehttphandler":
                    return new BlogEntryImageHttpHandler();
                case "metaweblogapi":
                    return new MetaWeblogApi();
                case "fileprocessorhttphandler":
                    return new FileProcessorHttpHandler();
                case "sitemaphttphandler":
                    return new SiteMapHttpHandler();
                case "urlrewritehttphandler":
                    return new UrlRewriteHttpHandler();
                case "urlprocessinghttphandler":
                    return new UrlProcessingHttpHandler();
                default:
                    throw new ArgumentOutOfRangeException("Unknown HttpHandler in HttpHandlerMatchText. Did you register the HttpHandler in the MinimaHttpHandlerFactoryRouter?");
            }
        }
    }
}