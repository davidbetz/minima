using System;
using System.Text;
using Themelia.Web;
//+
namespace Minima.Web.Helper
{
    public static class BlogEntryHelper
    {
        //- @BuildBlogEntry -//
        public static String BuildBlogEntry(DateTime postDateTime, String urlMapping, Uri baseBlogUri)
        {
            String baseBlogUrl = baseBlogUri.AbsoluteUri;
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
        public static String BuildBlogEntry(DateTime postDateTime, String urlMapping, String webDomain)
        {
            Uri baseBlogUri = WebDomain.GetUrl(webDomain);
            //+
            return BuildBlogEntry(postDateTime, urlMapping, baseBlogUri);
        }
    }
}