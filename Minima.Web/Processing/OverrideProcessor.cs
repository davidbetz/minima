#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Minima.Service;
//+
using Themelia.Web;
//+
namespace Minima.Web.Processing
{
    public class OverrideProcessor : Themelia.Web.Processing.OverrideProcessor
    {
        //- @OnPostProcessorExecute -//
        public override System.Web.IHttpHandler Execute(System.Web.IHttpHandler activeHttpHandler)
        {
            String blogGuid = HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            BlogMetaData blogMetaData = HttpData.GetScopedCacheItem<BlogMetaData>(Info.Scope, Info.BlogMetaData);
            if (blogMetaData == null)
            {
                blogMetaData = Minima.Service.Agent.BlogAgent.GetBlogMetaData(blogGuid);
                HttpData.SetScopedCacheItem<BlogMetaData>(Info.Scope, Info.BlogMetaData, blogMetaData);
            }
            //+
            return activeHttpHandler;
        }
    }
}