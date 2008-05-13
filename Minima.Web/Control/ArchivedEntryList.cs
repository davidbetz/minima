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
    public class ArchivedEntryList : MinimaListUserControlBase
    {
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
        //- @Heading -//
        public String Heading { get; set; }

        //- @ListCssClass -//
        public String ListCssClass { get; set; }

        //+
        //- @Ctor -//
        public ArchivedEntryList() { }

        //+
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            List<ArchiveCount> archiveList = BlogAgent.GetArchivedEntryList(this.BlogGuid);
            //+
            return archiveList.Select(p => new
            {
                Url = String.Format("/{0}/{1}", p.ArchiveDate.Year, p.ArchiveDate.ToString("MM")),
                MonthText = p.ArchiveDate.ToString("MMMM"),
                Year = p.ArchiveDate.Year,
                Count = p.Count
            });
        }

        //- $__BuildRepeaterControl -//
        private System.Web.UI.WebControls.Repeater __BuildRepeaterControl()
        {
            System.Web.UI.WebControls.Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.repeater = rpt;
            rpt.ItemTemplate = new ArchiveEntryTemplate();
            rpt.ID = "rptArchivedEntryList";
            return rpt;
        }

        //- $__BuildControlTree -//
        protected override void __BuildControlTree(General.Web.Control.DataUserControlBase __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            String heading = "Archives";
            if (!String.IsNullOrEmpty(this.Heading))
            {
                heading = this.Heading;
            }
            String listCssClass = "recent";
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