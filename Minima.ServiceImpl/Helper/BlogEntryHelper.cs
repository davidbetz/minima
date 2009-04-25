#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Text;
//+
namespace Minima.Service.Helper
{
    public static class BlogEntryHelper
    {
        //- @BuildBlogEntry -//
        public static String BuildBlogEntryLink(DateTime postDateTime, String urlMapping)
        {
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
            return blogEntryPage.ToString();
        }
    }
}