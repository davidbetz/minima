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
        //- $Data -//
        private class Info
        {
            public const string Minima = "Minima";
            public const string BlogGuid = "BlogGuid";
            public const string BlogMetaData = "BlogMetaData";
        }

        //+
        //- @OnPostProcessorExecute -//
        public override System.Web.IHttpHandler OnPostProcessorExecute(System.Web.HttpContext context, System.Web.IHttpHandler activeHttpHandler, params object[] parameterArray)
        {
            String blogGuid = HttpData.GetScopedItem<String>(Info.Minima, Info.BlogGuid);
            BlogMetaData blogMetaData = HttpData.GetScopedCacheItem<BlogMetaData>(Info.Minima, Info.BlogMetaData);
            if (blogMetaData == null)
            {
                blogMetaData = Minima.Service.Agent.BlogAgent.GetBlogMetaData(blogGuid);
                HttpData.SetScopedCacheItem<BlogMetaData>(Info.Minima, Info.BlogMetaData, blogMetaData);
            }
            //+
            return activeHttpHandler;
        }
    }
}