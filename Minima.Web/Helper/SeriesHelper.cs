using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//+
using Minima.Service;
using Minima.Configuration;
//+
namespace Minima.Web.Helper
{
    public static class SeriesHelper
    {
        //- @GetBlogEntryLabelSeries -//
        public static String GetBlogEntryLabelSeries(BlogEntry blogEntry)
        {
            StringBuilder labelSeries = new StringBuilder();
            if (blogEntry.LabelList != null)
            {
                Boolean first = true;
                labelSeries.Append("{");
                if (blogEntry.LabelList.Count < 1)
                {
                    return String.Empty;
                }
                foreach (Minima.Service.Label label in blogEntry.LabelList)
                {
                    if (blogEntry.LabelList.Count > 1 && !first)
                    {
                        labelSeries.Append(", ");
                    }

                    labelSeries.Append(String.Format("<a href=\"{1}\">{0}</a>", label.Title, LabelHelper.GetLabelUrl(label)));
                    first = false;
                }
                labelSeries.Append("}");
            }
            //+
            return labelSeries.ToString();
        }

        //- @GetBlogEntryAuthorSeries -//
        public static String GetBlogEntryAuthorSeries(BlogEntry blogEntry)
        {
            StringBuilder authorSeries = new StringBuilder();
            if (blogEntry.AuthorList != null)
            {
                Boolean first = true;
                if (blogEntry.AuthorList.Count < 1)
                {
                    return String.Empty;
                }
                else if (blogEntry.AuthorList.Count > 1)
                {
                    authorSeries.Append("{");
                }
                foreach (Author author in blogEntry.AuthorList)
                {
                    if (blogEntry.AuthorList.Count > 1 && !first)
                    {
                        authorSeries.Append(", ");
                    }
                    if (MinimaConfiguration.LinkAuthorToEmail)
                    {
                        authorSeries.Append(String.Format("<a href=\"mailto:{1}\">{0}</a>", author.Name, author.Email));
                    }
                    else
                    {
                        authorSeries.Append(author.Name);
                    }
                    first = false;
                }
                if (blogEntry.AuthorList.Count > 1)
                {
                    authorSeries.Append("}");
                }
            }
            //+
            return authorSeries.ToString();
        }
    }
}
