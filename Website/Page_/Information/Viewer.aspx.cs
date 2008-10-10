using System;
//+
namespace WebSite.Information
{
    public partial class Viewer : System.Web.UI.Page
    {
        //- #OnInit -//
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(Page_Load);
            //+
            base.OnInit(e);
        }

        //- $Page_Load -//
        private void Page_Load(Object sender, EventArgs e)
        {
        }
    }
}