using System;
using System.Linq;
//+
using Minima.Service.Behavior;
using Minima.Service.Helper;
using Minima.Service.Validation;
//+
using AuthorLINQ = Minima.Service.Data.Entity.Author;
using BlogEntryAuthorLINQ = Minima.Service.Data.Entity.BlogEntryAuthor;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
//+
namespace Minima.Service
{
    public class AuthorService : IAuthorService
    {
        //- @ApplyAuthor -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void ApplyAuthor(String blogEntryGuid, String authorEmail)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
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
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                String authorGuid = String.Empty;
                if (!db.Authors.Any(p => p.AuthorEmail == authorEmail))
                {
                    AuthorLINQ authorLinq = new AuthorLINQ();
                    authorLinq.AuthorEmail = authorEmail;
                    authorLinq.AuthorName = authorName;
                    authorLinq.AuthorGuid = GuidCreator.NewDatabaseGuid;
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
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
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
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
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