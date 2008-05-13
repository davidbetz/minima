using System;
using Minima.Web.Routing;
//+
namespace Minima.Web.Control
{
    public abstract class MinimaListUserControlBase : General.Web.Control.DataUserControlBase
    {
        protected System.Web.UI.WebControls.Repeater repeater;

        //+
        //- @BlogGuid -//
        public String BlogGuid
        {
            get
            {
                if (!String.IsNullOrEmpty(this.WebSection) && this.WebSection != ContextItemSet.WebSection)
                {
                    return WebSectionAccessor.GetBlogGuid(this.WebSection);
                }
                //+
                return ContextItemSet.BlogGuid;
            }
        }

        //- @WebSection -//
        public String WebSection { get; set; }

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
    }
}