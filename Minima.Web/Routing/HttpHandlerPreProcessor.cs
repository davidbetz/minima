using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            String webSection = String.Empty;
            String absoluteUrl = context.Request.Url.AbsoluteUri;
            String absolutePath = context.Request.Url.AbsolutePath;
            //+
            List<InstanceElement> instanceElementList = Minima.Web.Configuration.MinimaConfigurationFacade.GetWebConfiguration().Registration.OrderBy(p => p.Priority).ToList();
            InstanceElement t = instanceElementList.Where(p => p.WebSection != "/").FirstOrDefault(u => absolutePath.ToLower().Contains(u.WebSection.ToLower()));
            if (t != null)
            {
                blogGuid = t.BlogGuid;
                webSection = t.WebSection;
            }
            else
            {
                t = instanceElementList.FirstOrDefault(u => u.WebSection == "/");
                if (t != null)
                {
                    blogGuid = t.BlogGuid;
                    webSection = t.WebSection;
                }
            }
            //+
            context.Items.Add("BlogGuid", blogGuid);
            context.Items.Add("WebSection", webSection);
        }
    }
}