using System.Collections.Generic;
using System.Linq;
using System.Web;
//+
using Minima.Web.Configuration;
//+
namespace Minima.Web.Routing
{
    public class HttpHandlerPreProcessor : General.Web.Routing.PreProcessorBase
    {
        //- @OnPreHttpHandlerExecute -//
        public override void OnPreHttpHandlerExecute(HttpContext context)
        {
            List<InstanceElement> instanceElementList = MinimaConfigurationFacade.GetWebConfiguration().Registration.OrderBy(p => p.Priority).ToList();
            InstanceElement t = instanceElementList.FirstOrDefault(u => u.WebSection != null && ContextItemSet.WebSection.ToLower().Contains(u.WebSection.ToLower()));
            if (t != null)
            {
                context.Items.Add("BlogGuid", t.BlogGuid);
            }
        }
    }
}