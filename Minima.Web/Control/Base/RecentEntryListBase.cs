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
    public abstract class RecentEntryListBase : MinimaListUserControlBase
    {
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            List<BlogEntry> blogEntryList = BlogAgent.GetBlogEntryList(this.BlogGuid, MinimaConfiguration.RecentEntriesToShow, false);
            //+
            return blogEntryList.Select(p => new
            {
                Url = p.BlogEntryUri.AbsoluteUri,
                Title = p.Title
            });
        }
    }
}