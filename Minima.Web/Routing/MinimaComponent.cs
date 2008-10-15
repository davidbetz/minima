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
            aliasedHandlerDataList.Add(new HandlerFactoryData
            {
                FactoryType = "Minima.Web.Routing.HandlerFactory, Minima.Web"
            });
            processorFactoryDataList.Add(new ProcessorFactoryData
            {
                FactoryType = "Minima.Web.Routing.ProcessorFactory, Minima.Web"
            });
            preProcessorDataList.Add(new PreProcessorData
            {
                ProcessorType = "PreProcessor"
            });
            injectionProcessorDataList.Add(new InjectionProcessorData
            {
                ProcessorType = "InjectionProcessor"
            });
            postProcessorDataList.Add(new PostProcessorData
            {
                ProcessorType = "PostProcessor"
            });
            fallThroughProcessorDataList.Add(new FallThroughProcessorData
            {
                Priority = 1,
                ProcessorType = "BlogFallThroughProcessor"
            });
        }
    }
}