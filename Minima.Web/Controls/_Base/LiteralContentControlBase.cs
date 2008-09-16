using System;
using System.Web.UI;
//+
namespace Minima.Web.Controls
{
    public abstract class LiteralContentControlBase : System.Web.UI.Control
    {
        //+
        //- @Content -//
        public String Content { get; set; }

        //+
        //- #Render -//
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write(this.Content);
        }
    }
}