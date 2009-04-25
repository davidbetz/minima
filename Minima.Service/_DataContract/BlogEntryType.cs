#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Namespace.Minima)]
    public class BlogEntryType : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @Name -//
        [DataMember]
        public String Name { get; set; }

        //- @Extra -//
        [DataMember]
        public String Extra { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}