using System;
//+
namespace Minima.Web
{
    public static class LabelHelper
    {
        //- @GetLabelUrl -//
        public static String GetLabelUrl(Minima.Service.Label label)
        {
            String webDomain = Themelia.Web.WebDomain.Current ?? String.Empty;
            if (webDomain.ToLower() == "root")
            {
                webDomain = String.Empty;
            }
            else
            {
                webDomain = "/" + webDomain;
            }
            return webDomain + "/label/" + (!String.IsNullOrEmpty(label.FriendlyTitle) ? label.FriendlyTitle : label.Title).ToLower();
        }
    }
}