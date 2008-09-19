using System;
using System.Text;
using System.Web;
using System.Xml;
//+
namespace Minima.Web.Routing
{
    public class BlogDiscoveryHttpHandler : Themelia.Web.Routing.ReusableNonSessionHttpHandler
    {
        //- @ProcessRequest -//
        public override void ProcessRequest(HttpContext context)
        {
            String blogGuid = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            String key = "BlogDiscovery_" + blogGuid;
            if (String.IsNullOrEmpty(HttpContext.Current.Cache[key] as String))
            {
                StringBuilder xml = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(xml);
                xmlWriter.WriteProcessingInstruction("xml", @"version=""1.0"" encoding=""UTF-8""");
                xmlWriter.WriteStartElement("rsd", "http://archipelago.phrasewise.com/rsd");
                xmlWriter.WriteAttributeString("version", "1.0");
                xmlWriter.WriteStartElement("service");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("engineName");
                xmlWriter.WriteValue("Minima Blog Engine 3.0");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("engineLink");
                xmlWriter.WriteValue(Themelia.Web.WebDomain.Url);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("apis");
                xmlWriter.WriteStartElement("api");
                xmlWriter.WriteAttributeString("name", "MetaWeblog");
                xmlWriter.WriteAttributeString("preferred", "true");
                xmlWriter.WriteAttributeString("apiLink", Themelia.Web.WebDomain.Url + "xml-rpc/");
                xmlWriter.WriteAttributeString("blogID", blogGuid);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("api");
                xmlWriter.WriteAttributeString("name", "Blogger");
                xmlWriter.WriteAttributeString("preferred", "false");
                xmlWriter.WriteAttributeString("apiLink", Themelia.Web.WebDomain.Url + "xml-rpc/");
                xmlWriter.WriteAttributeString("blogID", blogGuid);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
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