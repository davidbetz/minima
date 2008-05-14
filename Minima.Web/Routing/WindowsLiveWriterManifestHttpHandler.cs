using System;
using System.Text;
using System.Web;
using System.Xml;
//+
namespace Minima.Web.Routing
{
    public class WindowsLiveWriterManifestHttpHandler : IHttpHandler
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //+
        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            String key = "WindowsLiveWriterManifest_" + ContextItemSet.BlogGuid;
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