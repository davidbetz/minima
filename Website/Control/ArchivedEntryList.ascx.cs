using System;
//+
public partial class ArchivedEntryList : Minima.Web.Control.ArchivedEntryListBase
{
    //- #Page_Load -//
    protected void Page_Load(Object sender, EventArgs e)
    {
        rptArchivedEntryList.DataSource = this.DataSource;
        rptArchivedEntryList.DataBind();
    }
}