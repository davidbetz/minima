using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Web.Routing.Component;
//+
namespace Minima.Web.Routing
{
    public class MinimaHandlerInjector : Themelia.Web.Routing.HandlerInjectorBase
    {
        //- @OnAddHttpHandlers -//
        public override void OnAddHttpHandlers(List<Themelia.Web.Configuration.HttpHandlerElement> injectedHandlerList, params Object[] parameterArray)
        {
            MinimaComponentSetting minimaComponentSetting = (MinimaComponentSetting)MinimaComponentSetting.CurrentComponentSetting;
            List<String> webSectionList = minimaComponentSetting.WebSections.Where(p => p.Key != null && p.Key.ToLower() != "root").Select(p => p.Key).ToList();
            //+ to support root, BlogFallThroughProcessor is required as it handles this
            foreach (String webSection in webSectionList)
            {
                SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
                {
                    Name = "Minima.Web.Routing.UrlProcessingHttpHandler, Minima.Web",
                    MatchType = "contains",
                    Priority = 5,
                    MatchText = "/" + Themelia.Web.WebSection.Current + "/"
                });
            }
            SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.BlogEntryImageHttpHandler, Minima.Web",
                MatchType = "contains",
                Priority = 4,
                MatchText = "/Image/"
            });
            SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.FileProcessorHttpHandler, Minima.Web",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/Materials/"
            });
            SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.BlogDiscoveryHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/rsd.xml"
            });
            SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.WindowsLiveWriterManifestHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/wlwmanifest.xml"
            });
            SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Routing.SiteMapHttpHandler, Minima.Web",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/blogmap.xml"
            });
            SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Api.MetaWeblog.MetaWeblogApi, Minima.Web",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc/"
            });
            SafelyAddHandler(injectedHandlerList, new Themelia.Web.Configuration.HttpHandlerElement
            {
                Name = "Minima.Web.Api.MetaWeblog.MetaWeblogApi, Minima.Web",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc"
            });
        }
    }
}