using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//+
using General.Web;
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
            //+
            List<InstanceElement> instanceElementList = MinimaConfigurationFacade.GetWebConfiguration().Registration.OrderBy(p => p.Priority).ToList();
            InstanceElement t = instanceElementList.Where(p => p.WebSection != "root").FirstOrDefault(u => u.WebSection != null && Http.Url.AbsolutePath.ToLower().Contains(u.WebSection.ToLower()));
            if (t != null)
            {
                blogGuid = t.BlogGuid;
            }
            else
            {
                t = instanceElementList.FirstOrDefault(u => u.WebSection != null && u.WebSection.Equals("root", StringComparison.InvariantCultureIgnoreCase));
                if (t != null)
                {
                    blogGuid = t.BlogGuid;
                }
            }
            //+
            context.Items.Add("BlogGuid", blogGuid);
        }
    }
}