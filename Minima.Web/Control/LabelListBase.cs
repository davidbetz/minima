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
    public class LabelListBase : ListUserControlBase
    {
        //- #GetDataSource -//
        protected override Object GetDataSource()
        {
            List<Label> labelList = LabelAgent.GetBlogLabelList(MinimaConfiguration.BlogGuid);
            return labelList.Select(label => new
            {
                Title = label.Title,
                Url = GetLabelUrl(label),
                EntryCount = label.BlogEntryCount
            });
        }

        //- $GetLabelUrl -//
        private String GetLabelUrl(Label label)
        {
            return WebConfiguration.Domain + "/label/" + (!String.IsNullOrEmpty(label.FriendlyTitle) ? label.FriendlyTitle : label.Title).ToLower();
        }
    }
}