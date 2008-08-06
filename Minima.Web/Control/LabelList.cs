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
        List<Label> rawDataSource;

        //+
        //- @Heading -//
        public String Heading { get; set; }

        //- @ListCssClass -//
        public String ListCssClass { get; set; }

        //- @TemplateType -//
        public Type TemplateType { get; set; }

        //- @RawDataSource -//
        public List<Label> RawDataSource
        {
            get
            {
                if (rawDataSource == null)
                {
                    this.rawDataSource = LabelAgent.GetBlogLabelList(this.BlogGuid);
                }
                //+
                return rawDataSource;
            }
        }

        //+
        //- @Ctor -//
        public LabelList() { }

        //+
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            return rawDataSource.OrderBy(p => p.Title).Select(label => new
            {
                Title = label.Title,
                Url = LabelHelper.GetLabelUrl(label),
                EntryCount = label.BlogEntryCount
            });
        }

        //- $GetTemplate -//
        private ITemplate GetTemplate(System.Web.UI.WebControls.ListItemType type)
        {
            String listCssClass = "labels";
            if (!String.IsNullOrEmpty(this.ListCssClass))
            {
                listCssClass = this.ListCssClass;
            }
            if (this.TemplateType == null)
            {
                return LabelListTemplateFactory.CreateTemplate(LabelListTemplateFactory.TemplateType.Linear, type, listCssClass, this.RawDataSource);
            }
            //+
            return (ITemplate)Themelia.Activation.ObjectCreator.Create(this.TemplateType, type, listCssClass, this.RawDataSource);
        }

        //- #__BuildRepeaterControl -//
        protected override System.Web.UI.WebControls.Repeater __BuildRepeaterControl()
        {
            System.Web.UI.WebControls.Repeater rpt = new System.Web.UI.WebControls.Repeater();
            this.repeater = rpt;
            rpt.HeaderTemplate = GetTemplate(System.Web.UI.WebControls.ListItemType.Header);
            rpt.ItemTemplate = GetTemplate(System.Web.UI.WebControls.ListItemType.Item);
            rpt.FooterTemplate = GetTemplate(System.Web.UI.WebControls.ListItemType.Footer);
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
            __parser.AddParsedSubObject(new LiteralControl("<h2>" + heading + "</h2>"));
            //+
            System.Web.UI.WebControls.Repeater repeater = this.__BuildRepeaterControl();
            __parser.AddParsedSubObject(repeater);
        }
    }
}