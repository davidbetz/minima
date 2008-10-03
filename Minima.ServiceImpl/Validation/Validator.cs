#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
//+
using AuthorLINQ = Minima.Service.Data.Entity.Author;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using BlogEntryTypeLINQ = Minima.Service.Data.Entity.BlogEntryType;
using BlogImageLINQ = Minima.Service.Data.Entity.BlogImage;
using BlogLINQ = Minima.Service.Data.Entity.Blog;
using CommentLINQ = Minima.Service.Data.Entity.Comment;
using LabelLINQ = Minima.Service.Data.Entity.Label;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
//+
namespace Minima.Service.Validation
{
    internal static class Validator
    {
        //- ~Message -//
        internal class Message
        {
            public const String InvalidEmail = "Invalid author email.";
            public const String InvalidBlogGuid = "Invalid blog guid.";
            public const String InvalidBlogEntryGuid = "Invalid blog entry guid.";
            public const String InvalidBlogEntryTypeGuid = "Invalid blog entry type guid.";
            public const String InvalidCommentGuid = "Invalid comment guid.";
            public const String InvalidImageGuid = "Invalid image guid.";
            public const String InvalidLabelGuid = "Invalid label guid.";
        }

        //- ~EnsureAuthorExists -//
        internal static void EnsureAuthorExists(String authorEmail, out AuthorLINQ authorLinq, MinimaServiceLINQDataContext db)
        {
            EnsureAuthorExists(authorEmail, out authorLinq, Message.InvalidEmail, db);
        }
        internal static void EnsureAuthorExists(String authorEmail, out AuthorLINQ authorLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<AuthorLINQ, Boolean> authorExists = x => x.AuthorEmail == authorEmail;
            authorLinq = db.Authors.SingleOrDefault(authorExists);
            if (authorLinq == null)
            {
                throw new ArgumentException(message);
            }
        }

        //- ~EnsureBlogExists -//
        internal static void EnsureBlogExists(String blogGuid, out BlogLINQ blogLinq, MinimaServiceLINQDataContext db)
        {
            EnsureBlogExists(blogGuid, out blogLinq, Message.InvalidBlogGuid, db);
        }
        internal static void EnsureBlogExists(String blogGuid, out BlogLINQ blogLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<BlogLINQ, Boolean> blogExists = x => x.BlogGuid == blogGuid;
            blogLinq = (from p in db.Blogs where p.BlogGuid == blogGuid select p).FirstOrDefault();
            if (blogLinq == null)
            {
                throw new ArgumentException(message);
            }
        }

        //- ~EnsureBlogEntryExists -//
        internal static void EnsureBlogEntryExists(String blogEntryGuid, out BlogEntryLINQ blogEntryLinq, MinimaServiceLINQDataContext db)
        {
            EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, Message.InvalidBlogEntryGuid, db);
        }
        internal static void EnsureBlogEntryExists(String blogEntryGuid, out BlogEntryLINQ blogEntryLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<BlogEntryLINQ, Boolean> blogEntryExists = x => x.BlogEntryGuid == blogEntryGuid;
            blogEntryLinq = (from p in db.BlogEntries where p.BlogEntryGuid == blogEntryGuid select p).FirstOrDefault();
            if (blogEntryLinq == null)
            {
                throw new ArgumentException(message);
            }
        }

        //- ~EnsureBlogImageExists -//
        internal static void EnsureBlogImageExists(String blogImageGuid, out BlogImageLINQ blogImageLinq, MinimaServiceLINQDataContext db)
        {
            EnsureBlogImageExists(blogImageGuid, out blogImageLinq, Message.InvalidImageGuid, db);
        }
        internal static void EnsureBlogImageExists(String blogImageGuid, out BlogImageLINQ blogImageLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<BlogImageLINQ, Boolean> blogImageExists = x => x.BlogImageGuid == blogImageGuid;
            blogImageLinq = db.BlogImages.SingleOrDefault(blogImageExists);
            if (blogImageLinq == null)
            {
                throw new ArgumentException(message);
            }
        }

