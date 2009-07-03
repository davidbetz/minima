#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using Themelia.Web.Processing.Data;
using System;
using Themelia.Web;
//+
namespace Minima.Web.Processing
{
    public class MinimaComponent : Themelia.Web.Processing.Component
    {
        //- @Register -//
        public override void Register()
        {
            String blogGuid = ParameterMap.PeekSafely("blogGuid");
            if (String.IsNullOrEmpty(blogGuid))
            {
                throw new System.Configuration.ConfigurationErrorsException("blogPage parameter is required for the Blog component.");
            }
            //+ factory
            AddFactory(FactoryData.Create("Minima.Web.Processing.HandlerFactory, Minima.Web"));
            AddFactory(FactoryData.Create("Minima.Web.Processing.ProcessorFactory, Minima.Web"));
            //+ processor
            AddProcessor(ProcessorData.Create<ProcessorData>("__$Minima$InitProcessor", new Object[] { blogGuid }));
            //+ handler
            AddEndpoint(EndpointData.Create(SelectorType.EndsWith, "/rsd.xml", "__$Minima$BlogDiscovery"));
            AddEndpoint(EndpointData.Create(SelectorType.EndsWith, "/wlwmanifest.xml", "__$Minima$WindowsLiveWriterManifest"));
            AddEndpoint(EndpointData.Create(SelectorType.EndsWith, "/blogmap.xml", "__$Minima$SiteMap"));
            AddEndpoint(EndpointData.Create(SelectorType.Contains, "/xml-rpc", "__$Minima$MetaWeblogApi"));
            AddEndpoint(EndpointData.Create(SelectorType.Contains, "/xml-rpc/", "__$Minima$MetaWeblogApi"));
            AddEndpoint(EndpointData.Create(SelectorType.Contains, "/imagestore", "__$Minima$Image"));
        }
    }
}