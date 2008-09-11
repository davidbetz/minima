using System.Collections.Generic;
//+
using Themelia.Web.Configuration;
using Themelia.Web.Routing.Component;
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing.Component
{
    public class MinimaComponent : ComponentBase
    {
        //- @Register -//
        public override void Register(PreProcessorDataList preProcessorDataList, ProcessorFactoryDataList processorFactoryDataList, AliasedHandlerFactoryDataList aliasedHandlerDataList, InjectionProcessorDataList injectionProcessorDataList, MidProcessorDataList midProcessorDataList, FallThroughProcessorDataList fallThroughProcessorDataList, PostProcessorDataList postProcessorDataList, PostStateProcessorDataList postStateProcessorDataList, ErrorProcessorDataList errorProcessorDataList)
        {
            preProcessorDataList.Add(new PreProcessorData
            {
                ProcessorType = "Minima.Web.Routing.MinimaPreProcessor, Minima.Web"
            });
            aliasedHandlerDataList.Add(new AliasedHandlerFactoryData
            {
                FactoryType = "Minima.Web.Routing.MappedHttpHandlerFactory, Minima.Web"
            });
            injectionProcessorDataList.Add(new InjectionProcessorData
            {
                ProcessorType = "Minima.Web.Routing.MinimaInjectionProcessor, Minima.Web"
            });
            postProcessorDataList.Add(new PostProcessorData
            {
                ProcessorType = "Minima.Web.Routing.MinimaPostProcessor, Minima.Web"
            });
            fallThroughProcessorDataList.Add(new FallThroughProcessorData
            {
                Priority = 1,
                ProcessorType = "Minima.Web.Routing.BlogFallThroughProcessor, Minima.Web"
            });
        }
    }
}