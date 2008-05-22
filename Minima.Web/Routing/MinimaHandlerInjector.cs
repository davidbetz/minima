using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Web.Routing.Component;
//+
namespace Minima.Web.Routing
{
    public class MinimaHandlerInjector : General.Web.Routing.HandlerInjectorBase
    {
        //- @OnAddHttpHandlers -//
        public override void OnAddHttpHandlers(List<General.Web.Configuration.HttpHandlerElement> injectedHandlerList)
        {
            MinimaComponentSetting minimaComponentSetting = (MinimaComponentSetting)MinimaComponentSetting.CurrentComponentSetting;
            List<String> webSectionList = minimaComponentSetting.WebSections.Where(p => p.Key != "root").Select(p => p.Key).ToList();
            //+ to support root, BlogFallThroughProcessor is required as it handles this
            foreach (String webSection in webSectionList)
            {
                SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
                {
                    Name = "Minima.Web.Routing.UrlProcessingHttpHandler, Minima.Web",
                    MatchType = "contains",
                    Priority = 5,
                    MatchText = "/" + General.Web.HttpWebSection.CurrentWebSection + "/"
                });
            }
            SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.BlogEntryImageHttpHandler, Minima.Web",
                MatchType = "contains",
                Priority = 4,
                MatchText = "/Image/"
            });
            SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.FileProcessorHttpHandler, Minima.Web",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/Materials/"
            });
            SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.BlogDiscoveryHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/rsd.xml"
            });
            SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.WindowsLiveWriterManifestHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/wlwmanifest.xml"
            });
            SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.SiteMapHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/blogmap.xml"
            });
            SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Api.MetaWeblog.MetaWeblogApi, Minima.Web",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc/"
            });
            SafelyAddHandler(injectedHandlerList, new General.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Api.MetaWeblog.MetaWeblogApi, Minima.Web",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc"
            });
        }
    }
}