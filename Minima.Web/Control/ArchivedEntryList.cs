using System;
using System.Web.UI;
using System.Web.UI.WebControls;
//+
namespace Minima.Web.Control
{
    [PartialCachingAttribute(3600, null, null, null, null, false)]
    public class ArchivedEntryList : ArchivedEntryListBase
    {
        protected Repeater rptArchivedEntryList;

        //+
        //- $ArchiveEntryTemplate -//
        private class ArchiveEntryTemplate : ITemplate
        {
            public void InstantiateIn(System.Web.UI.Control container)
            {
                System.Web.UI.WebControls.Literal literal = new System.Web.UI.WebControls.Literal();
                literal.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                {
                    IDataItemContainer item = (IDataItemContainer)container;
                    String url = DataBinder.Eval(item.DataItem, "Url").ToString();
                    String monthText = DataBinder.Eval(item.DataItem, "MonthText").ToString();
                    String year = DataBinder.Eval(item.DataItem, "Year").ToString();
                    String count = DataBinder.Eval(item.DataItem, "Count").ToString();
                    //+
                    literal.Text = @"<li><a href=""{Url}"">{MonthText} {Year} ({Count})</a></li>"
                        .Replace("{Url}", url)
                        .Replace("{MonthText}", monthText)
                        .Replace("{Year}", year)
                        .Replace("{Count}", count);
                });
                container.Controls.Add(literal);
            }
        }

        //+
        //- @Ctor -//
        public ArchivedEntryList() { }

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
            rptArchivedEntryList.DataSource = this.DataSource;
            rptArchivedEntryList.DataBind();
        }

        //- $__BuildRepeaterControl -//
        private Repeater __BuildRepeaterControl()
        {
            Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.rptArchivedEntryList = rpt;
            rpt.ItemTemplate = new ArchiveEntryTemplate();
            rpt.ID = "rptArchivedEntryList";
            return rpt;
        }

        //- $__BuildControlTree -//
        private void __BuildControlTree(ArchivedEntryList __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            __parser.AddParsedSubObject(new LiteralControl("<h2>Archives</h2>"));
            __parser.AddParsedSubObject(new LiteralControl("<ul id=\"recent\">"));
            //+
            Repeater repeater = this.__BuildRepeaterControl();
            __parser.AddParsedSubObject(repeater);
            //+
            __parser.AddParsedSubObject(new LiteralControl("</ul>"));
        }

        //- #FrameworkInitialize -//
        protected override void FrameworkInitialize()
        {
            base.FrameworkInitialize();
            this.__BuildControlTree(this);
        }
    }
}