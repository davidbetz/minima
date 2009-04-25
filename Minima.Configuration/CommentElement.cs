#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Configuration;
//+
namespace Minima.Configuration
{
    public class CommentElement : Themelia.Configuration.CommentableElement
    {
        //- @Subject -//
        [ConfigurationProperty("subject")]
        public String Subject
        {
            get
            {
                return (String)this["subject"];
            }
            set
            {
                this["subject"] = value;
            }
        }
    }
}