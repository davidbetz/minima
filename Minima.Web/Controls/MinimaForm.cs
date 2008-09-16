using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Minima.Web.Controls
{
    /// <summary>
    /// Used to allow postbacks when rewriting URLs
    /// Reference: http://msdn2.microsoft.com/en-us/library/ms972974.aspx
    /// </summary>
    public class MinimaForm : HtmlForm
    {
        //- #RenderAttributes -//
        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            writer.WriteAttribute("name", this.Name);
            base.Attributes.Remove("name");
            writer.WriteAttribute("method", this.Method);
            base.Attributes.Remove("method");
            this.Attributes.Render(writer);
            base.Attributes.Remove("action");
            if (base.ID != null)
            {
                writer.WriteAttribute("id", base.ClientID);
            }
        }
    }
}