using System;
using System.Data.Linq;
//+
using Minima.Service.Validation;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
using AuthorLINQ = Minima.Service.Data.Entity.Author;
using AuthorBlogAssociationLINQ = Minima.Service.Data.Entity.AuthorBlogAssociation;
using BlogLINQ = Minima.Service.Data.Entity.Blog;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using BlogEntryAuthorLINQ = Minima.Service.Data.Entity.BlogEntryAuthor;
using BlogEntryStatusLINQ = Minima.Service.Data.Entity.BlogEntryStatus;
using BlogEntryUrlMappingLINQ = Minima.Service.Data.Entity.BlogEntryUrlMapping;
using CommentLINQ = Minima.Service.Data.Entity.Comment;
using LabelLINQ = Minima.Service.Data.Entity.Label;
using LabelBlogEntryLINQ = Minima.Service.Data.Entity.LabelBlogEntry;
using UserRightLINQ = Minima.Service.Data.Entity.UserRight;
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
    }
}