#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using System;
//+
using Themelia.Web.Processing.Data;
//+
namespace Minima.Web.Processing
{
    public class MinimaProxyComponent : Themelia.Web.Processing.Component
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
            AddFactory(FactoryData.Create("Minima.Web.Processing.ProcessorFactory, Minima.Web"));
            //+ processor
            AddProcessor(ProcessorData.Create<ProcessorData>("__$Minima$InitProcessor", new Object[] { blogGuid }));
        }
    }
}