#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
//+
using Minima.Service;
using Minima.Service.Agent;
//+
namespace Minima.Web.Processing
{
    public class SiteMapHttpHandler : Themelia.Web.ReusableHttpHandler
    {
        //- @ProcessRequest -//
        public override void Process()
        {
            String blogGuid = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            String key = "SiteMap_" + blogGuid;
            if (String.IsNullOrEmpty(Themelia.Web.Http.Cache[key] as String))
            {
                List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(blogGuid, 0, false, BlogEntryRetreivalType.Full);
                StringBuilder xml = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(xml);
                xmlWriter.WriteProcessingInstruction("xml", @"version=""1.0"" encoding=""UTF-8""");
                xmlWriter.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                foreach (BlogEntry blogEntry in blogEntryList)
                {
                    xmlWriter.WriteStartElement("url");
                    //+
                    xmlWriter.WriteStartElement("loc");
                    //xmlWriter.WriteValue(blogEntry.BlogEntryUri.AbsoluteUri);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("lastmod");
                    xmlWriter.WriteValue(blogEntry.PostDateTime.ToString("yyyy-MM-dd"));
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("changefreq");
                    xmlWriter.WriteValue("never");
                    xmlWriter.WriteEndElement();
                    //+
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                //+
                Themelia.Web.Http.Cache.Insert(key, xml.ToString());
            }
            ContentType = "text/xml";
            Output.Append(Themelia.Web.Http.Cache[key] as String);
        }
    }
}