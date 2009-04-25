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
    public class Comment : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @BlogEntryGuid -//
        [DataMember]
        public String BlogEntryGuid { get; set; }

        //- @Name -//
        [DataMember]
        public String Name { get; set; }

        //- @Email -//
        [DataMember]
        public String Email { get; set; }

        //- @Website -//
        [DataMember]
        public String Website { get; set; }

        //- @Text -//
        [DataMember]
        public String Text { get; set; }

        //- @IsModerated -//
        [DataMember]
        public Boolean IsModerated { get; set; }

        //- @DateTime -//
        [DataMember]
        public DateTime DateTime { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}