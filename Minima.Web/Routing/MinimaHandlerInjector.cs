using System.Collections.Generic;
using System.Linq;
//+
using Minima.Web.Configuration;
//+
namespace Minima.Web.Routing
{
    public class MinimaHandlerInjector : General.Web.Routing.HandlerInjectorBase
    {
        //- @OnAddHttpHandlers -//
        public override void OnAddHttpHandlers(List<General.Web.Configuration.HttpHandlerElement> injectedHandlerList)
        {
            List<InstanceElement> instanceElementList = MinimaConfigurationFacade.GetWebConfiguration().Registration.Where(p => p.WebSection != "root").ToList();
            //+ to support root, BlogFallThroughProcessor is required as it handles this
            foreach (InstanceElement instanceElement in instanceElementList)
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