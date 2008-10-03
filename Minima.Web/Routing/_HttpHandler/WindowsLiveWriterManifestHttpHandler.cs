#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Text;
using System.Web;
using System.Xml;
//+
namespace Minima.Web.Routing
{
    public class WindowsLiveWriterManifestHttpHandler : Themelia.Web.Routing.ReusableNonSessionHttpHandler
    {
        //- @ProcessRequest -//
        public override void ProcessRequest(HttpContext context)
        {
            String key = "WindowsLiveWriterManifest_" + Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            if (String.IsNullOrEmpty(HttpContext.Current.Cache[key] as String))
            {
                StringBuilder xml = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(xml);
                xmlWriter.WriteStartElement("weblog");
                xmlWriter.WriteStartElement("serviceName");
                xmlWriter.WriteValue("Minima Blog Engine 3.0");
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