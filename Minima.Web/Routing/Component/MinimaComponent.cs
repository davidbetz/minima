using System;
using System.Collections.Generic;
using System.Linq;
//+
using Themelia.Web.Configuration;
using Themelia.Web.Routing.Component;
//+
namespace Minima.Web.Routing.Component
{
    public class MinimaComponent : ComponentBase
    {
        //- @Register -//
        public override void Register(List<ProcessorElement> preProcessorList, List<MappedHandlerElement> mappedHandlerElementList, List<HandlerInjectorElement> handlerInjectorElementList, List<ProcessorElement> fallThroughProcessorList)
        {
            this.ComponentSettingType = typeof(MinimaComponentSetting);
            //+
            preProcessorList.Add(new ProcessorElement
            {
                ProcessorType = "Minima.Web.Routing.HttpHandlerPreProcessor, Minima.Web"
            });
            mappedHandlerElementList.Add(new MappedHandlerElement
            {
                MappedHandlerType = "Minima.Web.Routing.MappedHttpHandlerFactory, Minima.Web"
            });
            handlerInjectorElementList.Add(new HandlerInjectorElement
            {
                InjectorType = "Minima.Web.Routing.MinimaHandlerInjector, Minima.Web"
            });
        }
    }
}