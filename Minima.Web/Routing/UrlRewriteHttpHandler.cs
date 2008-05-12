using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//+
using Minima.Web.Configuration;
//+
namespace Minima.Web
{
    class UrlRewriteHttpHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            context.Response.Cache.SetExpires(DateTime.MinValue);
            //+
            String absoluteUrl = context.Request.Url.AbsoluteUri;
            String absolutePath = context.Request.Url.AbsolutePath;
            //+
            List<UrlRewriteElement> urlRewriteList = WebConfigurationFacade.GetWebConfiguration().UrlRewrites.OrderBy(p => p.Priority).ToList();
            UrlRewriteElement t = urlRewriteList.FirstOrDefault(u => absolutePath.ToLower().Contains(u.Match.ToLower()));
            if (t != null)
            {
                String baseAddress = absoluteUrl.Substring(0, absoluteUrl.Length - absolutePath.Length);
                UriTemplate template = new UriTemplate(t.Source);
                UriTemplateMatch match = template.Match(new Uri(baseAddress), new Uri(absoluteUrl));
                //+
                if (match != null)
                {
                    SetContextItems(context, match);
                }
                //+
                IHttpHandler h = PageParser.GetCompiledPageInstance(String.Format("~/{0}", t.Target), null, context);
                h.ProcessRequest(context);
            }
        }

        //- $SetContextItems -//
        private void SetContextItems(HttpContext context, UriTemplateMatch match)
        {
            if (match != null)
            {
                foreach (String name in match.BoundVariables)
                {
                    context.Items.Add(name.ToLower(), match.BoundVariables[name]);
                }
            }
        }
    }
}