using System;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Service.Agent;
using Minima.Web;
//+
namespace WebSite.Blog
{
    public partial class Root : System.Web.UI.Page
    {
        //- #OnInit -//
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(Page_Load);
            //+
            phLabelList.Controls.Add(new Minima.Web.Controls.LabelList());
            phArchivedEntryList.Controls.Add(new Minima.Web.Controls.ArchivedEntryList());
            phRecentEntryList.Controls.Add(new Minima.Web.Controls.RecentEntryList());
            phRecentEntryListSecondary.Controls.Add(new Minima.Web.Controls.RecentEntryList { WebDomainName = "second", Heading = "Second Blog", HeadingIsLink = true });
            phMinimaBlog.Controls.Add(new Minima.Web.Controls.MinimaBlog
            {
                PostFooterTypeInfo = Themelia.Activation.TypeActivationInfo.GetInfo(Minima.Web.Controls.SamplePostFooter.Type, "Data Here")
            });
            //+
            base.OnInit(e);
        }

        //- $Page_Load -//
        private void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String domain = Minima.Configuration.BlogSection.GetConfigSection().Domain;
                String blogGuid = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
                BlogMetaData blogMetaData = BlogAgent.GetBlogMetaData(blogGuid);
                //+
                rsd.Attributes.Add("href", Themelia.Web.UrlCleaner.FixWebPathTail(domain) + "/rsd.xml");
                wlwmanifest.Attributes.Add("href", Themelia.Web.UrlCleaner.FixWebPathTail(domain) + "/wlwmanifest.xml");
                //+
                hlBlogUrl.NavigateUrl = blogMetaData.Uri.AbsoluteUri;
                hlBlogUrl.Text = blogMetaData.Title;
                //+
                image.Attributes.Add("onclick", "window.location='" + domain + "'");
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
                //+
            }
        }

        //- #OnPreRender -//
        protected override void OnPreRender(EventArgs e)
        {
            String pageTitle = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, "PageTitle");
            if (!String.IsNullOrEmpty(pageTitle))
            {
                this.Page.Title = pageTitle;
            }
            //+
            base.OnPreRender(e);
        }
    }
}