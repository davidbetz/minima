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
    public class Label : IMinimaEntity
    {
        //- @LabelGuid -//
        [DataMember]
        public String Guid { get; set; }

        //- @BlogGuid -//
        [DataMember]
        public String BlogGuid { get; set; }

        //- @City -//
        [DataMember]
        public String Title { get; set; }

        //- @State -//
        [DataMember]
        public String FriendlyTitle { get; set; }

        //- @BlogEntryCount -//
        [DataMember]
        public Int32 BlogEntryCount { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}