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

        //- @GetSettings -//
        public override ComponentSetting GetSettings(ComponentElement componentElement)
        {
            WebSectionSettingMap webSectionMap = new WebSectionSettingMap();
            List<ParameterCollection> parameterCollectionList = componentElement.Registration.ToList();
            foreach (ParameterCollection parameterCollection in parameterCollectionList)
            {
                ParameterSettingMap parameterMap = new ParameterSettingMap();
                foreach (ParameterElement parameterElement in parameterCollection)
                {
                    if (!String.IsNullOrEmpty(parameterElement.Name))
                    {
                        parameterMap[parameterElement.Name.ToLower()] = parameterElement.Value;
                    }
                }
                webSectionMap[parameterCollection.WebSection] = new WebSectionSetting
                {
                    Parameters = parameterMap
                };
            }
            //+
            return new MinimaComponentSetting
            {
                WebSections = webSectionMap
            };
        }
    }
}