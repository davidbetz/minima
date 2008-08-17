using System;
//+
using Minima.Web.Routing;
using Themelia.Web;
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Control
{
    public abstract class MinimaListUserControlBase : Themelia.Web.Control.DataUserControlBase
    {
        protected System.Web.UI.WebControls.Repeater repeater;
        private String webSection;

        //+
        //- @BlogGuid -//
        public String BlogGuid
        {
            get
            {
                WebSectionData webSection = WebSection.CurrentData;
                if (this.WebSectionName != webSection.Name)
                {
                    return WebSectionDataList.AllWebSectionData[this.WebSectionName].ComponentDataList[Info.Scope].ParameterDataList[Info.BlogGuid].Value;
                }
                //+
                return HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            }
        }

        //- @WebSection -//
        public String WebSectionName
        {
            get
            {
                if (!String.IsNullOrEmpty(webSection))
                {
                    return webSection;
                }
                return Themelia.Web.WebSection.CurrentData.Name;
            }
            set
            {
                webSection = value;
            }
        }

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
            repeater.DataSource = this.DataSource;
            repeater.DataBind();
        }

        //- #__BuildRepeaterControl -//
        protected abstract System.Web.UI.WebControls.Repeater __BuildRepeaterControl();
    }
}