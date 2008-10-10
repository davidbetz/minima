﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
//+
using Minima.Configuration;
using Minima.Service.Client;
//+
namespace Minima.Service.Agent
{
    public class BlogAgent
    {
        //- @GetBlogMetaData -//
        public static BlogMetaData GetBlogMetaData(String blogGuid)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return blogClient.GetBlogMetaData(blogGuid);
            }
        }

        //- @UpdateBlogEntry -//
        public static void UpdateBlogEntry(String blogEntryGuid, String title, String content, String blogEntryTypeGuid, List<Label> labelList, DateTime dateTime, Boolean publish)
        {
            UpdateBlogEntry(blogEntryGuid, title, content, blogEntryTypeGuid, labelList, dateTime, publish, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static void UpdateBlogEntry(String blogEntryGuid, String title, String content, String blogEntryTypeGuid, List<Label> labelList, DateTime dateTime, Boolean publish, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                blogClient.UpdateBlogEntry(blogEntryGuid, title, content, blogEntryTypeGuid, labelList, dateTime, publish);
            }
        }

        //- @GetBlogListForAssociatedAuthor -//
        public static List<BlogMetaData> GetBlogListForAssociatedAuthor(String emailAddress, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = emailAddress;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.GetBlogListForAssociatedAuthor(emailAddress);
            }
        }

        //- @DisableBlogEntry -//
        public static void DisableBlogEntry(String blogEntryGuid)
        {
            DisableBlogEntry(blogEntryGuid, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static void DisableBlogEntry(String blogEntryGuid, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                blogClient.DisableBlogEntry(blogEntryGuid);
            }
        }

        //- @GetSingleBlogEntryByLink -//
        public static BlogEntry GetSingleBlogEntryByLink(String blogGuid, String link, Boolean ignoreFooter)
        {
            return GetSingleBlogEntryByLink(blogGuid, link, ignoreFooter, false, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static BlogEntry GetSingleBlogEntryByLink(String blogGuid, String link, Boolean ignoreFooter, String username, String password)
        {
            return GetSingleBlogEntryByLink(blogGuid, link, ignoreFooter, false, username, password);
        }
        public static BlogEntry GetSingleBlogEntryByLink(String blogGuid, String link, Boolean ignoreFooter, Boolean metaDataOnly)
        {
            return GetSingleBlogEntryByLink(blogGuid, link, ignoreFooter, metaDataOnly, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static BlogEntry GetSingleBlogEntryByLink(String blogGuid, String link, Boolean ignoreFooter, Boolean metaDataOnly, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.GetSingleBlogEntryByLink(blogGuid, link, ignoreFooter, metaDataOnly);
            }
        }

        //- @GetSingleBlogEntry -//
        public static BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean ignoreFooter)
        {
            return GetSingleBlogEntry(blogEntryGuid, false, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean ignoreFooter, String username, String password)
        {
            return GetSingleBlogEntry(blogEntryGuid,  ignoreFooter, false, username, password);
        }
        public static BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean ignoreFooter, Boolean metaDataOnly)
        {
            return GetSingleBlogEntry(blogEntryGuid, metaDataOnly, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean ignoreFooter, Boolean metaDataOnly, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.GetSingleBlogEntry(blogEntryGuid, ignoreFooter, metaDataOnly);
            }
        }

        //- @GetBlogEntryList -//
        public static List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 maxEntryCount, Boolean ignoreFooter, BlogEntryRetreivalType blogEntryRetreivalType)
        {
            return GetBlogEntryList(blogGuid, maxEntryCount, ignoreFooter, blogEntryRetreivalType, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 maxEntryCount, Boolean ignoreFooter, BlogEntryRetreivalType blogEntryRetreivalType, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.GetBlogEntryList(blogGuid, maxEntryCount, true, ignoreFooter, blogEntryRetreivalType);
            }
        }

        //- @PostBlogEntry -//
        public static String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, String blogEntryTypeGuid, List<Label> labelList, Boolean publish)
        {
            return PostBlogEntry(blogGuid, authorList, title, content, dateTime, blogEntryTypeGuid, labelList, publish, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, String blogEntryTypeGuid, List<Label> labelList, Boolean publish, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.PostBlogEntry(blogGuid, authorList, title, content, dateTime, blogEntryTypeGuid, labelList, publish);
            }
        }

        //- @GetArchivedEntryList -//
        public static List<ArchiveCount> GetArchivedEntryList(String blogGuid)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return blogClient.GetArchivedEntryList(blogGuid);
            }
        }

        //- @GetNetBlogEntryList -//
        public static List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, Int32 maxBlogEntryCount, Boolean ignoreFooter)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return blogClient.GetNetBlogEntryList(blogGuid, label, archive, link, maxBlogEntryCount, ignoreFooter);
            }
        }

        //- @GetBlogEntryTypeList -//
        public static List<BlogEntryType> GetBlogEntryTypeList(String blogGuid, List<String> guidList)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return blogClient.GetBlogEntryTypeList(blogGuid, guidList);
            }
        }

        //- @GetBlogEntryListByDateRange -//
        public static List<BlogEntry> GetBlogEntryListByDateRange(string blogGuid, DateTime startDateTime, DateTime endDateTime, Boolean ignoreFooter, Boolean metaDataOnly)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return blogClient.GetBlogEntryListByDateRange(blogGuid, startDateTime, endDateTime, ignoreFooter, metaDataOnly);
            }
        }
    }
}