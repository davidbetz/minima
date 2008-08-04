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

        public void DisableBlogEntry(String blogEntryGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.DisableBlogEntry(blogEntryGuid);
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

        public List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 count, Boolean activeOnly, BlogEntryRetreivalType blogEntryRetreivalType)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogEntryList(blogGuid, count, activeOnly, blogEntryRetreivalType);
            }
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

        public List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, int maxBlogEntryCount)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetNetBlogEntryList(blogGuid, label, archive, link, maxBlogEntryCount);
            }
        }

        public BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean metaDataOnly)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                return base.Channel.GetSingleBlogEntry(blogEntryGuid, metaDataOnly);
            }
        }

        public String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, String blogEntryTypeGuid, List<Label> labelList, bool publish)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.PostBlogEntry(blogGuid, authorList, title, content, dateTime, blogEntryTypeGuid, labelList, publish);
            }
        }

        public void UpdateBlogEntry(String blogEntryGuid, String title, String content, String blogEntryTypeGuid, List<Label> labelList, DateTime dateTime, bool publish)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.UpdateBlogEntry(blogEntryGuid, title, content, blogEntryTypeGuid, labelList, dateTime, publish);
            }
        }

        public List<BlogEntryType> GetBlogEntryTypeList(String blogGuid, List<String> guidList)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogEntryTypeList(blogGuid, guidList);
            }
        }

        public List<BlogEntry> GetBlogEntryListByDateRange(String blogGuid, DateTime startDateTime, DateTime endDateTime, Boolean metaDataOnly)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogEntryListByDateRange(blogGuid, startDateTime, endDateTime, metaDataOnly);
            }
        }
    }
}