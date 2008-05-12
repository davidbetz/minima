using System;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Web;
using Minima.Web.Agent;
//+
public partial class SecondBlogPage : System.Web.UI.Page
{
    //- #OnInit -//
    protected override void OnInit(EventArgs e)
    {
        this.Load += new EventHandler(Page_Load);
        //+
        phMinimaBlog.Controls.Add(new Minima.Web.Control.MinimaBlog
        {
            SupportCommenting = false
        });
        //+
        base.OnInit(e);
    }

    //- $MasterPage_Load -//
    private void Page_Load(Object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BlogMetaData blogMetaData = BlogAgent.GetBlogMetaData(Minima.Web.Control.MinimaBlog.BlogGuid);
            //+
            hlBlogUrl.NavigateUrl = blogMetaData.Uri.AbsoluteUri;
            hlBlogUrl.Text = blogMetaData.Title;
            this.Page.Title = blogMetaData.Title;
            //+
            litBlogDescription.Text = blogMetaData.Description;
        }
    }
}