#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using System;
//+
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class MinimaViewerComponent : Themelia.Web.Routing.ComponentBase
    {
        //- @Register -//
        public override void Register(PreProcessorDataList preProcessorDataList, ProcessorFactoryDataList processorFactoryDataList, HandlerFactoryDataList handlerDataList, InjectionProcessorDataList injectionProcessorDataList, MidProcessorDataList midProcessorDataList, FallThroughProcessorDataList fallThroughProcessorDataList, PostProcessorDataList postProcessorDataList, PostStateProcessorDataList postStateProcessorDataList, ErrorProcessorDataList errorProcessorDataList)
        {
            processorFactoryDataList.Add(FactoryData.Create("Minima.Web.Routing.ProcessorFactory, Minima.Web"));
            preProcessorDataList.Add(ProcessorData.Create<ProcessorData>("__$Minima$PreProcessor", new Object[] { "MinimaViewer" }));
            fallThroughProcessorDataList.Add(ProcessorData.Create<ProcessorData>("__$Minima$BlogFallThroughProcessor"));
        }
    }
}