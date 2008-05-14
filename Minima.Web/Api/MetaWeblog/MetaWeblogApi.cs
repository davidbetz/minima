using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//+
using CookComputing.XmlRpc;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Web.Agent;
using Minima.Web.Helper;
using Minima.Web.Tracing;
//+
namespace Minima.Web.Api.MetaWeblog
{
    [XmlRpcService(Name = "Minima API", AutoDocumentation = true)]
    public class MetaWeblogApi : XmlRpcService
    {
        //- @DeletePost -//
        [XmlRpcMethod("blogger.deletePost")]
        public Boolean DeletePost(String appKey, String postid, String emailAddress, String password, Boolean publish)
        {
            String blogEntryGuid = postid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::DeletePost", new Object[] { appKey, blogEntryGuid, emailAddress, password, publish });
            //+
            BlogAgent.DisableBlogEntry(postid, emailAddress, password);
            //+
            return false;
        }

        //- @EditPost -//
        [XmlRpcMethod("metaWeblog.editPost")]
        public Boolean EditPost(String postid, String emailAddress, String password, Post post, Boolean publish)
        {
            String blogEntryGuid = postid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::EditPost", new Object[] { blogEntryGuid, emailAddress, password, post.description, post.title, publish });
            //+
            List<Label> labelList = new List<Label>();
            foreach (String title in post.categories)
            {
                labelList.Add(LabelAgent.GetLabelByTitle(title));
            }
            BlogAgent.UpdateBlogEntry(blogEntryGuid, post.title, post.description, labelList, new DateTime(0x79d, 1, 1), publish, emailAddress, password);
            //+
            return true;
        }

        //- @GetCategories -//
        [XmlRpcMethod("metaWeblog.getCategories")]
        public CategoryInfo[] GetCategories(String blogid, String emailAddress, String password)
        {
            String blogGuid = blogid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::GetCategories", new Object[] { blogGuid, emailAddress, password });
            //+
            List<Label> labelList = LabelAgent.GetBlogLabelList(blogGuid, emailAddress, password);
            //+
            return labelList.Select(p => new CategoryInfo
                {
                    categoryId = p.Guid,
                    htmlUrl = String.Empty,
                    rssUrl = String.Empty,
                    description = p.Title,
                    title = p.Title
                }).ToArray();
        }

        //- @GetPost -//
        [XmlRpcMethod("metaWeblog.getPost")]
        public FullPost GetPost(String postid, String emailAddress, String password)
        {
            String blogEntryGuid = postid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::GetPost", new Object[] { blogEntryGuid, emailAddress, password });
            //+
            BlogEntry blogEntry = BlogAgent.GetSingleBlogEntry(blogEntryGuid, emailAddress, password);
            //+
            return new FullPost
            {
                dateCreated = blogEntry.PostDateTime,
                description = blogEntry.Content,
                title = blogEntry.Title,
                categories = blogEntry.LabelList.Select(p => p.Title).ToArray(),
                link = BlogEntryHelper.BuildBlogEntry(blogEntry.PostDateTime, blogEntry.MappingNameList.First(), General.Web.HttpWebSection.CurrentWebSection),
                permalink = BlogEntryHelper.BuildBlogEntry(blogEntry.PostDateTime, blogEntry.MappingNameList.First(), General.Web.HttpWebSection.CurrentWebSection),
                postid = blogEntry.Guid,
                mt_allow_comments = blogEntry.AllowCommentStatus == AllowCommentStatus.Enabled ? "1" : "0",
                mt_convert_breaks = String.Empty,
                mt_allow_pings = 0,
                mt_excerpt = String.Empty,
                mt_tb_ping_urls = new String[] { },
                mt_text_mode = String.Empty
            };
        }

