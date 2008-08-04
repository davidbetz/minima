using System;
using System.Web.UI;
using System.Web.UI.WebControls;
//+
using Minima.Web.Helper;
using System.Collections.Generic;
using Minima.Service;
//+
namespace Minima.Web.Control
{
    internal class IndexEntryTemplate : ITemplate
    {
        ListItemType type = new ListItemType();
        System.Web.HttpContext httpContext;

        public IndexEntryTemplate(ListItemType type)
        {
            this.type = type;
            //+
            httpContext = System.Web.HttpContext.Current;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            System.Web.UI.WebControls.PlaceHolder pane = new System.Web.UI.WebControls.PlaceHolder();
            switch (type)
            {
                case ListItemType.Item:
                    pane.DataBinding += new EventHandler(delegate(Object sender, System.EventArgs ea)
                    {
                        RepeaterItem item = (RepeaterItem)pane.NamingContainer;
                        //+
                        System.Web.UI.WebControls.Literal dtHeader = new System.Web.UI.WebControls.Literal();
                        dtHeader.Text = "<dl class=\"index-section-list\"><dt>";
                        pane.Controls.Add(dtHeader);
                        //+
                        System.Web.UI.WebControls.Literal image = new System.Web.UI.WebControls.Literal();
                        Themelia.Template template = new Themelia.Template(Themelia.Template.Common.Image);
                        Themelia.Map map = new Themelia.Map();
                        BlogEntryType blogEntryType = FindBlogEntryType((String)DataBinder.Eval(item.DataItem, "TypeGuid"));
                        if (blogEntryType != null && !String.IsNullOrEmpty(blogEntryType.Extra))
                        {
                            map.Add("Source", blogEntryType.Extra);
                            map.Add("Text", blogEntryType.Name);
                            image.Text = template.Interpolate(map);
                            pane.Controls.Add(image);
                        }
                        //+
                        System.Web.UI.WebControls.Literal dtddConnection = new System.Web.UI.WebControls.Literal();
                        dtddConnection.Text = "</dt><dd>";
                        pane.Controls.Add(dtddConnection);
                        //+
                        System.Web.UI.WebControls.Literal link = new System.Web.UI.WebControls.Literal();
                        template = new Themelia.Template(Themelia.Template.Common.Link);
                        map = new Themelia.Map();
                        map.Add("Link", (String)DataBinder.Eval(item.DataItem, "Url"));
                        map.Add("Text", (String)DataBinder.Eval(item.DataItem, "Title"));
                        link.Text = template.Interpolate(map);
                        pane.Controls.Add(link);
                        //+
                        System.Web.UI.WebControls.Literal ddFooter = new System.Web.UI.WebControls.Literal();
                        ddFooter.Text = "</dd></dl>";
                        pane.Controls.Add(ddFooter);
                    });
                    break;

            }
            container.Controls.Add(pane);
        }

        //+
        //- $FindBlogEntryType -//
        private BlogEntryType FindBlogEntryType(String blogEntryTypeGuid)
        {
            String indexScope = "MinimaIndexImage";
            //+
            BlogEntryType blogEntryType = Themelia.Web.HttpData.GetScopedCacheItem<BlogEntryType>(indexScope, blogEntryTypeGuid);
            if (blogEntryType == null)
            {
                List<String> guidList = new List<String>(new String[] { blogEntryTypeGuid });
                String blogGuid = Themelia.Web.HttpData.GetScopedCacheItem<String>("Minima", "BlogGuid");
                List<BlogEntryType> blogEntryTypeList = Minima.Web.Agent.BlogAgent.GetBlogEntryTypeList(blogGuid, guidList);
                if (blogEntryTypeList != null && blogEntryTypeList.Count > 0)
                {
                    blogEntryType = blogEntryTypeList[0];
                    Themelia.Web.HttpData.SetScopedCacheItem<BlogEntryType>(indexScope, blogEntryTypeGuid, blogEntryType);
                }
            }
            //+
            return blogEntryType;
        }
    }
}