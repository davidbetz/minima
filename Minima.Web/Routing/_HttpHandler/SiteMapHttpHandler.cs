using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
//+
using Minima.Service;
using Minima.Service.Agent;
//+
namespace Minima.Web.Routing
{
    public class SiteMapHttpHandler : IHttpHandler
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            String blogGuid = Themelia.Web.HttpData.GetScopedItem<String>("Minima", "BlogGuid");
            String key = "SiteMap_" + blogGuid;
            if (String.IsNullOrEmpty(HttpContext.Current.Cache[key] as String))
            {
                List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(blogGuid, 0, BlogEntryRetreivalType.Full);
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
                HttpContext.Current.Cache.Insert(key, xml.ToString());
            }
            context.Response.ContentType = "text/xml";
            context.Response.Write(HttpContext.Current.Cache[key] as String);
        }
    }
}