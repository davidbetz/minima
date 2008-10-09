#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Minima.Service;
//+
using Themelia.Web;
//+
namespace Minima.Web.Routing
{
    public class PostProcessor : Themelia.Web.Routing.PostProcessorBase
    {
        //- @OnPostProcessorExecute -//
        public override System.Web.IHttpHandler OnPostProcessorExecute(System.Web.HttpContext context, System.Web.IHttpHandler activeHttpHandler, params Object[] parameterArray)
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