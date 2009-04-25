#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Linq;
//+
using Minima.Service.Behavior;
using Minima.Service.Validation;
using AuthorLINQ = Minima.Service.Data.Entity.Author;
using BlogEntryAuthorLINQ = Minima.Service.Data.Entity.BlogEntryAuthor;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
//+
using DataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
//+
namespace Minima.Service
{
    public class AuthorService : IAuthorService
    {
        //- @ApplyAuthor -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void ApplyAuthor(String blogEntryGuid, String authorEmail)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                AuthorLINQ authorLinq;
                Validator.EnsureAuthorExists(authorEmail, out authorLinq, db);
                //+
                if (!db.BlogEntryAuthors.Any(p => p.AuthorId == authorLinq.AuthorId && p.BlogEntryId == blogEntryLinq.BlogEntryId))
                {
                    BlogEntryAuthorLINQ blogEntryAuthorLinq = new BlogEntryAuthorLINQ();
                    blogEntryAuthorLinq.AuthorId = authorLinq.AuthorId;
                    blogEntryAuthorLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                    //+
                    db.BlogEntryAuthors.InsertOnSubmit(blogEntryAuthorLinq);
                    db.SubmitChanges();
                }
            }
        }

        //- @CreateAuthor -//
        [MinimaSystemSecurityBehavior]
        public String CreateAuthor(String authorEmail, String authorName)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                String authorGuid = String.Empty;
                if (!db.Authors.Any(p => p.AuthorEmail == authorEmail))
                {
                    AuthorLINQ authorLinq = new AuthorLINQ();
                    authorLinq.AuthorEmail = authorEmail;
                    authorLinq.AuthorName = authorName;
                    authorLinq.AuthorGuid = Themelia.GuidCreator.GetNewGuid();
                    authorLinq.AuthorCreateDate = DateTime.Now;
                    //+
                    db.Authors.InsertOnSubmit(authorLinq);
                    db.SubmitChanges();
                    //+
                    authorGuid = authorLinq.AuthorGuid;
                }
                //+
                return authorGuid;
            }
        }

        //- @RemoveAuthor -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void RemoveAuthor(String blogEntryGuid, String authorEmail)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                AuthorLINQ authorLinq;
                Validator.EnsureAuthorExists(authorEmail, out authorLinq, db);
                //+
                BlogEntryAuthorLINQ blogEntryAuthorLinq = db.BlogEntryAuthors.SingleOrDefault(p => p.AuthorId == authorLinq.AuthorId && p.BlogEntryId == blogEntryLinq.BlogEntryId);
                if (blogEntryAuthorLinq == null)
                {
                    db.BlogEntryAuthors.DeleteOnSubmit(blogEntryAuthorLinq);
                    db.SubmitChanges();
                }
            }
        }

        //- @UpdateAuthor -//
        [MinimaSystemSecurityBehavior]
        public void UpdateAuthor(String authorEmail, String authorName)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure author exists
                AuthorLINQ authorLinq;
                Validator.EnsureAuthorExists(authorEmail, out authorLinq, db);
                //+
                authorLinq.AuthorName = authorName;
                //+
                db.SubmitChanges();
            }
        }
    }
}