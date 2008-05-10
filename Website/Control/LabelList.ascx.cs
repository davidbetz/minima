using System;
//+
public partial class LabelList : Minima.Web.Control.LabelListBase
{
    //- #Page_Load -//
    protected void Page_Load(Object sender, EventArgs e)
    {
        rptLabelList.DataSource = this.DataSource;
        rptLabelList.DataBind();
    }
}