using System;
using System.Web;
//+
using General.Web;
//+
using Minima.Configuration;
//+
namespace Minima.Web.Module
{
    public class FixDomainHttpModule : IHttpModule
    {
        //- @Dispose -//
        public void Dispose() { }

        //- @Init -//
        public void Init(System.Web.HttpApplication context)
        {
            context.BeginRequest += delegate(Object sender, EventArgs ea)
            {
                HttpApplication ha = sender as HttpApplication;
                String absoluteUrl = Http.Url.ToString().ToLower();
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