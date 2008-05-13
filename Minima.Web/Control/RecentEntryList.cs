using System;
using System.Web.UI;
using System.Web.UI.WebControls;
//+
namespace Minima.Web.Control
{
    [PartialCachingAttribute(3600, null, null, null, null, false)]
    public class RecentEntryList : RecentEntryListBase
    {
        protected Repeater rptRecentEntryList;

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
        //- @Ctor -//
        public RecentEntryList() { }

        //+
        //- #OnInit -//
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(Page_Load);
            base.OnInit(e);
        }

        //- #Page_Load -//
        protected void Page_Load(Object sender, EventArgs e)
        {
            rptRecentEntryList.DataSource = this.DataSource;
            rptRecentEntryList.DataBind();
        }

        //- $__BuildRepeaterControl -//
        private Repeater __BuildRepeaterControl()
        {
            Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.rptRecentEntryList = rpt;
            rpt.ItemTemplate = new RecentEntryTemplate();
            rpt.ID = "rptRecentEntryList";
            return rpt;
        }

        //- $__BuildControlTree -//
        protected override void __BuildControlTree(General.Web.Control.DataUserControlBase __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            __parser.AddParsedSubObject(new LiteralControl("<h2>Previous Posts</h2>"));
            __parser.AddParsedSubObject(new LiteralControl("<ul id=\"recentPosts\">"));
            //+
            Repeater repeater = this.__BuildRepeaterControl();
            __parser.AddParsedSubObject(repeater);
            //+
            __parser.AddParsedSubObject(new LiteralControl("</ul>"));
        }
    }
}