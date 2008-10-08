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
    public class ArchiveCount : IMinimaEntity
    {
        //- @ArchiveDate -//
        [DataMember]
        public DateTime ArchiveDate { get; set; }

        //- @Email -//
        [DataMember]
        public Int32 Count { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}