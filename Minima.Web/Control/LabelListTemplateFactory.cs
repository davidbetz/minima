using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//+
namespace Minima.Web.Control
{
    public static class LabelListTemplateFactory
    {
        //- ~TemplateType -//
        internal enum TemplateType
        {
            Linear,
            Sized
        }
        //+
        //- @LinearTemplate -//
        public class LinearTemplate : ITemplate
        {
            private ListItemType type = ListItemType.Item;
            private String listCssClass = String.Empty;

            //- @Ctor -//
            public LinearTemplate(params Object[] parameterArray)
            {
                type = ((ListItemType)parameterArray[0]);
                listCssClass = ((String)parameterArray[1]);
            }

            //- @InstantiateIn -//
            public void InstantiateIn(System.Web.UI.Control container)
            {
                System.Web.UI.WebControls.PlaceHolder pane = new System.Web.UI.WebControls.PlaceHolder();
                switch (type)
                {
                    case ListItemType.Header:
                        pane.Controls.Add(new System.Web.UI.WebControls.Literal { Text = "<ul id=\"" + listCssClass + "\">" });
                        break;

                    case ListItemType.Item:
                        System.Web.UI.WebControls.Literal literal = new System.Web.UI.WebControls.Literal();
                        literal.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                        {
                            IDataItemContainer item = (IDataItemContainer)container;
                            String url = DataBinder.Eval(item.DataItem, "Url").ToString();
                            String title = DataBinder.Eval(item.DataItem, "Title").ToString();
                            String entryCount = DataBinder.Eval(item.DataItem, "EntryCount").ToString();
                            //+
                            literal.Text = @"<li><a href=""{Url}"">{Title} ({EntryCount})</a></li>"
                                .Replace("{Url}", url)
                                .Replace("{Title}", title)
                                .Replace("{EntryCount}", entryCount);
                        });
                        pane.Controls.Add(literal);
                        break;

                    case ListItemType.Footer:
                        pane.Controls.Add(new System.Web.UI.WebControls.Literal { Text = "</ul>" });
                        break;
                }
                //+
                container.Controls.Add(pane);
            }
        }
        //+
        //- @SizedLabelTemplate -//
        public class SizedTemplate : ITemplate
        {
            private ListItemType type = ListItemType.Item;
            private String listCssClass = String.Empty;
            Int32 max;
            Int32 largest = 18;

            //- @Ctor -//
            public SizedTemplate(params Object[] parameterArray)
            {
                type = ((ListItemType)parameterArray[0]);
                listCssClass = ((String)parameterArray[1]);
                //+
                List<Minima.Service.Label> labelList = (List<Minima.Service.Label>)parameterArray[2];
                max = labelList.Max(p => p.BlogEntryCount);
            }

            //- @InstantiateIn -//
            public void InstantiateIn(System.Web.UI.Control container)
            {
                System.Web.UI.WebControls.PlaceHolder pane = new System.Web.UI.WebControls.PlaceHolder();
                switch (type)
                {
                    case ListItemType.Header:
                        pane.Controls.Add(new System.Web.UI.WebControls.Literal { Text = "<div id=\"" + listCssClass + "\">" });
                        break;

                    case ListItemType.Item:
                        System.Web.UI.WebControls.Literal literal = new System.Web.UI.WebControls.Literal();
                        literal.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                        {
                            IDataItemContainer item = (IDataItemContainer)container;
                            String url = DataBinder.Eval(item.DataItem, "Url").ToString();
                            String title = DataBinder.Eval(item.DataItem, "Title").ToString();
                            Int32 entryCountInt32 = (DataBinder.Eval(item.DataItem, "EntryCount") as Int32?) ?? 0;
                            String entryCount = entryCountInt32.ToString();
                            String fontStyle = String.Format("font-size:{0}pt", (Double)(entryCountInt32 * largest) / max < 7 ? 7 : (entryCountInt32 * largest / max));
                            //+
                            literal.Text = @"<span style=""{FontStyle}""><a href=""{Url}"">{Title}</a></span>"
                                .Replace("{Url}", url)
                                .Replace("{Title}", title + " ")
                                .Replace("{EntryCount}", entryCount)
                                .Replace("{FontStyle}", fontStyle);
                        });
                        container.Controls.Add(literal);
                        pane.Controls.Add(literal);
                        break;

                    case ListItemType.Footer:
                        pane.Controls.Add(new System.Web.UI.WebControls.Literal { Text = "</div>" });
                        break;
                }
                //+
                container.Controls.Add(pane);
            }
        }

        //- ~CreateTemplate -//
        internal static ITemplate CreateTemplate(TemplateType templateType, params Object[] parameterArray)
        {
            switch (templateType)
            {
                case TemplateType.Linear:
                    return new LinearTemplate(parameterArray);
                case TemplateType.Sized:
                    return new SizedTemplate(parameterArray);
            }
            //+
            return null;
        }
    }
}