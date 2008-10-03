﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
//+
using Minima.Service.Behavior;
using Minima.Service.Helper;
using Minima.Service.Validation;
//+
using DataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using BlogLINQ = Minima.Service.Data.Entity.Blog;
using LabelBlogEntryLINQ = Minima.Service.Data.Entity.LabelBlogEntry;
using LabelLINQ = Minima.Service.Data.Entity.Label;
//+
namespace Minima.Service
{
    public class LabelService : ILabelService
    {
        //- @ApplyLabel -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void ApplyLabel(String blogEntryGuid, String labelGuid)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                LabelLINQ labelLinq;
                Validator.EnsureLabelExists(labelGuid, out labelLinq, db);
                //+
                if (!db.LabelBlogEntries.Any(p => p.LabelId == labelLinq.LabelId && p.BlogEntryId == blogEntryLinq.BlogEntryId))
                {
                    LabelBlogEntryLINQ labelBlogEntryLinq = new LabelBlogEntryLINQ();
                    labelBlogEntryLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                    labelBlogEntryLinq.LabelId = labelLinq.LabelId;
                    //+
                    db.LabelBlogEntries.InsertOnSubmit(labelBlogEntryLinq);
                    db.SubmitChanges();
                }
            }
        }

        //- @CreateLabel -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Create)]
        public String CreateLabel(String blogGuid, String title)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                LabelLINQ labelLinq;
                String labelGuid;
                labelLinq = db.Labels.SingleOrDefault(p => p.BlogId == blogLinq.BlogId && p.LabelTitle == title);
                if (labelLinq != null)
                {
                    labelGuid = labelLinq.LabelGuid;
                }
                else
                {
                    labelLinq = new LabelLINQ();
                    labelLinq.LabelTitle = title;
                    labelLinq.BlogId = blogLinq.BlogId;
                    labelLinq.LabelGuid = GuidCreator.NewDatabaseGuid;
                    db.Labels.InsertOnSubmit(labelLinq);
                    //+
                    db.SubmitChanges();
                    labelGuid = labelLinq.LabelGuid;
                }
                //+
                return labelGuid;
            }
        }

        //- @RemoveLabel -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void RemoveLabel(String labelGuid, String blogEntryGuid)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                LabelLINQ labelLinq;
                Validator.EnsureLabelExists(labelGuid, out labelLinq, db);
                //+
                LabelBlogEntryLINQ labelBlogEntryLinq = db.LabelBlogEntries.SingleOrDefault(p => p.LabelId == labelLinq.LabelId && p.BlogEntryId == blogEntryLinq.BlogEntryId);
                if (labelBlogEntryLinq != null)
                {
                    db.LabelBlogEntries.DeleteOnSubmit(labelBlogEntryLinq);
                    db.SubmitChanges();
                }
            }
        }

        //- @UpdateLabel -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void UpdateLabel(String labelGuid, String title)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure label exists
                LabelLINQ labelLinq;
                Validator.EnsureLabelExists(labelGuid, out labelLinq, db);
                //+
                labelLinq.LabelTitle = title;
                //+
                db.SubmitChanges();
            }
        }

        //- @GetBlogEntryLabelList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<Label> GetBlogEntryLabelList(String blogEntryGuid)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                //+
                return blogEntryLinq.LabelBlogEntries.Select(p => new Label
                {
                    BlogEntryCount = -1,
                    Title = p.Label.LabelTitle,
                    Guid = p.Label.LabelGuid,
                    BlogGuid = p.BlogEntry.Blog.BlogGuid,
                    FriendlyTitle = p.Label.LabelFriendlyTitle
                }).ToList();
            }
        }

        //- @GetBlogLabelList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<Label> GetBlogLabelList(String blogGuid)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                return blogLinq.Labels
                    .Where(p => p.BlogId == blogLinq.BlogId)
                    .Select(p => new Label
                    {
                        BlogGuid = blogLinq.BlogGuid,
                        Guid = p.LabelGuid,
                        FriendlyTitle = p.LabelFriendlyTitle,
                        Title = p.LabelTitle,
                        BlogEntryCount = GetEntryCount(p, db)
                    }).ToList();
            }
        }

        //- @GetLabelByTitle-//
        public Label GetLabelByTitle(String title)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<LabelLINQ>(p => p.Blog);
                LabelLINQ labelLinq = db.Labels.SingleOrDefault(p => p.LabelTitle == title);
                if (labelLinq == null)
                {
                    return null;
                }
                //+
                return new Label
                {
                    BlogGuid = labelLinq.Blog.BlogGuid,
                    FriendlyTitle = labelLinq.LabelFriendlyTitle,
                    Guid = labelLinq.LabelGuid,
                    Title = labelLinq.LabelTitle,
                    BlogEntryCount = GetEntryCount(labelLinq, db)
                };
            }
        }

        //- @GetLabelByNetTitle-//
        public Label GetLabelByNetTitle(String netTitle)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<LabelLINQ>(p => p.Blog);
                LabelLINQ labelLinq = db.Labels.SingleOrDefault(p => p.LabelNetTitle == netTitle);
                if (labelLinq == null)
                {
                    return null;
                }
                //+
                return new Label
                {
                    BlogGuid = labelLinq.Blog.BlogGuid,
                    FriendlyTitle = labelLinq.LabelFriendlyTitle,
                    Guid = labelLinq.LabelGuid,
                    Title = labelLinq.LabelTitle,
                    BlogEntryCount = GetEntryCount(labelLinq, db)
                };
            }
        }

        //+
        //- $GetEntryCount -//
        private Int32 GetEntryCount(LabelLINQ labelLinq, DataContext db)
        {
            return db.LabelBlogEntries.Where(p => p.LabelId == labelLinq.LabelId).Count();
        }
    }
}