#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Service.Agent;
//+
using Themelia.Web;
//+
namespace Minima.Web.Controls
{
    public abstract class MinimaBlogBase : System.Web.UI.Control
    {
        //- ~Info -//
        internal class Info : Minima.Web.Info
        {
            public const String Link = "Link";
            public const String Archive = "Archive";
            public const String Index = "Index";
            public const String Label = "Label";
        }

        //+
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
                return HttpData.GetScopedItem<String>(Info.Scope, Info.Label);
            }
        }

        //- @Archive -//
        public String Archive
        {
            get
            {
                return HttpData.GetScopedItem<String>(Info.Scope, Info.Archive);
            }
        }

        //- @Link -//
        public String Link
        {
            get
            {
                return HttpData.GetScopedItem<String>(Info.Scope, Info.Link);
            }
        }

        //- @Index -//
        public Int32 Index
        {
            get
            {
                return HttpData.GetScopedItem<Int32>(Info.Scope, Info.Index);
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
                blogEntryList = BlogAgent.GetNetBlogEntryList(blogGuid, this.Label, this.Archive, this.Link, BlogSection.GetConfigSection().EntriesToShow, false);
                //+
                if (this.AccessType == AccessType.Link)
                {
                    if (blogEntryList != null)
                    {
                        HttpData.SetScopedItem<String>(Info.Scope, "BlogEntryTitle", blogEntryList[0].Title);
                        //+
                        this.BlogEntryGuid = blogEntryList[0].Guid;
                    }
                }
            }
            //+
            return blogEntryList;
        }
    }
}