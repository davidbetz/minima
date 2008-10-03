#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using DataContext = Minima.Web.Data.Context.MinimaWebLINQDataContext;
using BlogEntryActivityLINQ = Minima.Web.Data.Entity.BlogEntryActivity;
//+
namespace Minima.Web.Agent
{
    internal class BlogEntryActivityAgent
    {
        internal static void ReportActivity(BlogEntryActivityLINQ blogEntryActivityLinq)
        {
            using (DataContext db = new DataContext(WebConfiguration.ConnectionString))
            {
                db.BlogEntryActivities.InsertOnSubmit(blogEntryActivityLinq);
                try
                {
                    db.SubmitChanges();
                }
                catch(System.Exception ex)
                {
                    throw new System.ApplicationException("Unable to access BlogEntryActivity tables.  Either set EnableActivityLogging to false in web.config or check your web database settings.", ex);
                }
            }
        }
    }
}