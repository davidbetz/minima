﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class MinimaProxyComponent : Themelia.Web.Routing.ComponentBase
    {
        //- @Register -//
        public override void Register(PreProcessorDataList preProcessorDataList, ProcessorFactoryDataList processorFactoryDataList, AliasedHandlerFactoryDataList aliasedHandlerDataList, InjectionProcessorDataList injectionProcessorDataList, MidProcessorDataList midProcessorDataList, FallThroughProcessorDataList fallThroughProcessorDataList, PostProcessorDataList postProcessorDataList, PostStateProcessorDataList postStateProcessorDataList, ErrorProcessorDataList errorProcessorDataList)
        {
            processorFactoryDataList.Add(new ProcessorFactoryData
            {
                FactoryType = "Minima.Web.Routing.ProcessorFactory, Minima.Web"
            });
            preProcessorDataList.Add(new PreProcessorData
            {
                ProcessorType = "PreProcessor"
            });
        }
    }
}