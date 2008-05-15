using System;
using System.Collections.Generic;
using System.Web;
//+
using General.ExceptionHandling;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Web.Agent;
using Minima.Web.Data.Entity;
//+
namespace Minima.Web.Control
{
    public abstract class MinimaBlogBase : System.Web.UI.Control
    {
        private List<BlogEntry> dataSource;

        //+
        //- @BlogEntryGuid -//
        public String BlogEntryGuid { get; private set; }

        //- @BlogGuid -//
        public static String BlogGuid
        {
            get
            {
                return HttpContext.Current.Items["BlogGuid"] as String;
            }
        }

        //- @Label -//
        public String Label
        {
            get
            {
                String label = String.Empty;
                if (Context.Items["label"] != null)
                {
                    label = (String)Context.Items["label"];
                }
                //+
                return label;
            }
        }

        //- @Archive -//
        public String Archive
        {
            get
            {
                String archive = String.Empty;
                if (Context.Items["archive"] != null)
                {
                    archive = (String)Context.Items["archive"];
                }
                //+
                return archive;
            }
        }

        //- @Link -//
        public String Link
        {
            get
            {
                String link = String.Empty;
                if (Context.Items["link"] != null)
                {
                    link = (String)Context.Items["link"];
                }
                //+
                return link;
            }
        }

        //- @IsLinkAccess -//
        public Boolean IsLinkAccess
        {
            get
            {
                return !String.IsNullOrEmpty(this.Link);
            }
        }

        //- @IsArchiveAccess -//
        public Boolean IsArchiveAccess
        {
            get
            {
                return !String.IsNullOrEmpty(this.Archive);
            }
        }

        //- @IsLabelAccess -//
        public Boolean IsLabelAccess
        {
            get
            {

                return !String.IsNullOrEmpty(this.Label);
            }
        }

        //- #DataSource -//
        protected List<BlogEntry> DataSource
        {
            get
            {
                if (dataSource == null)
                {
                    dataSource = GetDataSource();
                }
                //+
                return dataSource;
            }
        }

        //+
        //- $GetDataSource -//
        private List<BlogEntry> GetDataSource()
        {
            if (String.IsNullOrEmpty(ContextItemSet.BlogGuid))
            {
                throw new ArgumentNullException("BlogGuid is required.");
            }
            //+
            List<BlogEntry> blogEntryList = BlogAgent.GetNetBlogEntryList(ContextItemSet.BlogGuid, this.Label, this.Archive, this.Link, MinimaConfiguration.RecentEntriesToShow);
            //+
            BlogEntryActivity blogEntryActivity = new BlogEntryActivity();
            blogEntryActivity.BlogEntryActivityBrowser = HttpContext.Current.Request.UserAgent;
            blogEntryActivity.BlogEntryActivityTime = DateTime.Now;
            blogEntryActivity.BlogEntryActivityAddress = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
            blogEntryActivity.BlogEntryActivitySessionId = (String)HttpContext.Current.Session["SessionId"];
            blogEntryActivity.BlogEntryActivityTypeId = 0;
            //+
            if (this.IsLabelAccess)
            {
                blogEntryActivity.BlogEntryActivityTypeId = 6;
                if (blogEntryList == null)
                {
                    blogEntryActivity.BlogEntryActivityExtra = "Invalid label accessed";
                    ExceptionManager.Report("Invalid label accessed", this.Context);
                }
                blogEntryActivity.BlogEntryActivityExtra = this.Label;
                blogEntryActivity.BlogEntryActivityTypeId = 2;
            }
            if (this.IsArchiveAccess)
            {
                String[] parts = this.Archive.Split("/".ToCharArray());
                Int32 year = Int32.Parse(parts[0]);
                Int32 month = Int32.Parse(parts[1]);
                blogEntryActivity.BlogEntryActivityTypeId = 6;
                if (blogEntryList == null)
                {
                    blogEntryActivity.BlogEntryActivityExtra = "Invalid year/month accessed";
                    ExceptionManager.Report("Invalid year/month accessed", this.Context);
                }
                blogEntryActivity.BlogEntryActivityExtra = this.Archive;
                blogEntryActivity.BlogEntryActivityTypeId = 4;
            }
            if (this.IsLinkAccess)
            {
                if (blogEntryList != null)
                {
                    this.BlogEntryGuid = blogEntryList[0].Guid;
                    blogEntryActivity.BlogEntryActivityTypeId = 3;
                    blogEntryActivity.BlogEntryActivityExtra = this.Link;
                }
                else
                {
                    blogEntryActivity.BlogEntryActivityExtra = "Invalid URL accessed";
                    ExceptionManager.Report("Invalid URL accessed", this.Context);
                    blogEntryActivity.BlogEntryActivityTypeId = 6;
                }
            }
            //+
            if (blogEntryActivity.BlogEntryActivityTypeId == 0)
            {
                blogEntryActivity.BlogEntryActivityTypeId = 1;
            }
            //+
            BlogEntryActivityAgent.ReportActivity(blogEntryActivity);
            // it's none of the above.
            if (blogEntryList == null || blogEntryList.Count < 1)
            {
                if (String.IsNullOrEmpty(this.Link) && String.IsNullOrEmpty(this.Archive) && String.IsNullOrEmpty(this.Label))
                {
                    //blogEntryList = EntityDecorator.DecorateBlogEntryList(BlogAgent.GetBlogEntryList(MinimaConfiguration.BlogGuid, MinimaConfiguration.ViewableBlogEntryCount));
                }
            }
            //+
            return blogEntryList;
        }
    }
}