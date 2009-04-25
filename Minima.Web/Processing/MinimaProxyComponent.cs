#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia.Web.Processing.Data;
//+
namespace Minima.Web.Processing
{
    public class MinimaProxyComponent : Themelia.Web.Processing.ComponentBase
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
            //+
            AddHandler(HandlerData.Create("__$Minima$UrlProcessing", "contains", "/"));
            AddHandler(HandlerData.Create("__$Minima$SiteMap", "endswith", "/blogmap.xml"));
            AddHandler(HandlerData.Create("__$Minima$Image", "contains", "/imagestore/"));
        }
    }
}