        //- ~EnsureBlogEntryTypeExists -//
        internal static void EnsureBlogEntryTypeExists(String blogEntryTypeGuid, out BlogEntryTypeLINQ blogEntryTypeLinq, MinimaServiceLINQDataContext db)
        {
            EnsureBlogEntryTypeExists(blogEntryTypeGuid, out blogEntryTypeLinq, Message.InvalidBlogEntryTypeGuid, db);
        }
        internal static void EnsureBlogEntryTypeExists(String blogEntryTypeGuid, out BlogEntryTypeLINQ blogEntryTypeLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<BlogEntryTypeLINQ, Boolean> blogEntryTypeExists = x => x.BlogEntryTypeGuid == blogEntryTypeGuid;
            blogEntryTypeLinq = (from p in db.BlogEntryTypes where p.BlogEntryTypeGuid == blogEntryTypeGuid select p).FirstOrDefault();
            if (blogEntryTypeLinq == null)
            {
                throw new ArgumentException(message);
            }
        }

        //- ~EnsureBlogExistsViaBlogEntryOrBlogGuid -//
        internal static void EnsureBlogExistsViaBlogEntryOrBlogGuid(String blogGuid, out BlogLINQ blogLinq, MinimaServiceLINQDataContext db)
        {
            EnsureBlogExists(blogGuid, out blogLinq, Message.InvalidBlogGuid, db);
        }
        internal static void EnsureBlogExistsViaBlogEntryOrBlogGuid(String blogGuid, out BlogLINQ blogLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<BlogLINQ, Boolean> blogExists = x => x.BlogGuid == blogGuid;
            blogLinq = db.Blogs.SingleOrDefault(blogExists);
            if (blogLinq == null)
            {
                //+ perhaps this is actually a blog entry guid
                Func<BlogEntryLINQ, Boolean> blogEntryExists = x => x.BlogEntryGuid == blogGuid;
                BlogEntryLINQ blogEntryLinq = db.BlogEntries.SingleOrDefault(blogEntryExists);
                if (blogEntryLinq != null)
                {
                    blogLinq = blogEntryLinq.Blog;
                }
                else
                {
                    throw new ArgumentException(message);
                }
            }
        }

        //- ~EnsureCommentExists -//
        internal static void EnsureCommentExists(String commentGuid, out CommentLINQ commentLinq, MinimaServiceLINQDataContext db)
        {
            EnsureCommentExists(commentGuid, out commentLinq, Message.InvalidCommentGuid, db);
        }
        internal static void EnsureCommentExists(String commentGuid, out CommentLINQ commentLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<CommentLINQ, Boolean> commentExists = x => x.CommentGuid == commentGuid;
            commentLinq = db.Comments.SingleOrDefault(commentExists);
            if (commentLinq == null)
            {
                throw new ArgumentException(message);
            }
        }

        //- ~EnsureLabelExists -//
        internal static void EnsureLabelExists(String labelGuid, out LabelLINQ labelLinq, MinimaServiceLINQDataContext db)
        {
            EnsureLabelExists(labelGuid, out labelLinq, Message.InvalidLabelGuid, db);
        }
        internal static void EnsureLabelExists(String labelGuid, out LabelLINQ labelLinq, String message, MinimaServiceLINQDataContext db)
        {
            Func<LabelLINQ, Boolean> labelExists = x => x.LabelGuid == labelGuid;
            labelLinq = db.Labels.SingleOrDefault(labelExists);
            if (labelLinq == null)
            {
                throw new ArgumentException(message);
            }
        }

        //- ~EnsureEachAuthorExists -//
        internal static void EnsureEachAuthorExists(List<Author> authorList, out List<AuthorLINQ> authorLinqList, MinimaServiceLINQDataContext db)
        {
            authorLinqList = new List<AuthorLINQ>();
            foreach (Author author in authorList)
            {
                AuthorLINQ authorLinq;
                EnsureAuthorExists(author.Email, out authorLinq, db);
                //+
                authorLinqList.Add(authorLinq);
            }
        }

        //- ~EnsureEachLabelExists -//
        internal static void EnsureEachLabelExists(List<Label> labelList, out List<LabelLINQ> labelLinqList, MinimaServiceLINQDataContext db)
        {
            labelLinqList = new List<LabelLINQ>();
            foreach (Label label in labelList)
            {
                LabelLINQ labelLinq;
                EnsureLabelExists(label.Guid, out labelLinq, db);
                //+
                labelLinqList.Add(labelLinq);
            }
        }

        //- ~EnsureIsNotNull -//
        internal static void EnsureIsNotNull(Object @object, String message)
        {
            if (@object == null)
            {
                {
                    throw new ArgumentNullException(message);
                }
            }
        }

        //- ~IsNotZero -//
        internal static void IsNotZero(Int32 number, String message)
        {
            if (number == 0)
            {
                throw new ArgumentNullException(message);
            }
        }

        //- ~IsNotBlank -//
        internal static void IsNotBlank(String data, String message)
        {
            if (String.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}