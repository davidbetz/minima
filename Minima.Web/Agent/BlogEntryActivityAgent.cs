using BlogEntryActivityLINQ = Minima.Web.Data.Entity.BlogEntryActivity;
//+
using MinimaWebLINQDataContext = Minima.Web.Data.Context.MinimaWebLINQDataContext;
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