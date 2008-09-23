using System;
using System.Collections.Generic;
using System.Web;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Service.Agent;
using Minima.Web.Agent;
using Minima.Web.Data.Entity;
//+
using Themelia.Tracing;
using Themelia.Web;
//+
namespace Minima.Web.Controls
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
                return HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            }
        }

        //- @Label -//
        public String Label
        {
            get
            {
                return HttpData.GetScopedItem<String>(Info.Scope, "Label");
            }
        }

        //- @Archive -//
        public String Archive
        {
            get
            {
                return HttpData.GetScopedItem<String>(Info.Scope, "Archive");
            }
        }

        //- @Link -//
        public String Link
        {
            get
            {
                return HttpData.GetScopedItem<String>(Info.Scope, "Link");
            }
        }

        //- @Index -//
        public Int32 Index
        {
            get
            {
                return HttpData.GetScopedItem<Int32>(Info.Scope, "Index");
            }
        }

        //- #AccessType -//
        public AccessType AccessType
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Link))
                {
                    return AccessType.Link;
                }
                if (!String.IsNullOrEmpty(this.Archive))
                {
                    return AccessType.Archive;
                }
                if (!String.IsNullOrEmpty(this.Label))
                {
                    return AccessType.Label;
                }
                if (this.Index > 0)
                {
                    return AccessType.Index;
                }
                //+
                return AccessType.Default;
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
            String blogGuid = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            if (String.IsNullOrEmpty(blogGuid))
            {
                throw new ArgumentNullException("BlogGuid is required.");
            }
            //+
            List<BlogEntry> blogEntryList = new List<BlogEntry>();
            if (this.AccessType != AccessType.Index)
            {
                blogEntryList = BlogAgent.GetNetBlogEntryList(blogGuid, this.Label, this.Archive, this.Link, MinimaConfiguration.RecentEntriesToShow);
                //+
                HttpContext context = HttpContext.Current;
                HttpRequest request = context.Request;
                BlogEntryActivity blogEntryActivity = new BlogEntryActivity();
                blogEntryActivity.BlogEntryActivityBrowser = request.UserAgent;
                blogEntryActivity.BlogEntryActivityTime = DateTime.Now;
                blogEntryActivity.BlogEntryActivityAddress = request.ServerVariables["REMOTE_HOST"];
                blogEntryActivity.BlogEntryActivitySessionId = (String)context.Session["SessionId"];
                blogEntryActivity.BlogEntryActivityTypeId = 0;
                //+
                if (this.AccessType == AccessType.Label)
                {
                    blogEntryActivity.BlogEntryActivityTypeId = 6;
                    if (blogEntryList == null)
                    {
                        blogEntryActivity.BlogEntryActivityExtra = "Invalid label accessed";
                    }
                    blogEntryActivity.BlogEntryActivityExtra = this.Label;
                    blogEntryActivity.BlogEntryActivityTypeId = 2;
                }
                if (this.AccessType == AccessType.Archive)
                {
                    String[] parts = this.Archive.Split("/".ToCharArray());
                    Int32 year = Int32.Parse(parts[0]);
                    Int32 month = Int32.Parse(parts[1]);
                    blogEntryActivity.BlogEntryActivityTypeId = 6;
                    if (blogEntryList == null)
                    {
                        blogEntryActivity.BlogEntryActivityExtra = "Invalid year/month accessed";
                    }
                    blogEntryActivity.BlogEntryActivityExtra = this.Archive;
                    blogEntryActivity.BlogEntryActivityTypeId = 4;
                }
                if (this.AccessType == AccessType.Link)
                {
                    if (blogEntryList != null)
                    {
                        HttpData.SetScopedItem<String>("Minima", "BlogEntryTitle", blogEntryList[0].Title);
                        //+
                        this.BlogEntryGuid = blogEntryList[0].Guid;
                        //+
                        blogEntryActivity.BlogEntryActivityTypeId = 3;
                        blogEntryActivity.BlogEntryActivityExtra = this.Link;
                    }
                    else
                    {
                        blogEntryActivity.BlogEntryActivityExtra = "Invalid URL accessed";
                        blogEntryActivity.BlogEntryActivityTypeId = 6;
                    }
                }
                //+
                if (blogEntryActivity.BlogEntryActivityTypeId == 0)
                {
                    blogEntryActivity.BlogEntryActivityTypeId = 1;
                }
                //+
                if (MinimaConfiguration.EnableActivityLogging)
                {
                    BlogEntryActivityAgent.ReportActivity(blogEntryActivity);
                }
            }
            //+
            return blogEntryList;
        }
    }
}