using System;
using System.Web;
//+
using Minima.Configuration;
//+
namespace Minima.Web.Module
{
    public class FixDomainHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(System.Web.HttpApplication context)
        {
            context.BeginRequest += delegate(Object sender, EventArgs ea)
            {
                HttpApplication ha = sender as HttpApplication;
                String absoluteUrl = ha.Context.Request.Url.ToString().ToLower();
                if (ha != null)
                {
                    if (MinimaConfiguration.ForceSpecifiedPath)
                    {
                        if (!absoluteUrl.StartsWith(WebConfiguration.Domain.ToLower()))
                        {
                            context.Response.Redirect(WebConfiguration.Domain);
                        }
                    }
                }
            };
        }
    }
}