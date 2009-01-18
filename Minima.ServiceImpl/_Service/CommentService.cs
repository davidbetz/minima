#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Service.Behavior;
using Minima.Service.Helper;
using Minima.Service.Validation;
//+
using Themelia.Mail;
//+
using DataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using CommentLINQ = Minima.Service.Data.Entity.Comment;
//+
namespace Minima.Service
{
    public class CommentService : ICommentService
    {
        //- ~Message -//
        internal class Message
        {
            public const String InvalidEmail = "Invalid author e-mail.";
            public const String InvalidBlogGuid = "Invalid blog guid.";
        }

        //- @AuthorizeComment -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void AuthorizeComment(String commentGuid)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                CommentLINQ commentLinq;
                Validator.EnsureCommentExists(commentGuid, out commentLinq, db);
                //+
                commentLinq.CommentModerated = false;
                //+
                db.SubmitChanges();
            }
        }

        //- @DeleteComment -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Delete)]
        public void DeleteComment(String commentGuid)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                CommentLINQ commentLinq;
                Validator.EnsureCommentExists(commentGuid, out commentLinq, db);
                //+
                db.Comments.DeleteOnSubmit(commentLinq);
                //+
                db.SubmitChanges();
            }
        }

        //- @GetCommentList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<Comment> GetCommentList(String blogEntryGuid, Boolean showEveryComment)
        {
            String commentGuid = String.Empty;
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                //+
                Func<CommentLINQ, Boolean> commentStatus;
                if (showEveryComment)
                {
                    commentStatus = p => true;
                }
                else
                {
                    commentStatus = p => p.CommentModerated == false;
                }
                return blogEntryLinq.Comments.Where(commentStatus)
                    .Select(p => new Comment
                    {
                        Text = p.CommentText,
                        DateTime = p.CommentPostDate,
                        Website = p.CommentWebsite,
                        Guid = p.CommentGuid,
                        Email = p.CommentEmail,
                        Name = p.CommentAuthor
                    }).ToList();
            }
        }

        //- @PostNewComment -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Create)]
        public String PostNewComment(String blogEntryGuid, String text, String author, String email, String website, DateTime dateTime, String emailBodyTemplate, String emailSubject)
        {
            String commentGuid = String.Empty;
            String blogEntryTitle = String.Empty;
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                //+
                CommentLINQ commentLinq = new CommentLINQ();
                commentLinq.CommentEmail = email;
                commentLinq.CommentAuthor = author;
                commentLinq.CommentText = text;
                if (website.ToLower().StartsWith("http://") || website.ToLower().StartsWith("https://"))
                {
                    commentLinq.CommentWebsite = website;
                }
                commentLinq.CommentPostDate = dateTime;
                commentLinq.CommentModerated = true;
                commentLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                commentLinq.CommentGuid = Themelia.GuidCreator.GetNewGuid();
                //+
                db.Comments.InsertOnSubmit(commentLinq);
                db.SubmitChanges();
                //+
                commentGuid = commentLinq.CommentGuid;
                //+
                blogEntryTitle = blogEntryLinq.BlogEntryTitle;
            }
            //+ email
            Themelia.Map map = new Themelia.Map();
            map.Add("BlogEntryTitle", blogEntryTitle);
            map.Add("CommentGuid", commentGuid);
            String body = new Themelia.Template(emailBodyTemplate).Interpolate(map);
            //+ this could be sent from the person, but those e-mails will more than likely be caught by a spam filter.
            NotificationFacade.Send(MailConfigurationFacade.GeneralFromEmailAddress, MailConfigurationFacade.GeneralToEmailAddress, emailSubject, body, true);
            //+
            return commentGuid;
        }
    }
}