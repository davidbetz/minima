#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class InjectionProcessor : Themelia.Web.Routing.InjectionProcessorBase
    {
        //- @OnAddHttpHandlers -//
        public override void OnAddHttpHandlers(HttpHandlerDataList injectedHandlerList, params Object[] parameterArray)
        {
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "UrlProcessing",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "FileProcessor",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/Materials/"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "BlogDiscovery",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/rsd.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "WindowsLiveWriterManifest",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/wlwmanifest.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "SiteMap",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/blogmap.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "MetaWeblogApi",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "MetaWeblogApi",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc/"
            });
            SafelyAddHandler(injectedHandlerList, new HttpHandlerData
            {
                Name = "Image",
                MatchType = "contains",
                Priority = 2,
                MatchText = "/imagestore/"
            });
        }
    }
}