using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Configuration;
using Minima.Web.Agent;
using Minima.Service;
//+
namespace Minima.Web.Control
{
    public class ArchivedEntryListBase : ListUserControlBase
    {
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            List<ArchiveCount> archiveList = BlogAgent.GetArchivedEntryList(MinimaConfiguration.BlogGuid);
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