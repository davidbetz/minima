﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class MinimaViewerComponent : MinimaProxyComponent
    {
        //- @Register -//
        public override void Register(PreProcessorDataList preProcessorDataList, ProcessorFactoryDataList processorFactoryDataList, HandlerFactoryDataList handlerDataList, InjectionProcessorDataList injectionProcessorDataList, MidProcessorDataList midProcessorDataList, FallThroughProcessorDataList fallThroughProcessorDataList, PostProcessorDataList postProcessorDataList, PostStateProcessorDataList postStateProcessorDataList, ErrorProcessorDataList errorProcessorDataList)
        {
            fallThroughProcessorDataList.Add(ProcessorData.Create<ProcessorData>("BlogFallThroughProcessor"));
            //+
            base.Register(preProcessorDataList, processorFactoryDataList, handlerDataList, injectionProcessorDataList, midProcessorDataList, fallThroughProcessorDataList, postProcessorDataList, postStateProcessorDataList, errorProcessorDataList);
        }
    }
}