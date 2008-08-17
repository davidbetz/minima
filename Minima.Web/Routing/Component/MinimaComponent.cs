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
        public override void Register(PreProcessorDataList preProcessorDataList, MappedHandlerFactoryDataList mappedHandlerDataList, HandlerInjectorDataList handlerInjectorDataList, MidProcessorDataList midProcessorList, FallThroughProcessorDataList fallThroughProcessorList, PostProcessorDataList postProcessorList, PostStateProcessorDataList postStateProcessorDataList)
        {
            //this.ComponentSettingType = typeof(MinimaComponentSetting);
            //+
            preProcessorDataList.Add(new PreProcessorData
            {
                ProcessorType = "Minima.Web.Routing.MinimaPreProcessor, Minima.Web"
            });
            mappedHandlerDataList.Add(new MappedHandlerFactoryData
            {
                FactoryType = "Minima.Web.Routing.MappedHttpHandlerFactory, Minima.Web"
            });
            handlerInjectorDataList.Add(new HandlerInjectorData
            {
                InjectorType = "Minima.Web.Routing.MinimaHandlerInjector, Minima.Web"
            });
            postProcessorList.Add(new PostProcessorData
            {
                ProcessorType = "Minima.Web.Routing.MinimaPostProcessor, Minima.Web"
            });
        }
    }
}