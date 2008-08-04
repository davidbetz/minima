using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
//+
using Minima.Service;
using Minima.Web.Agent;
using Minima.Web.Helper;
using Themelia.Web;
//+
namespace Minima.Web.Control
{
    //[PartialCachingAttribute(3600, null, null, null, null, false)]
    public class IndexEntryList : MinimaListUserControlBase
    {
        //+
        //- @ShowDescending -//
        public Boolean ShowDescending { get; set; }

        //- @PageIndex -//
        public Int32 PageIndex { get; set; }

        //- @PageSize -//
        public Int32 PageSize { get; set; }

        //- @ListCssClass -//
        public String ListCssClass { get; set; }

        //- @AccessType -//
        public AccessType AccessType { get; set; }

        //- @Heading -//
        public String Heading { get; set; }

        //- @HeadingSuffix -//
        public String HeadingSuffix
        {
            get
            {
                switch (AccessType)
                {
                    case AccessType.Label:
                        return "Label Contents";
                    case AccessType.Archive:
                        return "Table of Contents";
                }
                //+
                return String.Empty;
            }
        }

        //+
        //- @Ctor -//
        public IndexEntryList(AccessType accessType, List<IndexEntry> dataSource)
        {
            if (accessType == AccessType.Default || accessType == AccessType.Link)
            {
                this.Visible = false;
            }
            else
            {
                this.AccessType = accessType;
                this.dataSource = dataSource;
            }
            this.Heading = GetHeading(accessType);
        }

        //+
        //- @GetHeading -//
        private String GetHeading(AccessType accessType)
        {
            String heading = String.Empty;
            if (accessType == AccessType.Archive)
            {
                Int32 year = HttpData.GetScopedItem<Int32>("Minima", "ArchiveYear");
                Int32 month = HttpData.GetScopedItem<Int32>("Minima", "ArchiveMonth");
                String monthName = Themelia.Data.Common.Date.GetMonthData()[month];
                //+
                heading = String.Format("{0} {1}", monthName, year);
            }
            else if (accessType == AccessType.Label)
            {
                heading = HttpData.GetScopedItem<String>("Minima", "LabelTitle");
            }
            //+
            return heading;
        }

        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            return null;
        }

        //- #__BuildRepeaterControl -//
        protected override System.Web.UI.WebControls.Repeater __BuildRepeaterControl()
        {
            System.Web.UI.WebControls.Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.repeater = rpt;
            rpt.HeaderTemplate = new IndexEntryTemplate(ListItemType.Header);
            rpt.ItemTemplate = new IndexEntryTemplate(ListItemType.Item);
            rpt.FooterTemplate = new IndexEntryTemplate(ListItemType.Footer);
            rpt.ID = "rptIndexEntryList";
            return rpt;
        }

        //- $__BuildControlTree -//
        protected override void __BuildControlTree(Themelia.Web.Control.DataUserControlBase __ctrl)
        {
            IParserAccessor __parser = ((IParserAccessor)(__ctrl));
            if (String.IsNullOrEmpty(this.Heading))
            {
                throw new ArgumentNullException("Heading may not be null");
            }
            String listCssClass = "index-section";
            if (!String.IsNullOrEmpty(this.ListCssClass))
            {
                listCssClass = this.ListCssClass;
            }
            __parser.AddParsedSubObject(new LiteralControl("<div class=\"" + listCssClass + "\">"));
            __parser.AddParsedSubObject(new LiteralControl(String.Format("<h3>{0} {1}</h3>", this.Heading, this.HeadingSuffix)));
            //+
            System.Web.UI.WebControls.Repeater repeater = this.__BuildRepeaterControl();
            __parser.AddParsedSubObject(repeater);
            //+
            __parser.AddParsedSubObject(new LiteralControl("</div>"));
        }
    }
}