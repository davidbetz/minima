using System;
using System.Text;
//+
namespace Minima.Service
{
    public static class UriBuilder
    {
        public static String Build(DateTime postDateTime, String urlMapping, String baseBlogUrl)
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
            return String.Format("{0}{1}", baseBlogUrl, blogEntryPage.ToString());
        }
    }
}