#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System.Configuration;
//+
namespace Minima.Configuration
{
    public class ServiceElement : Themelia.Configuration.CommentableElement
    {
        //- @Authentication -//
        [ConfigurationProperty("authentication")]
        public AuthenticationElement Authentication
        {
            get
            {
                return (AuthenticationElement)this["authentication"];
            }
            set
            {
                this["authentication"] = value;
            }
        }

        //- @Endpoint -//
        [ConfigurationProperty("endpoint")]
        public EndpointElement Endpoint
        {
            get
            {
                return (EndpointElement)this["endpoint"];
            }
            set
            {
                this["endpoint"] = value;
            }
        }
    }
}