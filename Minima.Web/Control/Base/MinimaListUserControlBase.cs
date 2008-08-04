using System;
//+
using Minima.Web.Routing;
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
                if (this.WebSection != Themelia.Web.WebSection.Current)
                {
                    return WebSectionAccessor.GetBlogGuid(this.WebSection);
                }
                //+
                return Themelia.Web.HttpData.GetScopedItem<String>("Minima", "BlogGuid");
            }
        }

        //- @WebSection -//
        public String WebSection
        {
            get
            {
                if (!String.IsNullOrEmpty(webSection))
                {
                    return webSection;
                }
                return Themelia.Web.WebSection.Current;
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