#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Web;
//+
using Minima.Configuration;
//+
using Themelia.Web;
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
                if (ha != null)
                {
                    if (MinimaConfiguration.ForceSpecifiedPath)
                    {
                        if (!Http.AbsoluteUrl.StartsWith(WebConfiguration.Domain.ToLower()))
                        {
                            context.Response.Redirect(WebConfiguration.Domain);
                        }
                    }
                }
            };
        }
    }
}