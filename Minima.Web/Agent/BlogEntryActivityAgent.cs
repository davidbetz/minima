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
                db.SubmitChanges();
            }
        }
    }
}