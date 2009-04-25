#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Configuration;
//+
namespace Minima.Configuration
{
    public class SuffixElement : Themelia.Configuration.CommentableElement
    {
        //- @Index -//
        [ConfigurationProperty("index")]
        public String Index
        {
            get
            {
                return (String)this["index"];
            }
            set
            {
                this["index"] = value;
            }
        }

        //- @Archive -//
        [ConfigurationProperty("archive")]
        public String Archive
        {
            get
            {
                return (String)this["archive"];
            }
            set
            {
                this["archive"] = value;
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