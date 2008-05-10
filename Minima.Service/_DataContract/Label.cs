using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
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