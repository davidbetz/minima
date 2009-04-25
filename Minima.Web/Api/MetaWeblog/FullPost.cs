#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
namespace Minima.Web.Api.MetaWeblog
{
    public class FullPost
    {
        public DateTime dateCreated;

        public String description;
        public String title;

        public String[] categories;

        public String link;
        public String permalink;
        public String postid;

        public String mt_allow_comments;
        public Int32 mt_allow_pings;
        public String mt_convert_breaks;
        public String mt_text_mode;
        public String mt_excerpt;
        public String[] mt_tb_ping_urls;
    }
}