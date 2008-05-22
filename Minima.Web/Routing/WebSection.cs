using System;
using System.Linq;
//+
using Minima.Web.Routing.Component;
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
            MinimaComponentSetting.MinimaInfo currentInfo = MinimaComponentSetting.CurrentComponentSetting.GetParameterList().FirstOrDefault(u => u.WebSection != null && u.WebSection == webSection);
            if (currentInfo != null)
            {
                return currentInfo.BlogGuid;
            }
            return String.Empty;
        }
    }
}