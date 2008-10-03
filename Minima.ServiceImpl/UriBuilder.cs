#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Linq;
using System.Text;
//+
using BlogEntryLINQ = Minima.Service.Data.Entity.BlogEntry;
using BlogEntryUrlMappingLINQ = Minima.Service.Data.Entity.BlogEntryUrlMapping;
using BlogLINQ = Minima.Service.Data.Entity.Blog;
//+
namespace Minima.Service
{
    public static class UriBuilder
    {
        public static String Build(BlogEntryLINQ blogEntryLinq, BlogLINQ blogLinq)
        {
            DateTime postDateTime = blogEntryLinq.BlogEntryPostDateTime;
            BlogEntryUrlMappingLINQ blogEntryUrlMappingLinq = blogEntryLinq.BlogEntryUrlMappings.FirstOrDefault();
            if (blogEntryUrlMappingLinq == null)
            {
                return String.Empty;
            }
            String urlMapping = blogEntryUrlMappingLinq.BlogEntryUrlMappingName;
            String baseBlogUrl = blogLinq.BlogPrimaryUrl;
            //+
            StringBuilder blogEntryPage = new StringBuilder();
            blogEntryPage.Append(postDateTime.Year);
            blogEntryPage.Append("/");
            String month = String.Empty;
            if (postDateTime.Month.ToString().Length < 2)
            {
                month = "0" + postDateTime.Month.ToString();
            }
            else
            {
                month = postDateTime.Month.ToString();
            }
            blogEntryPage.Append(String.Format("{0:00}/", postDateTime.Month));
            //+
            if (!String.IsNullOrEmpty(urlMapping))
            {
                blogEntryPage.Append(urlMapping);
            }
            else
            {
                throw new FormatException("BlogEntryPostURlMapping not set");
            }
            //+
            return String.Format("{0}{1}", baseBlogUrl, blogEntryPage.ToString());
        }
    }
}