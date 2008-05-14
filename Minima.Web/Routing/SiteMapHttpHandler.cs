using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
//+
using Minima.Service;
using Minima.Web.Agent;
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
            List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(ContextItemSet.BlogGuid, 0, false);
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
            context.Response.ContentType = "text/xml";
            context.Response.Write(xml.ToString());
        }
    }
}