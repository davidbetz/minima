#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Text;
using System.Web;
using System.Xml;
//+
namespace Minima.Web.Processing
{
    public class WindowsLiveWriterManifestHttpHandler : Themelia.Web.ReusableHttpHandler
    {
        //- @ProcessRequest -//
        public override void Process()
        {
            String key = "WindowsLiveWriterManifest_" + Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            if (String.IsNullOrEmpty(Themelia.Web.Http.Cache[key] as String))
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
                Themelia.Web.Http.Cache.Insert(key, xml.ToString());
            }
            ContentType = "text/xml";
            Output.Append(Themelia.Web.Http.Cache[key] as String);
        }
    }
}