using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Web.Agent;
using Minima.Web.Helper;
//+
namespace Minima.Web.Control
{
    [PartialCachingAttribute(3600, null, null, null, null, false)]
    public class RecentEntryList : MinimaListUserControlBase
    {
        //+
        //- $ArchiveEntryTemplate -//
        private class RecentEntryTemplate : ITemplate
        {
            public void InstantiateIn(System.Web.UI.Control container)
            {
                System.Web.UI.WebControls.Literal literal = new System.Web.UI.WebControls.Literal();
                literal.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                {
                    IDataItemContainer item = (IDataItemContainer)container;
                    String url = DataBinder.Eval(item.DataItem, "Url").ToString();
                    String title = DataBinder.Eval(item.DataItem, "Title").ToString();
                    //+
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

        //- @MaxEntryCount -//
        public Int32 MaxEntryCount { get; set; }

        //- @HeadingIsLink -//
        public Boolean HeadingIsLink { get; set; }

        //- @ListCssClass -//
        public String ListCssClass { get; set; }

        //+
        //- @Ctor -//
        public RecentEntryList() { }

        //+
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            Int32 maxEntryCount = MinimaConfiguration.RecentEntriesToShow;
            if (this.MaxEntryCount > 0)
            {
                maxEntryCount = this.MaxEntryCount;
            }
            List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(this.BlogGuid, maxEntryCount, false);
            //+
            return blogEntryList.Select(p => new
            {
                Url = BlogEntryHelper.BuildBlogEntry(p.PostDateTime, p.MappingNameList.First(), Themelia.Web.WebSection.CleanWebSection(this.WebSection)),
                Title = p.Title
            });
        }

        //- #__BuildRepeaterControl -//
        protected override System.Web.UI.WebControls.Repeater __BuildRepeaterControl()
        {
            System.Web.UI.WebControls.Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.repeater = rpt;
            rpt.ItemTemplate = new RecentEntryTemplate();
            rpt.ID = "rptRecentEntryList";
            return rpt;
        }

        //- $__BuildControlTree -//
        protected override void __BuildControlTree(Themelia.Web.Control.DataUserControlBase __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            String heading = "Previous Posts";
            if (!String.IsNullOrEmpty(this.Heading))
            {
                heading = this.Heading;
            }
            if (this.HeadingIsLink)
            {
                heading = String.Format(@"<a href=""{0}"">{1}</a>", Themelia.Web.WebSection.GetUrl(Themelia.Web.WebSection.CleanWebSection(this.WebSection)), heading);
            }
            String listCssClass = "recentPosts";
            if (!String.IsNullOrEmpty(this.ListCssClass))
            {
                listCssClass = this.ListCssClass;
            }
            __parser.AddParsedSubObject(new LiteralControl("<h2>" + heading + "</h2>"));
            __parser.AddParsedSubObject(new LiteralControl("<ul id=\"" + listCssClass + "\">"));
            //+
            System.Web.UI.WebControls.Repeater repeater = this.__BuildRepeaterControl();
            __parser.AddParsedSubObject(repeater);
            //+
            __parser.AddParsedSubObject(new LiteralControl("</ul>"));
        }
    }
}