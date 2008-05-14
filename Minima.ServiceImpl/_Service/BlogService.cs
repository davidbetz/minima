using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml;
//+
using Minima.Service.Behavior;
using Minima.Service.Helper;
using Minima.Service.Validation;
//+
using AuthorLINQ = Minima.Service.Data.Entity.Author;
using BlogEntryAuthorLINQ = Minima.Service.Data.Entity.BlogEntryAuthor;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using BlogEntryUrlMappingLINQ = Minima.Service.Data.Entity.BlogEntryUrlMapping;
using BlogLINQ = Minima.Service.Data.Entity.Blog;
using LabelBlogEntryLINQ = Minima.Service.Data.Entity.LabelBlogEntry;
using LabelLINQ = Minima.Service.Data.Entity.Label;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
//+
namespace Minima.Service
{
    public class BlogService : IBlogService
    {
        //+
        //- @PostBlogEntry -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Create)]
        public String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, List<Label> labelList, Boolean publish)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                if (authorList == null)
                {
                    authorList = new List<Author>();
                }
                List<AuthorLINQ> authorLinqList;
                Validator.EnsureEachAuthorExists(authorList, out authorLinqList, db);
                if (labelList == null)
                {
                    labelList = new List<Label>();
                }
                List<LabelLINQ> labelLinqList;
                Validator.EnsureEachLabelExists(labelList, out labelLinqList, db);
                Validator.IsNotZero(authorLinqList.Count, "At least one author is required.");
                //+
                using (TransactionScope scope = new TransactionScope())
                {
                    BlogEntryLINQ blogEntryLinq = new BlogEntryLINQ();
                    blogEntryLinq.BlogId = blogLinq.BlogId;
                    blogEntryLinq.BlogEntryTitle = title;
                    blogEntryLinq.BlogEntryText = content;
                    blogEntryLinq.BlogEntryStatus = db.BlogEntryStatus.SingleOrDefault(p => p.BlogEntryStatusId == (publish ? 1 : 3));
                    blogEntryLinq.BlogEntryCommentAllowStatus = db.BlogEntryCommentAllowStatus.SingleOrDefault(p => p.BlogEntryCommentAllowStatusId == 1);
                    blogEntryLinq.BlogEntryGuid = GuidCreator.NewDatabaseGuid;
                    if (dateTime.Year >= 1950)
                    {
                        blogEntryLinq.BlogEntryPostDateTime = dateTime;
                    }
                    else
                    {
                        blogEntryLinq.BlogEntryPostDateTime = DateTime.Now;
                    }
                    blogEntryLinq.BlogEntryModifyDateTime = DateTime.Now;
                    //+
                    db.BlogEntries.InsertOnSubmit(blogEntryLinq);
                    db.SubmitChanges();
                    //+
                    BlogEntryUrlMappingLINQ blogEntryUrlMappingLinq = new BlogEntryUrlMappingLINQ();
                    blogEntryUrlMappingLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                    blogEntryUrlMappingLinq.BlogEntryUrlMappingName = CreateBlogEntryPostUrlMapping(title);
                    db.BlogEntryUrlMappings.InsertOnSubmit(blogEntryUrlMappingLinq);
                    //+
                    db.SubmitChanges();
                    //+ label
                    foreach (LabelLINQ labelLinq in labelLinqList)
                    {
                        LabelBlogEntryLINQ labelBlogEntryLinq = new LabelBlogEntryLINQ();
                        labelBlogEntryLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                        labelBlogEntryLinq.LabelId = labelLinq.LabelId;
                        //+
                        db.LabelBlogEntries.InsertOnSubmit(labelBlogEntryLinq);
                    }
                    db.SubmitChanges();
                    //+ author
                    foreach (AuthorLINQ authorLinq in authorLinqList)
                    {
                        BlogEntryAuthorLINQ blogEntryAuthorLinq = new BlogEntryAuthorLINQ();
                        blogEntryAuthorLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                        blogEntryAuthorLinq.AuthorId = authorLinq.AuthorId;
                        //+
                        db.BlogEntryAuthors.InsertOnSubmit(blogEntryAuthorLinq);
                    }
                    db.SubmitChanges();
                    //+
                    scope.Complete();
                    //+
                    return blogEntryLinq.BlogEntryGuid;
                }
            }
        }

        //- @DisableBlogEntry -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Delete)]
        public void DisableBlogEntry(String blogEntryGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog entry exists
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                //+
                blogEntryLinq.BlogEntryStatusId = 2;
                //+
                db.SubmitChanges();
            }
        }

        //- @UpdateBlogEntry -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void UpdateBlogEntry(String blogEntryGuid, String title, String content, List<Label> labelList, DateTime dateTime, Boolean publish)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog entry exists
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                if (labelList == null)
                {
                    labelList = new List<Label>();
                }
                List<LabelLINQ> labelLinqList;
                Validator.EnsureEachLabelExists(labelList, out labelLinqList, db);
                using (TransactionScope scope = new TransactionScope())
                {
                    //+ has this title's mapping  been used by a different blog entry?
                    if (!String.IsNullOrEmpty(title))
                    {
                        String mapping = CreateBlogEntryPostUrlMapping(title);
                        BlogEntryUrlMappingLINQ blogEntryUrlMappingLINQ = db.BlogEntryUrlMappings.Where(p => p.BlogEntryUrlMappingName == mapping && p.BlogEntryId != blogEntryLinq.BlogEntryId).FirstOrDefault();
                        if (blogEntryUrlMappingLINQ != null)
                        {
                            throw new ArgumentException("This title's mapping has already been used, please change the title");
                        }
                        blogEntryUrlMappingLINQ = db.BlogEntryUrlMappings.Where(p => p.BlogEntryUrlMappingName == mapping && p.BlogEntryId == blogEntryLinq.BlogEntryId).FirstOrDefault();
                        //+ if this is a new title completely, create a new mapping to allow access by
                        //+ the old and the new links
                        if (blogEntryUrlMappingLINQ == null)
                        {
                            BlogEntryUrlMappingLINQ blogEntryUrlMappingLinq = new BlogEntryUrlMappingLINQ();
                            blogEntryUrlMappingLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                            blogEntryUrlMappingLinq.BlogEntryUrlMappingName = CreateBlogEntryPostUrlMapping(title);
                            db.BlogEntryUrlMappings.InsertOnSubmit(blogEntryUrlMappingLinq);
                            db.SubmitChanges();
                        }
                        //+
                        blogEntryLinq.BlogEntryTitle = title;
                    }
                    if (!String.IsNullOrEmpty(content))
                    {
                        blogEntryLinq.BlogEntryText = content;
                    }
                    if (dateTime.Year >= 1950)
                    {
                        blogEntryLinq.BlogEntryPostDateTime = dateTime;
                    }
                    blogEntryLinq.BlogEntryStatusId = publish ? 1 : 3;
                    //+ label
                    foreach (LabelLINQ labelLinq in labelLinqList)
                    {
                        if (!blogEntryLinq.LabelBlogEntries.Any(p => p.LabelId == labelLinq.LabelId))
                        {
                            LabelBlogEntryLINQ labelBlogEntryLinq = new LabelBlogEntryLINQ();
                            labelBlogEntryLinq.BlogEntryId = blogEntryLinq.BlogEntryId;
                            labelBlogEntryLinq.LabelId = labelLinq.LabelId;
                            //+
                            db.LabelBlogEntries.InsertOnSubmit(labelBlogEntryLinq);
                        }
                    }
                    foreach (LabelBlogEntryLINQ labelBlogEntryLINQ in blogEntryLinq.LabelBlogEntries)
                    {
                        if (!labelLinqList.Any(p => p.LabelId == labelBlogEntryLINQ.LabelId))
                        {
                            db.LabelBlogEntries.DeleteOnSubmit(labelBlogEntryLINQ);
                        }
                    }
                    //+
                    db.SubmitChanges();
                    scope.Complete();
                }
            }
        }

        //- @GetSingleBlogEntry -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public BlogEntry GetSingleBlogEntry(String blogEntryGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog entry exists
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                //+
                return new BlogEntry
                {
                    Title = blogEntryLinq.BlogEntryTitle,
                    Content = blogEntryLinq.BlogEntryText,
                    Guid = blogEntryLinq.BlogEntryGuid,
                    Status = blogEntryLinq.BlogEntryStatusId,
                    AllowCommentStatus = (AllowCommentStatus)blogEntryLinq.BlogEntryCommentAllowStatusId,
                    PostDateTime = blogEntryLinq.BlogEntryPostDateTime,
                    ModifyDateTime = blogEntryLinq.BlogEntryModifyDateTime,
                    MappingNameList = new List<String>(
                        blogEntryLinq.BlogEntryUrlMappings.Select(p => p.BlogEntryUrlMappingName)
                    ),
                    LabelList = new List<Label>(
                        blogEntryLinq.LabelBlogEntries.Select(p => new Label
                        {
                            Guid = p.Label.LabelGuid,
                            FriendlyTitle = p.Label.LabelFriendlyTitle,
                            Title = p.Label.LabelTitle
                        })
                    ),
                    AuthorList = new List<Author>(
                        blogEntryLinq.BlogEntryAuthors.Select(p => new Author
                        {
                            Name = p.Author.AuthorName,
                            Email = p.Author.AuthorEmail
                        })
                    ),
                    CommentList = new List<Comment>(
                        blogEntryLinq.Comments.Where(p => p.CommentModerated == false)
                        .Select(p => new Comment
                        {
                            Text = p.CommentText,
                            DateTime = p.CommentPostDate,
                            Website = p.CommentWebsite,
                            Guid = p.CommentGuid,
                            Email = p.CommentEmail,
                            Name = p.CommentAuthor
                        })
                    ),
                };
            }
        }

        //- @GetNetBlogEntryList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, Int32 maxBlogEntryCount)
        {
            List<BlogEntry> blogEntryList = new List<BlogEntry>();
            IQueryable<BlogEntryLINQ> blogEntryLinqList = null;
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                Func<BlogEntryLINQ, BlogEntry> blogEntryTransformation = be => new BlogEntry
                {
                    Title = be.BlogEntryTitle,
                    Content = be.BlogEntryText,
                    Guid = be.BlogEntryGuid,
                    Status = be.BlogEntryStatusId,
                    AllowCommentStatus = (AllowCommentStatus)be.BlogEntryCommentAllowStatusId,
                    PostDateTime = be.BlogEntryPostDateTime,
                    ModifyDateTime = be.BlogEntryModifyDateTime,
                    MappingNameList = new List<String>(
                        be.BlogEntryUrlMappings.Select(p => p.BlogEntryUrlMappingName)
                    ),
                    LabelList = new List<Label>(
                        be.LabelBlogEntries.Select(p => new Label
                        {
                            Guid = p.Label.LabelGuid,
                            FriendlyTitle = p.Label.LabelFriendlyTitle,
                            Title = p.Label.LabelTitle
                        })
                    ),
                    AuthorList = new List<Author>(
                        be.BlogEntryAuthors.Select(p => new Author
                        {
                            Name = p.Author.AuthorName,
                            Email = p.Author.AuthorEmail
                        })
                    ),
                    CommentList = new List<Comment>(
                        be.Comments.Where(p => p.CommentModerated == false)
                        .Select(p => new Comment
                        {
                            Text = p.CommentText,
                            DateTime = p.CommentPostDate,
                            Website = p.CommentWebsite,
                            Guid = p.CommentGuid,
                            Email = p.CommentEmail,
                            Name = p.CommentAuthor
                        })
                    )
                };
                //+ label?
                if (!String.IsNullOrEmpty(label))
                {

                    blogEntryLinqList = (from be in db.BlogEntries
                                         join lbe in db.LabelBlogEntries on be.BlogEntryId equals lbe.BlogEntryId
                                         join l in db.Labels on lbe.LabelId equals l.LabelId
                                         where be.BlogId == blogLinq.BlogId && l.LabelNetTitle.ToLower() == label.ToLower() && be.BlogEntryStatusId == 1
                                         select be);
                }
                //+ archive?
                if ((blogEntryLinqList == null || blogEntryLinqList.Count() == 0) && !String.IsNullOrEmpty(archive))
                {
                    String[] parts = archive.Split("/".ToCharArray());
                    Int32 year = Int32.Parse(parts[0]);
                    Int32 month = Int32.Parse(parts[1]);
                    blogEntryLinqList = db.BlogEntries.Where(p => p.BlogEntryPostDateTime.Month == month && p.BlogEntryPostDateTime.Year == year && p.BlogEntryStatusId == 1);
                }
                //+ link?
                if ((blogEntryLinqList == null || blogEntryLinqList.Count() == 0) && !String.IsNullOrEmpty(link))
                {
                    BlogEntryLINQ blogEntryLinq = this.GetBlogEntryByUrlMapping(blogLinq.BlogId, link, db);
                    if (blogEntryLinq != null)
                    {
                        return new List<BlogEntry>(new BlogEntry[] { blogEntryTransformation.Invoke(blogEntryLinq) });
                    }
                }
                //+ other?
                if (blogEntryLinqList == null || blogEntryLinqList.Count() == 0 || blogEntryList.Count < 1)
                {
                    if (String.IsNullOrEmpty(link) && String.IsNullOrEmpty(archive) && String.IsNullOrEmpty(label))
                    {
                        blogEntryLinqList = db.BlogEntries.Where(p => p.BlogId == blogLinq.BlogId && p.BlogEntryStatusId == 1);
                    }
                }
                //+
                return blogEntryLinqList
                    .Select(blogEntryTransformation)
                    .OrderByDescending(be => be.PostDateTime)
                    .Take(maxBlogEntryCount == 0 ? Int32.MaxValue : maxBlogEntryCount)
                    .ToList();
            }
        }

        //- @GetBlogEntryList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 maxEntryCount, Boolean activeOnly, Boolean includeContent)
        {
            List<BlogEntry> blogEntryList = null;
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+ create filter for blog entry list
                Func<BlogEntryLINQ, Boolean> blogEntryInBlog;
                if (activeOnly)
                {
                    blogEntryInBlog = x => x.BlogId == blogLinq.BlogId && x.BlogEntryStatusId == 1;
                }
                else
                {
                    blogEntryInBlog = x => x.BlogId == blogLinq.BlogId;
                }
                //+ get blog entry list
                if (maxEntryCount == 0)
                {
                    maxEntryCount = Int32.MaxValue;
                }
                blogEntryList = db.BlogEntries.Where(blogEntryInBlog)
                    .Select(be => new BlogEntry
                    {
                        Title = be.BlogEntryTitle,
                        Content = be.BlogEntryText,
                        Guid = be.BlogEntryGuid,
                        Status = be.BlogEntryStatusId,
                        AllowCommentStatus = (AllowCommentStatus)be.BlogEntryCommentAllowStatusId,
                        PostDateTime = be.BlogEntryPostDateTime,
                        ModifyDateTime = be.BlogEntryModifyDateTime,
                        MappingNameList = new List<String>(
                            be.BlogEntryUrlMappings.Select(p => p.BlogEntryUrlMappingName)
                        ),
                        LabelList = new List<Label>(
                            be.LabelBlogEntries.Select(p => new Label
                            {
                                Guid = p.Label.LabelGuid,
                                FriendlyTitle = p.Label.LabelFriendlyTitle,
                                Title = p.Label.LabelTitle
                            })
                        ),
                        AuthorList = new List<Author>(
                            be.BlogEntryAuthors.Select(p => new Author
                            {
                                Name = p.Author.AuthorName,
                                Email = p.Author.AuthorEmail
                            })
                        ),
                        CommentList = new List<Comment>(
                            be.Comments.Where(p => p.CommentModerated == false)
                            .Select(p => new Comment
                            {
                                Text = p.CommentText,
                                DateTime = p.CommentPostDate,
                                Website = p.CommentWebsite,
                                Guid = p.CommentGuid,
                                Email = p.CommentEmail,
                                Name = p.CommentAuthor
                            })
                        ),
                    })
                    .OrderByDescending(be => be.PostDateTime)
                    .Take(maxEntryCount)
                    .ToList();
            }
            //+
            return blogEntryList;
        }

        //- @GetBlogListForAssociatedAuthor -//
        [MinimaSystemSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<BlogMetaData> GetBlogListForAssociatedAuthor(String authorEmail)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                AuthorLINQ authorLinq;
                Validator.EnsureAuthorExists(authorEmail, out authorLinq, db);
                //+
                List<Int32> blogEntryIdList = db.AuthorBlogAssociations
                                                .Where(p => p.AuthorId == authorLinq.AuthorId)
                                                .Select(p => p.BlogId)
                                                .ToList();
                List<BlogMetaData> blogMetaDataList = new List<BlogMetaData>();
                foreach (Int32 blogId in blogEntryIdList)
                {
                    BlogMetaData blogMetaData = new BlogMetaData();
                    BlogLINQ blogLinq = db.Blogs.Single(p => p.BlogId == blogId);
                    blogMetaData.Description = blogLinq.BlogDescription;
                    blogMetaData.FeedTitle = blogLinq.BlogFeedTitle;
                    blogMetaData.FeedUri = new Uri(blogLinq.BlogFeedUrl);
                    blogMetaData.Guid = blogLinq.BlogGuid;
                    blogMetaData.Title = blogLinq.BlogTitle;
                    blogMetaData.Uri = new Uri(blogLinq.BlogPrimaryUrl);
                    blogMetaData.CreateDateTime = blogLinq.BlogCreateDate;
                    blogMetaData.LabelList = new List<Label>(
                        blogLinq.Labels.Select(p => new Label
                        {
                            Guid = p.LabelGuid,
                            FriendlyTitle = p.LabelFriendlyTitle,
                            Title = p.LabelTitle
                        })
                    );
                    blogMetaDataList.Add(blogMetaData);
                }
                //+
                return blogMetaDataList;
            }
        }

        //- @GetArchivedEntryList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<ArchiveCount> GetArchivedEntryList(String blogGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                return db.GetArchivedEntryList(blogLinq.BlogId).Select(p => new ArchiveCount
                {
                    ArchiveDate = DateTime.Parse(p.Month),
                    Count = (Int32)p.Count
                }).ToList();
            }
        }

        //- @GetBlogMetaData -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public BlogMetaData GetBlogMetaData(String blogGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                return new BlogMetaData
                {
                    Description = blogLinq.BlogDescription,
                    FeedTitle = blogLinq.BlogFeedTitle,
                    FeedUri = new Uri(blogLinq.BlogFeedUrl),
                    Guid = blogLinq.BlogGuid,
                    Title = blogLinq.BlogTitle,
                    Uri = new Uri(blogLinq.BlogPrimaryUrl),
                    CreateDateTime = blogLinq.BlogCreateDate,
                    LabelList = new List<Label>(
                        blogLinq.Labels.Select(p => new Label
                        {
                            Guid = p.LabelGuid,
                            FriendlyTitle = p.LabelFriendlyTitle,
                            Title = p.LabelTitle
                        })
                    )
                };
            }
        }

        //+
        //- $TransformBlogEntryList -//
        private IQueryable<BlogEntry> TransformBlogEntryList(IQueryable<BlogEntryLINQ> blogEntryLinqList, BlogLINQ blogLinq, Boolean includeContent)
        {
            Func<BlogEntryLINQ, BlogEntry> blogEntryTransformation = be => new BlogEntry
           {
               Title = be.BlogEntryTitle,
               Content = includeContent ? be.BlogEntryText : String.Empty,
               Guid = be.BlogEntryGuid,
               Status = be.BlogEntryStatusId,
               PostDateTime = be.BlogEntryPostDateTime,
               ModifyDateTime = be.BlogEntryModifyDateTime,
               MappingNameList = new List<String>(
                   be.BlogEntryUrlMappings.Select(p => p.BlogEntryUrlMappingName)
               ),
               LabelList = new List<Label>(
                   be.LabelBlogEntries.Select(p => new Label
                   {
                       Guid = p.Label.LabelGuid,
                       FriendlyTitle = p.Label.LabelFriendlyTitle,
                       Title = p.Label.LabelTitle
                   })
               )
           };
            //+
            return (IQueryable<BlogEntry>)blogEntryLinqList.Select(blogEntryTransformation);
        }

        //- $CreateBlogEntryPostUrlMapping -//
        private String CreateBlogEntryPostUrlMapping(String title)
        {
            return title.Replace(" - ", "-").Replace("/", "").Replace("{", "").Replace("}", "").Replace(".", "").Replace("'", "").Replace("\"", "").Replace("(", "").Replace(")", "").Replace("#", "").Replace("!", "").Replace(":", "").Replace(" ", "-").Replace("?", "").Replace(",", "");
        }

        //- $GetBlogGuidByBlogEntryGuid -//
        private String GetBlogGuidByBlogEntryGuid(String blogEntryGuid)
        {
            return BlogGuidFinder.ByBlogEntryGuid(blogEntryGuid);
        }

        //- $GetBlogEntryByUrlMapping -//
        private BlogEntryLINQ GetBlogEntryByUrlMapping(Int32 blogId, String link, MinimaServiceLINQDataContext db)
        {
            return (from be in db.BlogEntries
                    join beum in db.BlogEntryUrlMappings on be.BlogEntryId equals beum.BlogEntryId
                    where beum.BlogEntryUrlMappingName == link.ToLower() && be.BlogId == blogId
                    select be).FirstOrDefault();
        }
    }
}