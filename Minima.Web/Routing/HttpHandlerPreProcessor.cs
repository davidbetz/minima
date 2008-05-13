using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//+
//+
using Minima.Web.Configuration;
//+
namespace Minima.Web.Routing
{
    public class HttpHandlerPreProcessor : General.Web.Routing.HttpHandlerPreProcessorBase
    {
        //- @OnPreHttpHandlerExecute -//
        public override void OnPreHttpHandlerExecute(HttpContext context)
        {
            String blogGuid = String.Empty;
            String pageLocation = String.Empty;
            //+
            List<InstanceElement> instanceElementList = MinimaConfigurationFacade.GetWebConfiguration().Registration.OrderBy(p => p.Priority).ToList();
            InstanceElement t = instanceElementList.FirstOrDefault(u => u.WebSection != null && ContextItemSet.WebSection.ToLower().Contains(u.WebSection.ToLower()));
            if (t != null)
            {
                blogGuid = t.BlogGuid;
                pageLocation = t.Page;
                //+
                context.Items.Add("BlogGuid", blogGuid);
                context.Items.Add("BlogPage", pageLocation);
            }
        }
    }
}