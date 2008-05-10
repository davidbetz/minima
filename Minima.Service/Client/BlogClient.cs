using System;
using System.Collections.Generic;
using System.ServiceModel;
//+
namespace Minima.Service.Client
{
    public class BlogClient : MinimaClientBase<IBlogService>, IBlogService
    {
        //- @Ctor -//
        public BlogClient(String endpointConfigurationName)
            : base(endpointConfigurationName) { }

        #region IBlogService Members

        public String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, List<Label> labelList, bool publish)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.PostBlogEntry(blogGuid, authorList, title, content, dateTime, labelList, publish);
            }
        }

        public void DisableBlogEntry(String blogEntryGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.DisableBlogEntry(blogEntryGuid);
            }
        }

        public void UpdateBlogEntry(String blogEntryGuid, String title, String content, List<Label> labelList, DateTime dateTime, bool publish)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.UpdateBlogEntry(blogEntryGuid, title, content, labelList, dateTime, publish);
            }
        }

        public BlogEntry GetSingleBlogEntry(String blogEntryGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                return base.Channel.GetSingleBlogEntry(blogEntryGuid);
            }
        }

        public List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, int maxBlogEntryCount)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetNetBlogEntryList(blogGuid, label, archive, link, maxBlogEntryCount);
            }
        }

        public List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 count, Boolean activeOnly, Boolean includeContent)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogEntryList(blogGuid, count, activeOnly, includeContent);
            }
        }

        public List<ArchiveCount> GetArchivedEntryList(String blogGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetArchivedEntryList(blogGuid);
            }
        }

        public String CreateGoogleSiteMap(String blogGuid)
        {
            return base.Channel.CreateGoogleSiteMap(blogGuid);
        }

        public BlogMetaData GetBlogMetaData(String blogGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogMetaData(blogGuid);
            }
        }

        public List<BlogMetaData> GetBlogListForAssociatedAuthor(String authorEmail)
        {
            return base.Channel.GetBlogListForAssociatedAuthor(authorEmail);
        }

        #endregion
    }
}