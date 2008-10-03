#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
    public class Tag : IMinimaEntity
    {
        //- @LabelGuid -//
        [DataMember]
        public String Guid { get; set; }

        //- @BlogEntryGuid -//
        [DataMember]
        public String BlogEntryGuid { get; set; }

        //- @City -//
        [DataMember]
        public String Title { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}