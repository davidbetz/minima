using System;
using System.Data.Linq;
//+
using Minima.Service.Validation;
//+
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using BlogImageLINQ = Minima.Service.Data.Entity.BlogImage;
using CommentLINQ = Minima.Service.Data.Entity.Comment;
using LabelLINQ = Minima.Service.Data.Entity.Label;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
//+
namespace Minima.Service
{
    internal class BlogGuidFinder
    {
        //- ~ByBlogEntryGuid -//
        internal static String ByBlogEntryGuid(String blogEntryGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<BlogEntryLINQ>(p => p.Blog);
                db.LoadOptions = options;
                //+
                //+ ensure blog exists
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                //+
                return blogEntryLinq.Blog.BlogGuid;
            }
        }

        //- ~ByCommentGuid -//
        internal static String ByCommentGuid(String commentGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<CommentLINQ>(p => p.BlogEntry);
                options.LoadWith<BlogEntryLINQ>(p => p.Blog);
                db.LoadOptions = options;
                //+
                CommentLINQ commentLinq;
                Validator.EnsureCommentExists(commentGuid, out commentLinq, db);
                //+
                return commentLinq.BlogEntry.Blog.BlogGuid;
            }
        }

        //- ~ByLabelGuid -//
        internal static String ByLabelGuid(String labelGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<LabelLINQ>(p => p.Blog);
                db.LoadOptions = options;
                //+ ensure blog exists
                LabelLINQ labelLinq;
                Validator.EnsureLabelExists(labelGuid, out labelLinq, db);
                //+
                return labelLinq.Blog.BlogGuid;
            }
        }

        //- ~ByBlogImageGuid -//
        internal static String ByBlogImageGuid(String blogImageGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<BlogImageLINQ>(p => p.Blog);
                db.LoadOptions = options;
                //+ ensure blog exists
                BlogImageLINQ blogImageLinq;
                Validator.EnsureBlogImageExists(blogImageGuid, out blogImageLinq, db);
                //+
                return blogImageLinq.Blog.BlogGuid;
            }
        }
    }
}