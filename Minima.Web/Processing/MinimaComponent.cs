#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using Themelia.Web.Processing.Data;
using System;
//+
namespace Minima.Web.Processing
{
    public class MinimaComponent : Themelia.Web.Processing.ComponentBase
    {
        //- @Register -//
        public override void Register()
        {
            String blogGuid = ParameterMap.PeekSafely("blogGuid");
            if (String.IsNullOrEmpty(blogGuid))
            {
                throw new System.Configuration.ConfigurationErrorsException(String.Format("Missing parameter: {0}.", "blogGuid"));
            }
            //+
            AddFactory(FactoryData.Create("Minima.Web.Processing.HandlerFactory, Minima.Web"));
            AddFactory(FactoryData.Create("Minima.Web.Processing.ProcessorFactory, Minima.Web"));
            //+
            AddProcessor(ProcessorData.Create<ProcessorData>("__$Minima$PreProcessor", new Object[] { blogGuid }));
            AddProcessor(ProcessorData.Create<ProcessorData>("__$Minima$PostProcessor"));
            //+
            AddHandler(HandlerData.Create("__$Minima$UrlProcessing", "contains", "/"));
            AddHandler(HandlerData.Create("__$Minima$BlogDiscovery", "endswith", "/rsd.xml"));
            AddHandler(HandlerData.Create("__$Minima$WindowsLiveWriterManifest", "endswith", "/wlwmanifest.xml"));
            AddHandler(HandlerData.Create("__$Minima$SiteMap", "endswith", "/blogmap.xml"));
            AddHandler(HandlerData.Create("__$Minima$MetaWeblogApi", "contains", "/xml-rpc"));
            AddHandler(HandlerData.Create("__$Minima$MetaWeblogApi", "contains", "/xml-rpc/"));
            AddHandler(HandlerData.Create("__$Minima$Image", "contains", "/imagestore/"));
        }
    }
}