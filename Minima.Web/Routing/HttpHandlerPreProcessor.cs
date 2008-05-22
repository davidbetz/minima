using System.Linq;
using System.Web;
//+
using Minima.Web.Routing.Component;
//+
namespace Minima.Web.Routing
{
    public class HttpHandlerPreProcessor : General.Web.Routing.PreProcessorBase
    {
        //- @OnPreHttpHandlerExecute -//
        public override void OnPreHttpHandlerExecute(HttpContext context)
        {
            MinimaComponentSetting.MinimaInfo currentInfo = MinimaComponentSetting.CurrentComponentSetting.GetParameterList().FirstOrDefault(u => u.WebSection != null && General.Web.HttpWebSection.CurrentWebSection.ToLower().Contains(u.WebSection.ToLower()));
            if (currentInfo != null)
            {
                context.Items.Add("BlogGuid", currentInfo.BlogGuid);
            }
        }
    }
}