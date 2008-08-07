using System;
using System.Linq;
using System.Text.RegularExpressions;
//+
using Themelia;
//+
using Minima.Service;
using Minima.Service.Agent;
using Minima.Web.Helper;
//+
namespace Minima.Parsing
{
    public class BlogEntryCodeParser : Themelia.CodeParsing.CodeParserBase
    {
        //- @Id -//
        public override string Id
        {
            get { return "BlogEntry"; }
        }

        //+
        //- @Parse -//
        public override String ParseCode(String code)
        {
            Template linkTemplate = new Template(@"<a href=""{Link}"">{Text}</a>");
            Map map = new Map();
            if (!String.IsNullOrEmpty(code))
            {
                String[] partArray = code.Split('|');
                BlogEntry blogEntry = BlogAgent.GetSingleBlogEntry(partArray[0], false);
                if (blogEntry != null && blogEntry.MappingNameList != null)
                {
                    String link = BlogEntryHelper.BuildBlogEntry(blogEntry.PostDateTime, blogEntry.MappingNameList.First(), Themelia.Web.WebSection.Current);
                    map.Add("Link", link);
                }
                if (partArray.Length > 1)
                {
                    String[] partArray2 = partArray[1].Split(';');
                    map.Add("Text", partArray2[0]);
                }
                else
                {
                    map.Add("Text", blogEntry.Title);
                }
                return linkTemplate.Interpolate(map);
            }
            //+
            return null;
        }
    }
}