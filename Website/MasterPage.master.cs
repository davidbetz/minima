using System;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Web;
using Minima.Web.Agent;
//+
public partial class MasterPage : System.Web.UI.MasterPage
{
    //- #OnInit -//
    protected override void OnInit(EventArgs e)
    {
        this.Load += new EventHandler(MasterPage_Load);
        base.OnInit(e);
    }

    //- $MasterPage_Load -//
    private void MasterPage_Load(Object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BlogMetaData blogMetaData = BlogAgent.GetBlogMetaData(MinimaConfiguration.BlogGuid);
            //+
            rsd.Attributes.Add("href", WebConfiguration.Domain + "rsd.xml");
            wlwmanifest.Attributes.Add("href", WebConfiguration.Domain + "wlwmanifest.xml");
            //+
            hlBlogUrl.NavigateUrl = WebConfiguration.Domain;
            hlBlogUrl.Text = blogMetaData.Title;
            this.Page.Title = blogMetaData.Title;
            image.Attributes.Add("onclick", "window.location='" + WebConfiguration.Domain + "'");
            //+
            rssLink.Attributes.Add("title", blogMetaData.FeedTitle);
            rssLink.Attributes.Add("href", blogMetaData.FeedUri.AbsoluteUri);
            rssLink.Attributes.Remove("id");
            //+
            hlFeedUrl.NavigateUrl = blogMetaData.FeedUri.AbsoluteUri;
            hlFeedUrl.Attributes.Add("title", "Subscribe to my feed");
            hlFeedUrl.Attributes.Add("rel", "alternate");
            hlFeedUrl.Attributes.Add("type", "application/rss+xml");
            //+
            litBlogDescription.Text = blogMetaData.Description;
        }
    }
}