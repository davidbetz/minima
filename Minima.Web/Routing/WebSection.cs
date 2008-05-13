using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Web.Configuration;
//+
namespace Minima.Web.Routing
{
    public static class WebSectionAccessor
    {
        public static String GetBlogGuid(String webSection)
        {
            if (String.IsNullOrEmpty(webSection))
            {
                return String.Empty;
            }
            webSection = webSection.ToLower();
            //+
            List<InstanceElement> instanceElementList = MinimaConfigurationFacade.GetWebConfiguration().Registration.OrderBy(p => p.Priority).ToList();
            InstanceElement t = instanceElementList.FirstOrDefault(u => u.WebSection != null && u.WebSection == webSection);
            if (t != null)
            {
                return t.BlogGuid;
            }
            return String.Empty;
        }
    }
}