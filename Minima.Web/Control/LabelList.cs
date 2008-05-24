using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
//+
using Minima.Service;
using Minima.Web.Agent;
//+
namespace Minima.Web.Control
{
    [PartialCachingAttribute(3600, null, null, null, null, false)]
    public class LabelList : MinimaListUserControlBase
    {
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
                    literal.Text = @"<li><a href=""{Url}"">{Title} ({EntryCount})</a></li>"
                        .Replace("{Url}", url)
                        .Replace("{Title}", title)
                        .Replace("{EntryCount}", entryCount);
                });
                container.Controls.Add(literal);
            }
        }

        //+
        //- @Heading -//
        public String Heading { get; set; }

        //- @ListCssClass -//
        public String ListCssClass { get; set; }

        //+
        //- @Ctor -//
        public LabelList() { }

        //+
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            List<Label> labelList = LabelAgent.GetBlogLabelList(this.BlogGuid);
            return labelList.Select(label => new
            {
                Title = label.Title,
                Url = LabelHelper.GetLabelUrl(label),
                EntryCount = label.BlogEntryCount
            });
        }

        //- $__BuildRepeaterControl -//
        private System.Web.UI.WebControls.Repeater __BuildRepeaterControl()
        {
            System.Web.UI.WebControls.Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.repeater = rpt;
            rpt.ItemTemplate = new LabelTemplate();
            rpt.ID = "rptLabelList";
            return rpt;
        }

        //- $__BuildControlTree -//
        protected override void __BuildControlTree(Themelia.Web.Control.DataUserControlBase __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            String heading = "Labels";
            if (!String.IsNullOrEmpty(this.Heading))
            {
                heading = this.Heading;
            }
            String listCssClass = "labels";
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