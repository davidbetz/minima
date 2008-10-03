#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
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