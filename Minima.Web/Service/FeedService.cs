#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Syndication;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Service.Client;
using Minima.Web.Helper;
//+
namespace Minima.Web.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FeedService : IFeedService
    {
        //- @GetRssFeed -//
        public Rss20FeedFormatter GetRssFeed(String blogGuid, String maxCount)
        {
            Int32 maxCountInt32;
            if (!Int32.TryParse(maxCount, out maxCountInt32))
            {
                return null;
            }
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                List<BlogEntry> blogEntryList = null;
                try
                {
                    blogEntryList = blogClient.GetBlogEntryList(blogGuid, maxCountInt32, true, BlogEntryRetreivalType.MetaDataOnly);
                }
                catch (FaultException<ArgumentException>)
                {
                    return null;
                }
                catch (FaultException<SecurityException>)
                {
                    return null;
                }
                catch
                {
                    return null;
                }
                if (blogEntryList.Count == 0)
                {
                    return null;
                }
                BlogMetaData blogMetaData = blogClient.GetBlogMetaData(blogGuid);
                //+ blog
                SyndicationFeed syndicationFeed = new SyndicationFeed();
                syndicationFeed.Description = new TextSyndicationContent(blogMetaData.Description);
                foreach (Label label in blogMetaData.LabelList)
                {
                    syndicationFeed.Categories.Add(new SyndicationCategory(label.Title));
                }
                //+ blog entry list
                List<SyndicationItem> itemList = new List<SyndicationItem>();
                foreach (BlogEntry blogEntry in blogEntryList)
                {
                    SyndicationItem syndicationItem = new SyndicationItem
                    {
                        Title = new TextSyndicationContent(blogEntry.Title),
                        Summary = new TextSyndicationContent(blogEntry.Content),
                        PublishDate = new DateTimeOffset(blogEntry.PostDateTime),
                    };
                    syndicationItem.Links.Add(new SyndicationLink(new Uri(Themelia.Web.Http.Root + "/" + Themelia.Web.UrlCleaner.FixWebPathHead(blogEntry.MappingNameList.First()))));
                    foreach (Author author in blogEntry.AuthorList)
                    {
                        syndicationItem.Authors.Add(new SyndicationPerson(author.Email));
                    }
                    foreach (Label label in blogEntry.LabelList)
                    {
                        syndicationItem.Categories.Add(new SyndicationCategory(label.Title));
                    }
                    //+
                    itemList.Add(syndicationItem);
                }
                syndicationFeed.Items = itemList;
                //+
                return new Rss20FeedFormatter(syndicationFeed);
            }
        }
    }
}