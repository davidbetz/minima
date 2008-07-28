using System;
//+
namespace Minima.Web
{
    public static class LabelHelper
    {
        //- @GetLabelUrl -//
        public static String GetLabelUrl(Minima.Service.Label label)
        {
            String webSection = Themelia.Web.WebSection.Current ?? String.Empty;
            if (webSection.ToLower() == "root")
            {
                webSection = String.Empty;
            }
            else
            {
                webSection = "/" + webSection;
            }
            return webSection + "/label/" + (!String.IsNullOrEmpty(label.FriendlyTitle) ? label.FriendlyTitle : label.Title).ToLower();
        }
    }
}