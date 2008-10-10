#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
//+
using CookComputing.XmlRpc;
//+
using Themelia.Reporting;
using Themelia.Web.Routing.Data;
//+
using Minima.Service;
using Minima.Service.Agent;
using Minima.Web.Helper;
//+
namespace Minima.Web.Api.MetaWeblog
{
    [XmlRpcService(Name = "Minima API", AutoDocumentation = true)]
    public class MetaWeblogApi : XmlRpcService
    {
        public Reporter _reporter;

        //+
        //- @Info -//
        public class Info : Minima.Web.Info
        {
            public const string BlogReporter = "BlogReporter";
        }

        //+
        //- @Reporter -//
        public Reporter Reporter
        {
            get
            {
                if (_reporter == null)
                {
                    _reporter = ReportController.GetReporter(Info.BlogReporter);
                    if (!_reporter.Initialized)
                    {
                        _reporter.ReportCreator = new ObjectArrayReportCreator();
                    }
                }
                //+
                return _reporter;
            }
        }

        //+
        //- @DeletePost -//
        [XmlRpcMethod("blogger.deletePost")]
        public Boolean DeletePost(String appKey, String postid, String emailAddress, String password, Boolean publish)
        {
            String blogEntryGuid = postid;
            //+
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { appKey, blogEntryGuid, emailAddress, password, publish }, "XmlRpcApi::DeletePost", false);
            }
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
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { blogEntryGuid, emailAddress, password, post.description, post.title, publish }, "XmlRpcApi::EditPost", false);
            }
            //+
            List<Label> labelList = new List<Label>();
            foreach (String title in post.categories)
            {
                labelList.Add(LabelAgent.GetLabelByTitle(title));
            }
            BlogAgent.UpdateBlogEntry(blogEntryGuid, post.title, post.description, null, labelList, new DateTime(0x79d, 1, 1), publish, emailAddress, password);
            //+
            return true;
        }

        //- @GetCategories -//
        [XmlRpcMethod("metaWeblog.getCategories")]
        public CategoryInfo[] GetCategories(String blogid, String emailAddress, String password)
        {
            String blogGuid = blogid;
            //+
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { blogGuid, emailAddress, password }, "XmlRpcApi::GetCategories", false);
            }
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
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { blogEntryGuid, emailAddress, password }, "XmlRpcApi::GetPost", false);
            }
            //+
            BlogEntry blogEntry = BlogAgent.GetSingleBlogEntry(blogEntryGuid, emailAddress, password);
            //+
            return new FullPost
            {
                dateCreated = blogEntry.PostDateTime,
                description = blogEntry.Content,
                title = blogEntry.Title,
                categories = blogEntry.LabelList.Select(p => p.Title).ToArray(),
                link = Themelia.Web.UrlCleaner.FixWebPathHead(blogEntry.MappingNameList.First()),
                permalink = Themelia.Web.UrlCleaner.FixWebPathHead(blogEntry.MappingNameList.First()),
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
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { blogGuid, emailAddress, password, maxEntryCount }, "XmlRpcApi::GetRecentPosts", false);
            }
            //+
            List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(blogGuid, maxEntryCount, BlogEntryRetreivalType.Full, emailAddress, password);
            //+
            return blogEntryList.Select(p => new FullPost
                {
                    dateCreated = p.PostDateTime,
                    description = p.Content,
                    title = p.Title,
                    link = Themelia.Web.Http.Root + "/" + Themelia.Web.UrlCleaner.FixWebPathHead(p.MappingNameList.First()),
                    permalink = Themelia.Web.Http.Root + "/" + Themelia.Web.UrlCleaner.FixWebPathHead(p.MappingNameList.First()),
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
            this.Reporter.SendSingle(new Object[] { appKey, blogGuid, emailAddress, password, templateType }, "XmlRpcApi::GetTemplate", false);
            //+
            return String.Empty;
        }

        //- @GetUsersBlogs -//
        [XmlRpcMethod("blogger.getUsersBlogs")]
        public BlogInfo[] GetUsersBlogs(String key, String emailAddress, String password)
        {
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { key, emailAddress, password }, "XmlRpcApi::GetUsersBlogs", false);
            }
            //+
            List<BlogMetaData> blogList = BlogAgent.GetBlogListForAssociatedAuthor(emailAddress, password);
            //+
            Themelia.Map webDomainPathMap = new Themelia.Map();
            foreach (WebDomainData webDomainData in WebDomainDataList.AllWebDomainData)
            {
                ComponentData data = webDomainData.ComponentDataList[Info.Scope];
                if (data != null)
                {
                    String blogGuid = data.ParameterDataList[Info.BlogGuid].Value;
                    webDomainPathMap.Add(blogGuid, webDomainData.Path);
                }
            }
            List<String> registeredBlogGuidList = webDomainPathMap.GetKeyList();
            //+
            BlogInfo[] netBlogList = (from b in blogList
                                      where registeredBlogGuidList.Contains(b.Guid)
                                      select new BlogInfo
                                      {
                                          blogid = b.Guid,
                                          blogName = b.Title,
                                          url = Themelia.Web.WebDomain.GetUrl(webDomainPathMap[b.Guid])
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
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { blogGuid, emailAddress, password, post.description, post.title, publish }, "XmlRpcApi::MetaWeblogNewPost", false);
            }
            //+
            return BlogAgent.PostBlogEntry(blogGuid, new List<Author>(
                new Author[] {
                    new Author {
                        Email = emailAddress
                    } 
                }
            ), post.title, post.description, DateTime.Now, null, null, publish, emailAddress, password);
        }

        //- @NewMediaObject -//
        [XmlRpcMethod("metaWeblog.newMediaObject")]
        public UrlInfo NewMediaObject(String blogid, String emailAddress, String password, MediaType enc)
        {
            String blogGuid = blogid;
            //+
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { blogGuid, emailAddress, password, enc.name, enc.type, enc.bits.Length }, "XmlRpcApi::NewMediaObject", false);
            }
            //+
            Uri uri = new Uri(Themelia.Web.Http.Root + "/imagestore/" + blogGuid);
            String blogImageGuid = Themelia.Net.HttpAbstractor.PostHttpRequest(uri, enc.bits, new Themelia.Map("ImageContentType=" + enc.type));
            //+
            return new UrlInfo
            {
                url = Themelia.Web.Http.Root + "/imagestore/" + blogImageGuid
            };
        }

        //- @SetTemplate -//
        [XmlRpcMethod("metaWeblog.setTemplate")]
        public Boolean SetTemplate(String key, String blogId, String emailAddress, String password, String template, String templateType)
        {
            String blogGuid = blogId;
            //+
            if (this.Reporter.Initialized)
            {
                this.Reporter.SendSingle(new Object[] { key, blogGuid, emailAddress, password, template, templateType }, "XmlRpcApi::SetTemplate", false);
            }
            //+
            return true;
        }
    }
}