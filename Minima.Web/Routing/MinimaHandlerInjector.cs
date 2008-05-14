using System.Collections.Generic;
//+
using General.Web.Configuration;
//+
namespace Minima.Web.Routing
{
    public class MinimaHandlerInjector : General.Web.Routing.HandlerInjectorBase
    {
        public override void OnAddHttpHandlers(List<HttpHandlerElement> injectedHandlerList)
        {
            injectedHandlerList.Add(new HttpHandlerElement
            {
                Name = "BlogDiscovery",
                MatchType = "endswith",
                Priority = 7,
                MatchText = "/rsd.xml"
            });
            injectedHandlerList.Add(new HttpHandlerElement
            {
                Name = "WindowsLiveWriterManifest",
                MatchType = "endswith",
                Priority = 7,
                MatchText = "/wlwmanifest.xml"
            });
            injectedHandlerList.Add(new HttpHandlerElement
            {
                Name = "SiteMap",
                MatchType = "endswith",
                Priority = 7,
                MatchText = "/blogmap.xml"
            });
            injectedHandlerList.Add(new HttpHandlerElement
            {
                Name = "MetaWeblogAPI",
                MatchType = "contains",
                Priority = 4,
                MatchText = "/xml-rpc/"
            });
            injectedHandlerList.Add(new HttpHandlerElement
            {
                Name = "MetaWeblogAPI",
                MatchType = "contains",
                Priority = 4,
                MatchText = "/xml-rpc"
            });
        }
    }
}