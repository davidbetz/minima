using System;
using System.Web.UI;
using System.Web.UI.WebControls;
//+
//+
namespace Minima.Web.Control
{
    [PartialCachingAttribute(3600, null, null, null, null, false)]
    public class LabelList : LabelListBase
    {
        protected Repeater rptLabelList;

        //+
        //- $ArchiveEntryTemplate -//
        private class LabelTemplate : ITemplate
        {
            public void InstantiateIn(System.Web.UI.Control container)
            {
                System.Web.UI.WebControls.Literal literal = new System.Web.UI.WebControls.Literal();
                literal.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                {
                    IDataItemContainer item = (IDataItemContainer)container;
                    String url = DataBinder.Eval(item.DataItem, "Url").ToString();
                    String title = DataBinder.Eval(item.DataItem, "Title").ToString();
                    String entryCount = DataBinder.Eval(item.DataItem, "EntryCount").ToString();
                    //+
                    literal.Text = String.Format(@"<li><a href=""{0}"">{1} ({2})</a></li>", url, title, entryCount);
                });
                container.Controls.Add(literal);
            }
        }

        //+
        //- @Ctor -//
        public LabelList() { }

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
            rptLabelList.DataSource = this.DataSource;
            rptLabelList.DataBind();
        }

        //- $__BuildRepeaterControl -//
        private Repeater __BuildRepeaterControl()
        {
            Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.rptLabelList = rpt;
            rpt.ItemTemplate = new LabelTemplate();
            rpt.ID = "rptLabelList";
            return rpt;
        }

        //- $__BuildControlTree -//
        private void __BuildControlTree(LabelList __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            __parser.AddParsedSubObject(new LiteralControl("<h2>Labels</h2>"));
            __parser.AddParsedSubObject(new LiteralControl("<ul id=\"labels\">"));
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