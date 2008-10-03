#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
//+
using Minima.Service.Behavior;
using Minima.Service.Helper;
using Minima.Service.Validation;
//+
using DataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
using AuthorLINQ = Minima.Service.Data.Entity.Author;
using BlogEntryAuthorLINQ = Minima.Service.Data.Entity.BlogEntryAuthor;
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using BlogEntryTypeLINQ = Minima.Service.Data.Entity.BlogEntryType;
using BlogEntryUrlMappingLINQ = Minima.Service.Data.Entity.BlogEntryUrlMapping;
using BlogLINQ = Minima.Service.Data.Entity.Blog;
using LabelBlogEntryLINQ = Minima.Service.Data.Entity.LabelBlogEntry;
using LabelLINQ = Minima.Service.Data.Entity.Label;
//+
namespace Minima.Service
{
    public class BlogService : IBlogService
    {
        //+
        //- @PostBlogEntry -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Create)]
        public String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, String blogEntryTypeGuid, List<Label> labelList, Boolean publish)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ validate
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                BlogEntryTypeLINQ blogEntryTypeLinq = null;
                if (!String.IsNullOrEmpty(blogEntryTypeGuid))
                {
                    Validator.EnsureBlogEntryTypeExists(blogEntryTypeGuid, out blogEntryTypeLinq, db);
                }
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
                    blogEntryLinq.BlogEntryTypeId = 1;
                    blogEntryLinq.BlogEntryText = content;
                    if (!String.IsNullOrEmpty(blogEntryTypeGuid))
                    {
                        blogEntryLinq.BlogEntryTypeId = blogEntryTypeLinq.BlogEntryTypeId;
                    }
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
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
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
        public void UpdateBlogEntry(String blogEntryGuid, String title, String content, String blogEntryTypeGuid, List<Label> labelList, DateTime dateTime, Boolean publish)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog entry exists
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                if (!String.IsNullOrEmpty(blogEntryTypeGuid))
                {
                    BlogEntryTypeLINQ blogEntryTypeLinq;
                    Validator.EnsureBlogEntryTypeExists(blogEntryTypeGuid, out blogEntryTypeLinq, db);
                }
                if (labelList == null)
                {
                    labelList = new List<Label>();
                }
                List<LabelLINQ> labelLinqList;
                Validator.EnsureEachLabelExists(labelList, out labelLinqList, db);
                using (TransactionScope scope = new TransactionScope())
                {
                    //+ has this title's mapping been used by a different blog entry?
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
                    if (!String.IsNullOrEmpty(blogEntryTypeGuid))
                    {
                        blogEntryLinq.BlogEntryTypeId = blogEntryLinq.BlogEntryTypeId;
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
        public BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean metaDataOnly)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog entry exists
                BlogEntryLINQ blogEntryLinq;
                Validator.EnsureBlogEntryExists(blogEntryGuid, out blogEntryLinq, db);
                //+
                return new BlogEntry
                {
                    Title = blogEntryLinq.BlogEntryTitle,
                    Content = metaDataOnly ? String.Empty : blogEntryLinq.BlogEntryText,
                    Guid = blogEntryLinq.BlogEntryGuid,
                    Status = blogEntryLinq.BlogEntryStatusId,
                    BlogEntryTypeGuid = blogEntryLinq.BlogEntryType.BlogEntryTypeGuid,
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
                    CommentList = metaDataOnly ? new List<Comment>() : new List<Comment>(
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
                    )
                };
            }
        }

        //- @GetNetBlogEntryList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, Int32 maxBlogEntryCount)
        {
            IQueryable<BlogEntryLINQ> blogEntryLinqList = null;
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
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
                    BlogEntryTypeGuid = be.BlogEntryType.BlogEntryTypeGuid,
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
                    maxBlogEntryCount = 0;
                    blogEntryLinqList = (from be in db.BlogEntries
                                         join lbe in db.LabelBlogEntries on be.BlogEntryId equals lbe.BlogEntryId
                                         join l in db.Labels on lbe.LabelId equals l.LabelId
                                         where be.BlogId == blogLinq.BlogId && l.LabelNetTitle.ToLower() == label.ToLower() && (be.BlogEntryStatusId == 1 || be.BlogEntryStatusId == 4)
                                         select be);
                }
                //+ archive?
                if ((blogEntryLinqList == null || blogEntryLinqList.Count() == 0) && !String.IsNullOrEmpty(archive))
                {
                    maxBlogEntryCount = 0;
                    String[] parts = archive.Split("/".ToCharArray());
                    Int32 year = Int32.Parse(parts[0]);
                    Int32 month = Int32.Parse(parts[1]);
                    blogEntryLinqList = db.BlogEntries.Where(p => p.BlogId == blogLinq.BlogId && p.BlogEntryPostDateTime.Month == month && p.BlogEntryPostDateTime.Year == year && p.BlogEntryStatusId == 1);
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
                if (blogEntryLinqList == null || blogEntryLinqList.Count() == 0)
                {
                    blogEntryLinqList = db.BlogEntries.Where(p => p.BlogId == blogLinq.BlogId && p.BlogEntryStatusId == 1);
                }
                //+
                return blogEntryLinqList
                    .Select(blogEntryTransformation)
                    .OrderByDescending(be => be.PostDateTime)
                    .Take(maxBlogEntryCount == 0 ? Int32.MaxValue : maxBlogEntryCount)
                    .ToList();
            }
        }

        //- @GetBlogEntryListByDateRange -//
        public List<BlogEntry> GetBlogEntryListByDateRange(String blogGuid, DateTime startDateTime, DateTime endDateTime, Boolean metaDataOnly)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                Func<BlogEntryLINQ, BlogEntry> blogEntryTransformation = be => new BlogEntry
                {
                    Title = be.BlogEntryTitle,
                    Content = metaDataOnly ? String.Empty : be.BlogEntryText,
                    Guid = be.BlogEntryGuid,
                    Status = be.BlogEntryStatusId,
                    BlogEntryTypeGuid = be.BlogEntryType.BlogEntryTypeGuid,
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
                    CommentList = metaDataOnly ? new List<Comment>() : new List<Comment>(
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
                //+
                Func<BlogEntryLINQ, Boolean> blogEntryListInRange = be => be.BlogId == blogLinq.BlogId && (be.BlogEntryStatusId == 1 || be.BlogEntryStatusId == 4) && (be.BlogEntryPostDateTime >= startDateTime && be.BlogEntryPostDateTime <= endDateTime);
                //+
                return db.BlogEntries.Where(blogEntryListInRange)
                    .Select(blogEntryTransformation)
                    .OrderByDescending(be => be.PostDateTime)
                    .ToList();
            }
        }

        //- @GetBlogEntryList -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 maxEntryCount, Boolean activeOnly, BlogEntryRetreivalType blogEntryRetreivalType)
        {
            List<BlogEntry> blogEntryList = null;
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+ create filter for blog entry list
                Func<BlogEntryLINQ, Boolean> blogEntryInBlog;
                if (activeOnly)
                {
                    blogEntryInBlog = x => x.BlogId == blogLinq.BlogId && (x.BlogEntryStatusId == 1 || x.BlogEntryStatusId == 4);
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
                Int32 blogEntryRetreivalTypeInt32 = (Int32)blogEntryRetreivalType;
                blogEntryList = db.BlogEntries.Where(blogEntryInBlog)
                    .Select(be => new BlogEntry
                    {
                        Title = be.BlogEntryTitle,
                        Content = (blogEntryRetreivalType == BlogEntryRetreivalType.Full) ? String.Empty : be.BlogEntryText,
                        Guid = be.BlogEntryGuid,
                        Status = be.BlogEntryStatusId,
                        BlogEntryTypeGuid = be.BlogEntryType.BlogEntryTypeGuid,
                        AllowCommentStatus = (AllowCommentStatus)be.BlogEntryCommentAllowStatusId,
                        PostDateTime = be.BlogEntryPostDateTime,
                        ModifyDateTime = be.BlogEntryModifyDateTime,
                        MappingNameList = (blogEntryRetreivalTypeInt32 < 1) ? new List<String>() : new List<String>(
                            be.BlogEntryUrlMappings.Select(p => p.BlogEntryUrlMappingName)
                        ),
                        LabelList = (blogEntryRetreivalTypeInt32 < 1) ? new List<Label>() : new List<Label>(
                            be.LabelBlogEntries.Select(p => new Label
                            {
                                Guid = p.Label.LabelGuid,
                                FriendlyTitle = p.Label.LabelFriendlyTitle,
                                Title = p.Label.LabelTitle
                            })
                        ),
                        AuthorList = (blogEntryRetreivalTypeInt32 < 1) ? new List<Author>() : new List<Author>(
                            be.BlogEntryAuthors.Select(p => new Author
                            {
                                Name = p.Author.AuthorName,
                                Email = p.Author.AuthorEmail
                            })
                        ),
                        CommentList = (blogEntryRetreivalType == BlogEntryRetreivalType.Full) ? new List<Comment>() : new List<Comment>(
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
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
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
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
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
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
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

        //- @GetBlogEntryTypeList -//
        public List<BlogEntryType> GetBlogEntryTypeList(String blogGuid, List<String> guidList)
        {
            using (DataContext db = new DataContext(ServiceConfiguration.ConnectionString))
            {
                List<BlogEntryType> blogEntryTypeList = new List<BlogEntryType>();
                var blogEntryData = db.BlogEntryTypes.Select(p => p);
                foreach (BlogEntryTypeLINQ blogEntryTypeLinq in blogEntryData)
                {
                    if (guidList.Contains(blogEntryTypeLinq.BlogEntryTypeGuid))
                    {
                        blogEntryTypeList.Add(new BlogEntryType
                        {
                            Extra = blogEntryTypeLinq.BlogEntryTypeExtra,
                            Name = blogEntryTypeLinq.BlogEntryTypeName,
                            Guid = blogEntryTypeLinq.BlogEntryTypeGuid
                        });
                    }
                }
                //+
                return blogEntryTypeList;
            }
        }

        //+
        //- $TransformBlogEntryList -//
        private IQueryable<BlogEntry> TransformBlogEntryList(IQueryable<BlogEntryLINQ> blogEntryLinqList, BlogLINQ blogLinq, Boolean metaDataOnly)
        {
            Func<BlogEntryLINQ, BlogEntry> blogEntryTransformation = be => new BlogEntry
            {
               Title = be.BlogEntryTitle,
               Content = metaDataOnly ? String.Empty : be.BlogEntryText,
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
        private BlogEntryLINQ GetBlogEntryByUrlMapping(Int32 blogId, String link, DataContext db)
        {
            return (from be in db.BlogEntries
                    join beum in db.BlogEntryUrlMappings on be.BlogEntryId equals beum.BlogEntryId
                    where beum.BlogEntryUrlMappingName == link.ToLower() && be.BlogId == blogId
                    select be).FirstOrDefault();
        }
    }
}