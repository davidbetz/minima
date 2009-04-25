#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Configuration;
//+
namespace Minima.Configuration
{
    public class DisplayElement : Themelia.Configuration.CommentableElement
    {
        //- @LinkAuthorsToEmail -//
        [ConfigurationProperty("linkAuthorsToEmail")]
        public Boolean LinkAuthorsToEmail
        {
            get
            {
                return (Boolean)this["linkAuthorsToEmail"];
            }
            set
            {
                this["linkAuthorsToEmail"] = value;
            }
        }

        //- @BlankMessage -//
        [ConfigurationProperty("blankMessage")]
        public String BlankMessage
        {
            get
            {
                return (String)this["blankMessage"];
            }
            set
            {
                this["blankMessage"] = value;
            }
        }
    }
}