#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Linq;
//+
using Minima.Service;
using Minima.Service.Agent;
//+
using Themelia;
//+
namespace Minima.Parsing
{
    public class BlogEntryCodeParser : Themelia.CodeParsing.CodeParser
    {
        //- @Id -//
        public override String Id
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
                    map.Add("Link", "/" + Themelia.Web.UrlCleaner.FixWebPathHead(blogEntry.MappingNameList.First()));
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