using System;
//+
using Minima.Web.Api.MetaWeblog;
//+
namespace Minima.Web.Routing
{
    public class MappedHttpHandlerFactory : Themelia.Web.Routing.AliasedHandlerFactoryBase
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
                case "metaweblogapi":
                    return new MetaWeblogApi();
                case "fileprocessor":
                    return new FileProcessorHttpHandler();
                case "image":
                    return new ImageHttpHandler();
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