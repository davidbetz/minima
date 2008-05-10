using System;
using System.Collections.Generic;
//+
using Minima.Service;
using Minima.Service.Client;
//+
using MinimaWebLINQDataContext = Minima.Web.Data.Context.MinimaWebLINQDataContext;
using AccessLINQ = Minima.Web.Data.Entity.Access;
using BlogEntryActivityLINQ = Minima.Web.Data.Entity.BlogEntryActivity;
using BlogEntryActivityTypeLINQ = Minima.Web.Data.Entity.BlogEntryActivityType;
using FileMappingLINQ = Minima.Web.Data.Entity.FileMapping;
using TraceLINQ = Minima.Web.Data.Entity.Trace;
using TraceTypeLINQ = Minima.Web.Data.Entity.TraceType;
//+
namespace Minima.Web.Agent
{
    internal class BlogEntryActivityAgent
    {
        internal static void ReportActivity(BlogEntryActivityLINQ blogEntryActivityLinq)
        {
            using (MinimaWebLINQDataContext db = new MinimaWebLINQDataContext(WebConfiguration.ConnectionString))
            {
                db.BlogEntryActivities.InsertOnSubmit(blogEntryActivityLinq);
                db.SubmitChanges();
            }
        }
    }
}