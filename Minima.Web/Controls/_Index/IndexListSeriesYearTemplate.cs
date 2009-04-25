#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
//+
namespace Minima.Web.Controls
{
    internal class IndexListSeriesYearTemplate : ITemplate
    {
        ListItemType type = new ListItemType();
        Boolean first = true;

        //+
        //- @Ctor -//
        public IndexListSeriesYearTemplate(ListItemType type)
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
                        literal.Text = " <ul class=\"index-year-list\">{ ";
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
                        Int32 year = Themelia.Parser.ParseInt32(item.DataItem);
                        String yearPath = String.Empty;
                        String webDomain = Themelia.Web.WebDomain.Name ?? String.Empty;
                        if (webDomain.ToLower() == "root")
                        {
                            webDomain = String.Empty;
                        }
                        else
                        {
                            webDomain = "/" + Themelia.Web.WebDomain.Path;
                        }
                        yearPath = webDomain + "/index/" + year.ToString();
                        //+
                        literal.Text += String.Format("<li>{2}<a href=\"{0}\">{1}</a></li>", yearPath, year, comma);
                        this.first = false;
                    });
                    break;

                case ListItemType.Footer:
                    lit.DataBinding += new EventHandler(delegate(Object sender, System.EventArgs ea)
                    {
                        System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)sender;
                        RepeaterItem item = (RepeaterItem)literal.NamingContainer;
                        literal.Text = " }</ul>";
                    });
                    break;

            }
            container.Controls.Add(lit);
        }
    }
}