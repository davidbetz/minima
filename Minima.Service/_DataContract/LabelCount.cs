using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
    public class LabelCount : IMinimaEntity
    {
        //- @Label -//
        [DataMember]
        public Label Label { get; set; }

        //- @Count -//
        [DataMember]
        public Int32 Count { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}