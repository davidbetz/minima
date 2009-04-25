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
    public class Author : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @Name -//
        [DataMember]
        public String Name { get; set; }

        //- @Email -//
        [DataMember]
        public String Email { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}