using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//+
using Minima.Service;
//+
namespace Minima.Web.Control
{
    internal class IndexListSeriesTemplate : ITemplate
    {
        ListItemType type = new ListItemType();

        //+
        //- @Ctor -//
        public IndexListSeriesTemplate(ListItemType type)
        {
            this.type = type;
        }

        //+
        //- @InstantiateIn -//
        public void InstantiateIn(System.Web.UI.Control container)
        {
            PlaceHolder lit = new PlaceHolder();
            switch (type)
            {
                case ListItemType.Header:
                    lit.DataBinding += new EventHandler(delegate(Object sender, System.EventArgs ea)
                    {
                    });
                    break;

                case ListItemType.Item:
                    lit.DataBinding += new EventHandler(delegate(Object sender, System.EventArgs ea)
                    {
                        PlaceHolder literal = (PlaceHolder)sender;
                        RepeaterItem item = (RepeaterItem)literal.NamingContainer;
                        Repeater repeater = item.Parent as Repeater;
                        IndexListSeries indexListSeries = repeater.Parent as IndexListSeries;
                        if (indexListSeries != null)
                        {
                            Int32 month = (Int32?)DataBinder.Eval(item.DataItem, "Number") ?? 0;
                            String name = DataBinder.Eval(item.DataItem, "Name") as String;
                            if (month > 0)
                            {
                                Func<IndexEntry, Boolean> blogEntryListForMonth = be => be.PostDateTime.Month == month;
                                List<IndexEntry> blogEntryList = indexListSeries.BlogEntryDataSource.Where(blogEntryListForMonth).OrderBy(p=>p.PostDateTime).ToList();
                                if (blogEntryList.Count > 0)
                                {
                                    literal.Controls.Add(new IndexEntryList(AccessType.Index, blogEntryList)
                                    {
                                        Heading = name
                                    });
                                }
                            }
                        }
                    });
                    break;
            }
            container.Controls.Add(lit);
        }
    }
}