using System;
using System.Linq;
using System.Web;
//+
using Minima.Web.Routing.Component;
//+
namespace Minima.Web.Routing
{
    public class HttpHandlerPreProcessor : Themelia.Web.Routing.PreProcessorBase
    {
        //- @OnPreHttpHandlerExecute -//
        public override void OnPreHttpHandlerExecute(HttpContext context, params Object[] parameterArray)
        {
            MinimaComponentSetting.MinimaInfo currentInfo = MinimaComponentSetting.CurrentComponentSetting.GetParameterList().FirstOrDefault(u => u.WebSection != null && Themelia.Web.HttpWebSection.CurrentWebSection.ToLower().Contains(u.WebSection.ToLower()));
            if (currentInfo != null)
            {
                context.Items.Add("BlogGuid", currentInfo.BlogGuid);
            }
        }
    }
}