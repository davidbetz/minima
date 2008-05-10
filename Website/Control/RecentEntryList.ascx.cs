using System;
//+
public partial class RecentEntryList : Minima.Web.Control.RecentEntryListBase
{
    //- #Page_Load -//
    protected void Page_Load(Object sender, EventArgs e)
    {
        rptRecentEntryList.DataSource = this.DataSource;
        rptRecentEntryList.DataBind();
    }
}