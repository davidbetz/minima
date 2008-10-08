#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Namespace.Minima)]
    public class BlogImage : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @Data -//
        [DataMember]
        public Byte[] Data { get; set; }

        //- @ContentType -//
        [DataMember]
        public String ContentType { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}