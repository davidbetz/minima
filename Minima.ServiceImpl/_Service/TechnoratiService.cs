using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
//+
using Minima.Service.Behavior;
using Minima.Service.Data;
using Minima.Service.Helper;
using Minima.Service.Validation;
using Minima.Service.Technorati;
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
    public class TechnoratiService : ITechnoratiService
    {
        //- @PingTechnorati -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void PingTechnorati(String blogGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog entry exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                TechnoratiNotifier.Ping(blogLinq.BlogTitle, new Uri(blogLinq.BlogPrimaryUrl));
            }
        }
    }
}