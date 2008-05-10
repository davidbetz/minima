using System;
using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class UrlRewriteElement : ConfigurationElement
    {
        //- @Match -//
        [ConfigurationProperty("match", IsRequired = true, IsKey = true)]
        public String Match
        {
            get
            {
                return (String)this["match"];
            }
            set
            {
                this["match"] = value;
            }
        }

        //- @Source -//
        [ConfigurationProperty("source", IsRequired = true)]
        public String Source
        {
            get
            {
                return (String)this["source"];
            }
            set
            {
                this["source"] = value;
            }
        }

        //- @Target true
        [ConfigurationProperty("target", IsRequired = true)]
        public String Target
        {
            get
            {
                return (String)this["target"];
            }
            set
            {
                this["target"] = value;
            }
        }

        //- @Priority -//
        [ConfigurationProperty("priority", DefaultValue = 5, IsRequired = false)]
        public Int32 Priority
        {
            get
            {
                return (Int32)this["priority"];
            }
            set
            {
                this["priority"] = value;
            }
        }
    }
}