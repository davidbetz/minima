using System;
//+
using Themelia.Web;
//+
using Minima.Service;
//+
namespace Minima.Web.Routing
{
    public class MinimaPostProcessor : Themelia.Web.Routing.PostProcessorBase
    {
        //- @OnPostProcessorExecute -//
        public override System.Web.IHttpHandler OnPostProcessorExecute(System.Web.HttpContext context, System.Web.IHttpHandler activeHttpHandler, params object[] parameterArray)
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