        //- @GetRecentPosts -//
        [XmlRpcMethod("metaWeblog.getRecentPosts")]
        public FullPost[] GetRecentPosts(String blogid, String emailAddress, String password, Int32 maxEntryCount)
        {
            String blogGuid = blogid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::GetRecentPosts", new Object[] { blogGuid, emailAddress, password, maxEntryCount });
            //+
            List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(blogGuid, maxEntryCount, true, emailAddress, password);
            //+
            return blogEntryList.Select(p => new FullPost
                {
                    dateCreated = p.PostDateTime,
                    description = p.Content,
                    title = p.Title,
                    link = BlogEntryHelper.BuildBlogEntry(p.PostDateTime, p.MappingNameList.First(), General.Web.HttpWebSection.CurrentWebSection),
                    permalink = BlogEntryHelper.BuildBlogEntry(p.PostDateTime, p.MappingNameList.First(), General.Web.HttpWebSection.CurrentWebSection),
                    postid = p.Guid,
                    mt_allow_comments = "1",
                    mt_convert_breaks = "0",
                    mt_text_mode = "",
                    mt_excerpt = "0",
                    mt_tb_ping_urls = new String[0],
                    categories = p.LabelList != null ? p.LabelList.Select(q => q.Title).ToArray() : null
                }).ToArray();
        }

        //- @GetRecentPosts -//
        [XmlRpcMethod("metaWeblog.getTemplate")]
        public String GetTemplate(String appKey, String blogid, String emailAddress, String password, String templateType)
        {
            String blogGuid = blogid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::GetTemplate", new Object[] { appKey, blogGuid, emailAddress, password, templateType });
            //+
            return String.Empty;
        }

        //- @GetUsersBlogs -//
        [XmlRpcMethod("blogger.getUsersBlogs")]
        public BlogInfo[] GetUsersBlogs(String key, String emailAddress, String password)
        {
            TraceManager.RecordMethodCall("XmlRpcApi::GetUsersBlogs", new Object[] { key, emailAddress, password });
            //+
            List<BlogMetaData> blogList = BlogAgent.GetBlogListForAssociatedAuthor(emailAddress, password);
            //+
            List<Minima.Web.Configuration.InstanceElement> instanceElementList = Minima.Web.Configuration.MinimaConfigurationFacade.GetWebConfiguration().Registration.ToList();
            //+
            var netBlogList = (from b in blogList
                               join e in instanceElementList on b.Guid equals e.BlogGuid
                               select new BlogInfo
                               {
                                   blogid = e.BlogGuid,
                                   blogName = b.Title,
                                   url = General.Web.HttpWebSection.GetUrl(e.WebSection).AbsoluteUri
                               }).ToArray();
            //+
            return netBlogList;
        }

        //- @MetaWeblogNewPost -//
        [XmlRpcMethod("metaWeblog.newPost")]
        public String MetaWeblogNewPost(String blogid, String emailAddress, String password, Post post, Boolean publish)
        {
            String blogGuid = blogid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::MetaWeblogNewPost", new Object[] { blogGuid, emailAddress, password, post.description, post.title, publish });
            //+
            return BlogAgent.PostBlogEntry(blogGuid, new List<Author>(
                new Author[] {
                    new Author {
                        Email = emailAddress
                    } 
                }
            ), post.title, post.description, DateTime.Now, null, publish, emailAddress, password);
        }

        //- @NewMediaObject -//
        [XmlRpcMethod("metaWeblog.newMediaObject")]
        public UrlInfo NewMediaObject(String blogid, String emailAddress, String password, MediaType enc)
        {
            String blogGuid = blogid;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::NewMediaObject", new Object[] { blogGuid, emailAddress, password, enc.name, enc.type, enc.bits.Length });
            //+
            String basePath = MinimaConfiguration.SupportImagePhysicalLocation;
            String fixedName = enc.name.Replace("/", "\\");
            FileInfo fi = new FileInfo(basePath + "\\" + fixedName);
            if (!fi.Exists)
            {
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                using (FileStream fs = fi.Create())
                {
                    fs.Write(enc.bits, 0, enc.bits.Length);
                }
            }
            //+
            return new UrlInfo
            {
                url = General.Web.UrlHelper.FixWebPath(WebConfiguration.Domain) + "/" + General.Web.UrlHelper.FixWebPath(MinimaConfiguration.SupportImageWebRelativePath) + "/" + fixedName
            };
        }

        //- @SetTemplate -//
        [XmlRpcMethod("metaWeblog.setTemplate")]
        public Boolean SetTemplate(String key, String blogId, String emailAddress, String password, String template, String templateType)
        {
            String blogGuid = blogId;
            //+
            TraceManager.RecordMethodCall("XmlRpcApi::SetTemplate", new Object[] { key, blogGuid, emailAddress, password, template, templateType });
            //+
            return true;
        }
    }
}