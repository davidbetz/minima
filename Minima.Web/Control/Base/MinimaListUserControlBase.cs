using System;
using Minima.Web.Routing;
//+
namespace Minima.Web.Control
{
    public abstract class MinimaListUserControlBase : General.Web.Control.DataUserControlBase
    {
        //- @BlogGuid -//
        public String BlogGuid
        {
            get
            {
                if (!String.IsNullOrEmpty(this.WebSection) && this.WebSection != ContextItemSet.WebSection)
                {
                    return WebSectionAccessor.GetBlogGuid(this.WebSection);
                }
                //+
                return ContextItemSet.BlogGuid;
            }
        }

        //- @WebSection -//
        public String WebSection { get; set; }
    }
}