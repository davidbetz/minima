#region Copyright
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
        public override void Register(FactoryDataList factoryDataList, ProcessorDataList processorDataList, HandlerDataList handlerDataList, AliasDataList aliasDataList, RedirectDataList redirectDataList)
        {
            handlerDataList.Add(HandlerData.Create("__$Minima$UrlProcessing", "contains", "/"));
            handlerDataList.Add(HandlerData.Create("__$Minima$SiteMap", "endswith", "/blogmap.xml"));
            handlerDataList.Add(HandlerData.Create("__$Minima$Image", "contains", "/imagestore/"));
            //+
            factoryDataList.Add(FactoryData.Create("Minima.Web.Routing.ProcessorFactory, Minima.Web"));
            processorDataList.Add(ProcessorData.Create<ProcessorData>("__$Minima$PreProcessor"));
        }
    }
}