#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Minima.Web.Api.MetaWeblog;
//+
namespace Minima.Web.Routing
{
    public class HandlerFactory : Themelia.Web.Routing.HandlerFactoryBase
    {
        //- @CreateHttpHandler -//
        public override System.Web.IHttpHandler CreateHttpHandler(String text)
        {
            switch (text)
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