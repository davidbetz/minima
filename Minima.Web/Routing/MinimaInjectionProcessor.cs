using System;
//+
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class MinimaInjectionProcessor : Themelia.Web.Routing.InjectionProcessorBase
    {
        //- @OnAddHttpHandlers -//
        public override void OnAddHttpHandlers(HttpHandlerDataList injectedHandlerList, params Object[] parameterArray)
        {
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Routing.UrlProcessingHttpHandler, Minima.Web",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Routing.FileProcessorHttpHandler, Minima.Web",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/Materials/"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Routing.BlogDiscoveryHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/rsd.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Routing.WindowsLiveWriterManifestHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/wlwmanifest.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Routing.SiteMapHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/blogmap.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Api.MetaWeblog.MetaWeblogApi, Minima.Web",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc/"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Api.MetaWeblog.MetaWeblogApi, Minima.Web",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Minima.Web.Routing.ImageHttpHandler, Minima.Web",
                MatchType = "contains",
                Priority = 4,
                MatchText = "/image/blog/"
            });
        }
    }
}