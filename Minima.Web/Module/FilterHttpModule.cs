using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//+
using Minima.Configuration;
//+
using AccessLINQ = Minima.Web.Data.Entity.Access;
//+
using MinimaWebLINQDataContext = Minima.Web.Data.Context.MinimaWebLINQDataContext;
//+
namespace Minima.Web.Module
{
    public class FilterHttpModule : IHttpModule
    {
        //- @Dispose -//
        public void Dispose()
        {
        }

        //- @Init -//
        public void Init(System.Web.HttpApplication context)
        {
            context.BeginRequest += delegate(Object sender, EventArgs ea)
            {
                HttpApplication ha = sender as HttpApplication;
                if (ha != null)
                {
                    using (MinimaWebLINQDataContext db = new MinimaWebLINQDataContext(WebConfiguration.ConnectionString))
                    {
                        List<AccessLINQ> accessLinqList = db.Accesses.Where(p => p.AccessType == 'I' && p.BlogGuid == MinimaConfiguration.BlogGuid && p.AccessEnabled == true).ToList();
                        //+ ip address
                        foreach (AccessLINQ access in accessLinqList)
                        {
                            if (context.Request.ServerVariables["REMOTE_ADDR"] == access.AccessContent)
                            {
                                BlockAccess(access);
                                return;
                            }
                        }
                        //+ user agent
                        accessLinqList = db.Accesses.Where(p => p.AccessType == 'U' && p.BlogGuid == MinimaConfiguration.BlogGuid && p.AccessEnabled == true).ToList();
                        foreach (AccessLINQ access in accessLinqList)
                        {
                            if (context.Request.UserAgent.Contains(access.AccessContent))
                            {
                                BlockAccess(access);
                                return;
                            }
                        }
                        //+ http referrer
                        accessLinqList = db.Accesses.Where(p => p.AccessType == 'H' && p.BlogGuid == MinimaConfiguration.BlogGuid && p.AccessEnabled == true).ToList();
                        foreach (AccessLINQ access in accessLinqList)
                        {
                            if (context.Request.UrlReferrer != null && context.Request.UrlReferrer.AbsoluteUri == access.AccessContent)
                            {
                                BlockAccess(access);
                                return;
                            }
                        }
                    }
                }
            };
        }

        //- $BlockAccess -//
        private void BlockAccess(AccessLINQ access)
        {
            if (!String.IsNullOrEmpty(access.AccessHttpForward))
            {
                HttpContext.Current.Response.Redirect(access.AccessHttpForward);
                return;
            }
            if (!String.IsNullOrEmpty(access.AccessOutputMessage))
            {
                SurpressContent(HttpContext.Current, access.AccessOutputMessage);
                return;
            }
            SurpressContent(HttpContext.Current);
        }

        //- $SurpressContent -//
        private void SurpressContent(HttpContext context, String message)
        {
            context.Response.StatusCode = 200;
            context.Response.Write(message);
            context.Response.SuppressContent = false;
            context.Response.End();
        }

        //- $SurpressContent -//
        private void SurpressContent(HttpContext context)
        {
            context.Response.StatusCode = 404;
            context.Response.Write(String.Empty);
            context.Response.SuppressContent = true;
            context.Response.End();
        }
    }
}