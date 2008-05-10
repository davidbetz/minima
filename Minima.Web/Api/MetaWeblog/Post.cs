using System;
//+
namespace Minima.Web.Api.MetaWeblog
{
    [Serializable]
    public class Post
    {
        public String description;
        public String title;

        public String[] categories;
    }
}