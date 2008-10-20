#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class MinimaComponent : Themelia.Web.Routing.ComponentBase
    {
        //- @Register -//
        public override void Register(PreProcessorDataList preProcessorDataList, ProcessorFactoryDataList processorFactoryDataList, HandlerFactoryDataList aliasedHandlerDataList, InjectionProcessorDataList injectionProcessorDataList, MidProcessorDataList midProcessorDataList, FallThroughProcessorDataList fallThroughProcessorDataList, PostProcessorDataList postProcessorDataList, PostStateProcessorDataList postStateProcessorDataList, ErrorProcessorDataList errorProcessorDataList)
        {
            aliasedHandlerDataList.Add(FactoryData.Create("Minima.Web.Routing.HandlerFactory, Minima.Web"));
            processorFactoryDataList.Add(FactoryData.Create("Minima.Web.Routing.ProcessorFactory, Minima.Web"));
            preProcessorDataList.Add(ProcessorData.Create<ProcessorData>("__$Minima$PreProcessor"));
            injectionProcessorDataList.Add(ProcessorData.Create<ProcessorData>("__$Minima$InjectionProcessor"));
            postProcessorDataList.Add(ProcessorData.Create<ProcessorData>("__$Minima$PostProcessor"));
            fallThroughProcessorDataList.Add(ProcessorData.Create<ProcessorData>("__$Minima$BlogFallThroughProcessor"));
        }
    }
}