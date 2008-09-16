using System;
using System.Web.UI;
using System.Web.UI.WebControls;
//+
//+
namespace Minima.Web.Controls
{
    internal class IndexLabelListTemplate : ITemplate
    {
        ListItemType type = new ListItemType();
        Boolean first = true;

        //+
        //- @Ctor -//
        public IndexLabelListTemplate(ListItemType type)
        {
            this.type = type;
        }

        //+
        //- @InstantiateIn -//
        public void InstantiateIn(System.Web.UI.Control container)
        {
            System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
            //+
            switch (type)
            {
                case ListItemType.Header:
                    lit.DataBinding += new EventHandler(delegate(Object sender, System.EventArgs ea)
                    {
                        System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)sender;
                        RepeaterItem item = (RepeaterItem)literal.NamingContainer;
                        literal.Text = " <span class=\"index-label-list\">{ ";
                    });
                    break;

                case ListItemType.Item:
                    lit.DataBinding += new EventHandler(delegate(Object sender, System.EventArgs ea)
                    {
                        System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)sender;
                        String comma = String.Empty;
                        if (!this.first)
                        {
                            comma = ", ";
                        }
                        RepeaterItem item = (RepeaterItem)literal.NamingContainer;
                        String url = DataBinder.Eval(item.DataItem, "Url") as String;
                        String title = DataBinder.Eval(item.DataItem, "Title") as String;
                        //+
                        literal.Text += String.Format("{2}<a href=\"{0}\">{1}</a>", url, title, comma);
                        this.first = false;
                    });
                    break;

                case ListItemType.Footer:
                    lit.DataBinding += new EventHandler(delegate(Object sender, System.EventArgs ea)
                    {
                        System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)sender;
                        RepeaterItem item = (RepeaterItem)literal.NamingContainer;
                        literal.Text = " }</span>";
                    });
                    break;

            }
            container.Controls.Add(lit);
        }
    }
}