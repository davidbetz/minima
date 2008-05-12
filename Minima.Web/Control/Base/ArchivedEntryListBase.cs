using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Service;
using Minima.Web.Agent;
//+
namespace Minima.Web.Control
{
    public class ArchivedEntryListBase : ListUserControlBase
    {
        //- @BlogGuid -//
        public String BlogGuid
        {
            get
            {
                return ContextItemSet.BlogGuid;
            }
        }

        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            List<ArchiveCount> archiveList = BlogAgent.GetArchivedEntryList(this.BlogGuid);
            //+
            return archiveList.Select(p => new
            {
                Url = String.Format("/{0}/{1}", p.ArchiveDate.Year, p.ArchiveDate.ToString("MM")),
                MonthText = p.ArchiveDate.ToString("MMMM"),
                Year = p.ArchiveDate.Year,
                Count = p.Count
            });
        }
    }
}