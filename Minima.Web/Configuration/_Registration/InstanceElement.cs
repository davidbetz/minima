﻿using System;
using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class InstanceElement : ConfigurationElement
    {
        //- @WebSection -//
        [ConfigurationProperty("webSection", IsRequired = true, IsKey = true)]
        public String WebSection
        {
            get
            {
                return (String)this["webSection"];
            }
            set
            {
                this["webSection"] = value;
            }
        }

        //- @Page -//
        [ConfigurationProperty("page", IsRequired = true)]
        public String Page
        {
            get
            {
                return (String)this["page"];
            }
            set
            {
                this["page"] = value;
            }
        }

        //- @BlogGuid -//
        [ConfigurationProperty("blogGuid", IsRequired = true)]
        public String BlogGuid
        {
            get
            {
                return (String)this["blogGuid"];
            }
            set
            {
                this["blogGuid"] = value;
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