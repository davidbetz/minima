using System;
//+
using Themelia.Web;
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Control
{
    public abstract class MinimaListUserControlBase : Themelia.Web.Control.DataUserControlBase
    {
        protected System.Web.UI.WebControls.Repeater repeater;
        private String webDomain;

        //+
        //- @BlogGuid -//
        public String BlogGuid
        {
            get
            {
                WebDomainData webDomain = WebDomain.CurrentData;
                if (webDomain != null && this.WebDomainName != webDomain.Name)
                {
                    return WebDomainDataList.AllWebDomainData[this.WebDomainName].ComponentDataList[Info.Scope].ParameterDataList[Info.BlogGuid].Value;
                }
                //+
                return HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            }
        }

        //- @WebDomain -//
        public String WebDomainName
        {
            get
            {
                if (!String.IsNullOrEmpty(webDomain))
                {
                    return webDomain;
                }
                if (Themelia.Web.WebDomain.CurrentData != null)
                {
                    return Themelia.Web.WebDomain.CurrentData.Name;
                }
                //+
                return String.Empty;
            }
            set
            {
                webDomain = value;
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