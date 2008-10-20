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
                MatchText = "/"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "FileProcessor",
                MatchType = "contains",
                MatchText = "/Materials/"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "BlogDiscovery",
                MatchType = "endswith",
                MatchText = "/rsd.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "WindowsLiveWriterManifest",
                MatchType = "endswith",
                MatchText = "/wlwmanifest.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "SiteMap",
                MatchType = "endswith",
                MatchText = "/blogmap.xml"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "MetaWeblogApi",
                MatchType = "contains",
                MatchText = "/xml-rpc"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "MetaWeblogApi",
                MatchType = "contains",
                MatchText = "/xml-rpc/"
            });
            SafelyAddHandler(injectedHandlerList, new HandlerData
            {
                Name = "Image",
                MatchType = "contains",
                MatchText = "/imagestore/"
            });
        }
    }
}