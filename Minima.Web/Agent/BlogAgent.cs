using System;
using System.Collections.Generic;
using Minima.Configuration;
//+
using Minima.Service;
using Minima.Service.Client;
//+
namespace Minima.Web.Agent
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
        public static void UpdateBlogEntry(String blogEntryGuid, String title, String content, List<Label> labelList, DateTime dateTime, Boolean publish)
        {
            UpdateBlogEntry(blogEntryGuid, title, content, labelList, dateTime, publish, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static void UpdateBlogEntry(String blogEntryGuid, String title, String content, List<Label> labelList, DateTime dateTime, Boolean publish, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                blogClient.UpdateBlogEntry(blogEntryGuid, title, content, labelList, dateTime, publish);
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

        //- @GetSingleBlogEntry -//
        public static BlogEntry GetSingleBlogEntry(String blogEntryGuid)
        {
            return GetSingleBlogEntry(blogEntryGuid, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static BlogEntry GetSingleBlogEntry(String blogEntryGuid, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.GetSingleBlogEntry(blogEntryGuid);
            }
        }

        //- @GetBlogEntryList -//
        public static List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 maxEntryCount, Boolean includeContent)
        {
            return GetBlogEntryList(blogGuid, maxEntryCount, includeContent, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 maxEntryCount, Boolean includeContent, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.GetBlogEntryList(blogGuid, maxEntryCount, true, includeContent);
            }
        }

        //- @PostBlogEntry -//
        public static String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, List<Label> labelList, Boolean publish)
        {
            return PostBlogEntry(blogGuid, authorList, title, content, dateTime, labelList, publish, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, List<Label> labelList, Boolean publish, String username, String password)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = username;
                blogClient.ClientCredentials.UserName.Password = password;
                //+
                return blogClient.PostBlogEntry(blogGuid, authorList, title, content, dateTime, labelList, publish);
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
        public static List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, Int32 maxBlogEntryCount)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return blogClient.GetNetBlogEntryList(blogGuid, label, archive, link, maxBlogEntryCount);
            }
        }

        //- @CreateGoogleSiteMap -//
        public static String CreateGoogleSiteMap(String blogGuid)
        {
            using (BlogClient blogClient = new BlogClient(MinimaConfiguration.ActiveBlogServiceEndpoint))
            {
                blogClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                blogClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return blogClient.CreateGoogleSiteMap(blogGuid);
            }
        }
    }
}