using System;
using System.Collections.Generic;
using System.Linq;
//+
using Minima.Service;
using Minima.Web.Agent;
//+
namespace Minima.Web.Control
{
    public class LabelListBase : ListUserControlBase
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
            List<Label> labelList = LabelAgent.GetBlogLabelList(this.BlogGuid);
            return labelList.Select(label => new
            {
                Title = label.Title,
                Url = LabelHelper.GetLabelUrl(label),
                EntryCount = label.BlogEntryCount
            });
        }
    }
}