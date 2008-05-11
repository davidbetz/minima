using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//+
using Minima.Web.Configuration;
//+
namespace Minima.Web.Routing
{
    public class RoutingHttpHandlerFactory : PageHandlerFactory
    {
        static Dictionary<String, IHttpHandler> handlerCache = new Dictionary<String, IHttpHandler>();

        //+
        //- @GetHandler -//
        public override IHttpHandler GetHandler(HttpContext context, String requestType, String url, String pathTranslated)
        {
            String absoluteUrl = context.Request.Url.ToString().ToLower();
            String baseUrl = context.Request.Url.AbsoluteUri.Substring(0, context.Request.Url.AbsoluteUri.Length - context.Request.Url.AbsolutePath.Length);
            //+
            if (handlerCache.ContainsKey(absoluteUrl))
            {
                return handlerCache[absoluteUrl];
            }
            //+
            List<HttpHandlerElement> handlerList = WebConfigurationFacade.GetWebConfiguration().HttpHandlers.OrderBy(p => p.Priority).ToList();
            IHttpHandler hh = null;
            foreach (HttpHandlerElement h in handlerList)
            {
                hh = HttpHandlerFactoryRouter.MatchHttpHandler(absoluteUrl, h.Name.ToLower(), h.MatchType, h.MatchText.ToLower());
                if (hh != null)
                {
                    break;
                }
            }
            if (hh is MinimaBaseHttpHandler)
            {
                return base.GetHandler(context, requestType, url, pathTranslated);
            }
            else if (hh != null)
            {
                if (!handlerCache.ContainsKey(absoluteUrl))
                {
                    handlerCache.Add(absoluteUrl, hh);
                }
                return hh;
            }
            //+
            return base.GetHandler(context, requestType, url, pathTranslated);
        }
    }
}