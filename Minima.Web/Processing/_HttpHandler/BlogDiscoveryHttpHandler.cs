#region Copyright
//+ Copyright � Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Text;
using System.Web;
using System.Xml;
//+
namespace Minima.Web.Processing
{
    public class BlogDiscoveryHttpHandler : Themelia.Web.ReusableHttpHandler
    {
        //- @ProcessRequest -//
        public override void Process()
        {
            String blogGuid = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            String key = "BlogDiscovery_" + blogGuid;
            if (String.IsNullOrEmpty(Themelia.Web.Http.Cache[key] as String))
            {
                StringBuilder xml = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(xml);
                xmlWriter.WriteProcessingInstruction("xml", @"version=""1.0"" encoding=""UTF-8""");
                xmlWriter.WriteStartElement("rsd", "http://archipelago.phrasewise.com/rsd");
                xmlWriter.WriteAttributeString("version", "1.0");
                xmlWriter.WriteStartElement("service");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("engineName");
                xmlWriter.WriteValue("Minima Blog Engine 3.1");
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
                Themelia.Web.Http.Cache.Insert(key, xml.ToString());
            }
            ContentType = "text/xml";
            Output.Append(Themelia.Web.Http.Cache[key] as String);
        }
    }
}