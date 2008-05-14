using System;
//+
using Minima.Web.Api.MetaWeblog;
//+
namespace Minima.Web.Routing
{
    public class MappedHttpHandlerFactory : General.Web.Routing.MappedHttpHandlerFactoryBase
    {
        //- @GetHttpHandler -//
        public override System.Web.IHttpHandler CreateHttpHandler(String text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return null;
            }
            switch (text.ToLower())
            {
                case "blogdiscovery":
                    return new BlogDiscoveryHttpHandler();
                case "windowslivewritermanifest":
                    return new WindowsLiveWriterManifestHttpHandler();
                case "blogentryimage":
                    return new BlogEntryImageHttpHandler();
                case "metaweblogapi":
                    return new MetaWeblogApi();
                case "fileprocessor":
                    return new FileProcessorHttpHandler();
                case "sitemap":
                    return new SiteMapHttpHandler();
                case "urlprocessing":
                    return new UrlProcessingHttpHandler();
            }
            //+
            return null;
        }
    }
}