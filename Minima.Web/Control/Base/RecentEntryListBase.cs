using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Web.Agent;
//+
namespace Minima.Web.Control
{
    public class RecentEntryListBase : ListUserControlBase
    {
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(MinimaConfiguration.BlogGuid, MinimaConfiguration.RecentEntriesToShow, false);
            //+
            return blogEntryList.Select(p => new
            {
                Url = p.BlogEntryUri.AbsoluteUri,
                Title = p.Title
            });
        }
    }
}