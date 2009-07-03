﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Minima.Web.Api.MetaWeblog;
//+
namespace Minima.Web.Processing
{
    public class HandlerFactory : Themelia.Web.Processing.HandlerFactory
    {
        //- @CreateHttpHandler -//
        public override System.Web.IHttpHandler CreateHttpHandler(String text)
        {
            switch (text)
            {
                case "__$minima$blogdiscovery":
                    return new BlogDiscoveryHttpHandler();
                case "__$minima$windowslivewritermanifest":
                    return new WindowsLiveWriterManifestHttpHandler();
                case "__$minima$metaweblogapi":
                    return new MetaWeblogApi();
                case "__$minima$image":
                    return new ImageHttpHandler();
                case "__$minima$sitemap":
                    return new SiteMapHttpHandler();
            }
            //+
            return null;
        }
    }
}