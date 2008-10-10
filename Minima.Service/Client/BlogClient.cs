#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
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

        public List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 count, Boolean activeOnly, Boolean ignoreFooter, BlogEntryRetreivalType blogEntryRetreivalType)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogEntryList(blogGuid, count, activeOnly, ignoreFooter, blogEntryRetreivalType);
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

        public List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, int maxBlogEntryCount, Boolean ignoreFooter)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetNetBlogEntryList(blogGuid, label, archive, link, maxBlogEntryCount, ignoreFooter);
            }
        }

        public BlogEntry GetSingleBlogEntryByLink(String blogGuid, String link, Boolean ignoreFooter, Boolean metaDataOnly)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetSingleBlogEntryByLink(blogGuid, link, ignoreFooter, metaDataOnly);
            }
        }

        public BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean ignoreFooter, Boolean metaDataOnly)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                return base.Channel.GetSingleBlogEntry(blogEntryGuid, ignoreFooter, metaDataOnly);
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

        public List<BlogEntry> GetBlogEntryListByDateRange(String blogGuid, DateTime startDateTime, DateTime endDateTime, Boolean ignoreFooter, Boolean metaDataOnly)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogEntryListByDateRange(blogGuid, startDateTime, endDateTime, ignoreFooter, metaDataOnly);
            }
        }
    }
}