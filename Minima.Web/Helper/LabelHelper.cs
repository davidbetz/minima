#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
namespace Minima.Web
{
    public static class LabelHelper
    {
        //- @GetLabelUrl -//
        public static String GetLabelUrl(Minima.Service.Label label)
        {
            String webDomain = Themelia.Web.WebDomain.Path ?? String.Empty;
            if (webDomain.ToLower() == "root")
            {
                webDomain = String.Empty;
            }
            else
            {
                webDomain = "/" + webDomain;
            }
            return Themelia.Web.Http.Root + webDomain + "label/" + (!String.IsNullOrEmpty(label.FriendlyTitle) ? label.FriendlyTitle : label.Title).ToLower();
        }
    }
}