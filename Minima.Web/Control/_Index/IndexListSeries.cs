using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Minima.Service;
//+
namespace Minima.Web.Control
{
    [ToolboxData("<{0}:IndexListSeries runat=\"server\"></{0}:IndexListSeries>")]
    public class IndexListSeries : MinimaListUserControlBase
    {
        //+
        //- @Ctor -//
        public IndexListSeries(List<IndexEntry> blogEntryDataSource)
        {
            this.BlogEntryDataSource = blogEntryDataSource;
        }

        //+
        //- @Year -//
        public Int32 Year { get; set; }

        //- @ListCssClass -//
        public String ListCssClass { get; set; }

        //- @HeadingSuffix -//
        public String HeadingSuffix { get; set; }

        //- @BlogEntryDataSource -//
        public List<IndexEntry> BlogEntryDataSource { get; set; }

        //+
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            return new List<Object>(new[]
            {
                new { Year=this.Year, Name="January", Number=1 },
                new { Year=this.Year, Name="February", Number=2 },
                new { Year=this.Year, Name="March", Number=3 },
                new { Year=this.Year, Name="April", Number=4 },
                new { Year=this.Year, Name="May", Number=5 },
                new { Year=this.Year, Name="June" ,Number=6 },
                new { Year=this.Year, Name="July", Number=7 },
                new { Year=this.Year, Name="August", Number=8 },
                new { Year=this.Year, Name="September", Number=9 },
                new { Year=this.Year, Name="October", Number=10 },
                new { Year=this.Year, Name="November", Number=11 },
                new { Year=this.Year, Name="December", Number=12 }
            });
        }

        //- #__BuildRepeaterControl -//
        protected override System.Web.UI.WebControls.Repeater __BuildRepeaterControl()
        {
            System.Web.UI.WebControls.Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.repeater = rpt;
            rpt.HeaderTemplate = new IndexListSeriesTemplate(ListItemType.Header);
            rpt.ItemTemplate = new IndexListSeriesTemplate(ListItemType.Item);
            rpt.ID = "rptIndexListSeries";
            return rpt;
        }

        //- #__BuildControlTree -//
        protected override void __BuildControlTree(Themelia.Web.Control.DataUserControlBase __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            String listCssClass = "index-series";
            if (!String.IsNullOrEmpty(this.ListCssClass))
            {
                listCssClass = this.ListCssClass;
            }
            __parser.AddParsedSubObject(new LiteralControl(String.Format("<div class=\"{0}\">", listCssClass)));
            __parser.AddParsedSubObject(new LiteralControl(String.Format("<h2>{0} {1}</h2>", this.Year, this.HeadingSuffix)));
            //+
            System.Web.UI.WebControls.Repeater repeater = this.__BuildRepeaterControl();
            __parser.AddParsedSubObject(repeater);
            __parser.AddParsedSubObject(new LiteralControl("</div>"));
        }
    }
}