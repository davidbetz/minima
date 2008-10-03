#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Web.UI;
//+
namespace Minima.Web.Controls
{
    public class FeedBurnerPostFooter : PostFooterBase
    {
        public static Type Type = typeof(FeedBurnerPostFooter);

        //+
        //- @FeedBurnerUrl -//
        public String FeedBurnerUrl { get; set; }

        //- @Ctor -//
        public FeedBurnerPostFooter(params Object[] parameterArray)
        {
            FeedBurnerUrl = (String)parameterArray[0];
        }

        //+
        //- #Render -//
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            String url = DataBinder.Eval(this.Data, "Url") as String;
            //+
            writer.Write(@"
<div class=""feedburner-tracker"">
<script src=""" + this.FeedBurnerUrl + @"?i=" + url + @""" type=""text/javascript"" charset=""utf-8"">
</script>
</div>
");
        }
    }
}