using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
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