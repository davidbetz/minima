#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Web.UI;
//+
namespace Minima.Web.Controls
{
    public class SamplePostFooter : PostFooterBase
    {
        public static Type Type = typeof(SamplePostFooter);

        //+
        //- @Data -//
        public String TestProperty { get; set; }

        //- @Ctor -//
        public SamplePostFooter(params Object[] parameterArray)
        {
            TestProperty = (String)parameterArray[0];
        }

        //+
        //- #Render -//
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            String url = (DataBinder.Eval(this.Data, "Url") as String) ?? String.Empty;
            //+
            writer.Write(@"
<div>
This is the sample footer.  The TestData propery contains '" + this.TestProperty + @"'. The current post URL is '" + url + @"'
</div>
");
        }
    }
}