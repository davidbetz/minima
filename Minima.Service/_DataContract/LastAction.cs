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
    public class LastAction
    {
        //- @Code -//
        [DataMember]
        public Int32 Code { get; set; }

        //- @Message -//
        [DataMember]
        public String Message { get; set; }
    }
}