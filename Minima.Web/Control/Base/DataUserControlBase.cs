using System;
//+
namespace Minima.Web.Control
{
    public abstract class ListUserControlBase : System.Web.UI.UserControl
    {
        protected Object dataSource;

        //- @DataSource -//
        public Object DataSource
        {
            get
            {
                if (dataSource == null)
                {
                    dataSource = GetDataSource();
                }
                //+
                return dataSource;
            }
        }

        //- #GetDataSource -//
        protected abstract Object GetDataSource();
    }
}