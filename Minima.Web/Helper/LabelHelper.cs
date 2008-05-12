using System;
//+
namespace Minima.Web
{
    public static class LabelHelper
    {
        //- @GetLabelUrl -//
        public static String GetLabelUrl(Minima.Service.Label label)
        {
            return General.Web.UrlHelper.FixWebPath(ContextItemSet.WebSection) + "/label/" + (!String.IsNullOrEmpty(label.FriendlyTitle) ? label.FriendlyTitle : label.Title).ToLower();
        }
    }
}