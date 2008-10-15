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
        public override void OnAddHttpHandlers(HandlerDataList injectedHandlerList, params Object[] parameterArray)
        {
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "UrlProcessing",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "FileProcessor",
                MatchType = "contains",
                Priority = 5,
                MatchText = "/Materials/"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "BlogDiscovery",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/rsd.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "WindowsLiveWriterManifest",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/wlwmanifest.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "SiteMap",
                MatchType = "endswith",
                Priority = 2,
                MatchText = "/blogmap.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "MetaWeblogApi",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "MetaWeblogApi",
                MatchType = "contains",
                Priority = 3,
                MatchText = "/xml-rpc/"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "Image",
                MatchType = "contains",
                Priority = 2,
                MatchText = "/imagestore/"
            });
        }
    }
}