#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia.Web;
using Themelia.Web.Processing.Data;
//+
namespace Minima.Web.Controls
{
    public abstract class MinimaListUserControlBase : Themelia.Web.Controls.DataUserControlBase
    {
        protected System.Web.UI.WebControls.Repeater repeater;
        private String webDomain;

        //+
        //- @BlogGuid -//
        public String BlogGuid
        {
            get
            {
                WebDomainData webDomain = WebDomain.Current;
                if (webDomain != null && this.WebDomainName != webDomain.Name)
                {
                    return WebDomainDataList.AllWebDomainData[this.WebDomainName].ComponentDataList["Minima"].ParameterDataList[Info.BlogGuid].Value;
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
                if (Themelia.Web.WebDomain.Current != null)
                {
                    return Themelia.Web.WebDomain.Current.Name;
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