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