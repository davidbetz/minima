#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Runtime.Serialization;
//+
namespace Minima.Web.Service
{
    [DataContract]
    public class CaptchMathInformation
    {
        //- @A -//
        [DataMember]
        public Int32 A { get; set; }

        //- @B -//
        [DataMember]
        public Int32 B { get; set; }
    }
}