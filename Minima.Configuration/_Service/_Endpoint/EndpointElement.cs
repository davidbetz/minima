#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Configuration;
//+
namespace Minima.Configuration
{
    public class EndpointElement : Themelia.Configuration.CommentableElement
    {
        //- @Author -//
        [ConfigurationProperty("author")]
        public String Author
        {
            get
            {
                return (String)this["author"];
            }
            set
            {
                this["author"] = value;
            }
        }

        //- @Blog -//
        [ConfigurationProperty("blog")]
        public String Blog
        {
            get
            {
                return (String)this["blog"];
            }
            set
            {
                this["blog"] = value;
            }
        }
        //- @Comment -//
        [ConfigurationProperty("comment")]
        public String Comment
        {
            get
            {
                return (String)this["comment"];
            }
            set
            {
                this["comment"] = value;
            }
        }
        //- @Image -//
        [ConfigurationProperty("image")]
        public String Image
        {
            get
            {
                return (String)this["image"];
            }
            set
            {
                this["image"] = value;
            }
        }
        //- @Label -//
        [ConfigurationProperty("label")]
        public String Label
        {
            get
            {
                return (String)this["label"];
            }
            set
            {
                this["label"] = value;
            }
        }
    }
}