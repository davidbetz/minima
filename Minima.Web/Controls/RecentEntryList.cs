#region Copyright
//+ Copyright � Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Service.Agent;
//+
namespace Minima.Web.Controls
{
    [PartialCachingAttribute(3600, null, null, null, null, false)]
    public class RecentEntryList : MinimaListUserControlBase
    {
        //+
        //- $ArchiveEntryTemplate -//
        private class RecentEntryTemplate : ITemplate
        {
            //- @WebDomainName -//
            public String WebDomainName { get; set; }

            //+
            //- @InstantiateIn -//
            public void InstantiateIn(System.Web.UI.Control container)
            {
                System.Web.UI.WebControls.Literal literal = new System.Web.UI.WebControls.Literal();
                literal.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                {
                    IDataItemContainer item = (IDataItemContainer)container;
                    String url = DataBinder.Eval(item.DataItem, "Url").ToString();
                    String title = DataBinder.Eval(item.DataItem, "Title").ToString();
                    //+
                    if (!String.IsNullOrEmpty(this.WebDomainName))
                    {
                        url = Themelia.Web.WebDomain.GetUrl(this.WebDomainName) + Themelia.Web.UrlCleaner.FixWebPathHead(url);
                    }
                    literal.Text = @"<li><a href=""{Url}"">{Title}</a></li>"
                        .Replace("{Url}", url)
                        .Replace("{Title}", title);
                });
                container.Controls.Add(literal);
            }
        }

        //+
        //- @Heading -//
        public String Heading { get; set; }

        //- @ShowHeader -//
        public Boolean ShowHeading { get; set; }

        //- @MaxEntryCount -//
        public Int32 MaxEntryCount { get; set; }

        //- @HeadingIsLink -//
        public Boolean HeadingIsLink { get; set; }

        //- @ListCssClass -//
        public String ListCssClass { get; set; }

        //+
        //- @Ctor -//
        public RecentEntryList()
        {
            ShowHeading = true;
        }

        //+
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            Int32 maxEntryCount = BlogSection.GetConfigSection().EntriesToShow;
            if (this.MaxEntryCount > 0)
            {
                maxEntryCount = this.MaxEntryCount;
            }
            List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(this.BlogGuid, maxEntryCount, false, BlogEntryRetreivalType.Full);
            //+
            return blogEntryList.Select(p => new
            {
                Url = "/" + Themelia.Web.UrlCleaner.FixWebPathHead(p.MappingNameList.First()),
                Title = p.Title
            });
        }

        //- #__BuildRepeaterControl -//
        protected override System.Web.UI.WebControls.Repeater __BuildRepeaterControl()
        {
            System.Web.UI.WebControls.Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.repeater = rpt;
            rpt.ItemTemplate = new RecentEntryTemplate { WebDomainName = this.WebDomainName };
            rpt.ID = "rptRecentEntryList";
            return rpt;
        }

        //- $__BuildControlTree -//
        protected override void __BuildControlTree(Themelia.Web.Controls.DataUserControlBase __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            if (this.ShowHeading)
            {
                String heading = "Previous Posts";
                if (!String.IsNullOrEmpty(this.Heading))
                {
                    heading = this.Heading;
                }
                if (this.HeadingIsLink)
                {
                    heading = String.Format(@"<a href=""{0}"">{1}</a>", Themelia.Web.WebDomain.GetUrl(Themelia.Web.WebDomain.GetCleanWebDomain(this.WebDomainName)), heading);
                }
                __parser.AddParsedSubObject(new LiteralControl("<h2>" + heading + "</h2>"));
            }
            String listCssClass = "recentPosts";
            if (!String.IsNullOrEmpty(this.ListCssClass))
            {
                listCssClass = this.ListCssClass;
            }
            __parser.AddParsedSubObject(new LiteralControl("<ul id=\"" + listCssClass + "\">"));
            //+
            System.Web.UI.WebControls.Repeater repeater = this.__BuildRepeaterControl();
            __parser.AddParsedSubObject(repeater);
            //+
            __parser.AddParsedSubObject(new LiteralControl("</ul>"));
        }
    }
}