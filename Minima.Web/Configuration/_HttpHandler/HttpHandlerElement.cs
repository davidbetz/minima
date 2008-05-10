using System;
using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class HttpHandlerElement : ConfigurationElement
    {
        //- @MatchType -//
        [ConfigurationProperty("matchType", IsRequired = true)]
        public Char MatchType
        {
            get
            {
                return (Char)this["matchType"];
            }
            set
            {
                this["matchType"] = value;
            }
        }

        //- @Name -//
        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get
            {
                return (String)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        //- @MatchText -//
        [ConfigurationProperty("matchText", IsRequired = true, IsKey = true)]
        public String MatchText
        {
            get
            {
                return (String)this["matchText"];
            }
            set
            {
                this["matchText"] = value;
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