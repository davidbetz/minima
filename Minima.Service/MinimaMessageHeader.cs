#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract]
    public class MinimaMessageHeader
    {
        [DataMember]
        public MinimaMessageHeaderType HeaderType { get; set; }

        [DataMember]
        public String Content { get; set; }
    }
}