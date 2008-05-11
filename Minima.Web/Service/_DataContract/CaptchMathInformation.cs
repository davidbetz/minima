using System;
using System.Runtime.Serialization;
//+
namespace Minima.Web.Service
{
    [DataContract]
    public class CaptchMathInformation
    {
        //- @A -//
        [DataMember]
        public Int32 A { get; set; }

        //- @B -//
        [DataMember]
        public Int32 B { get; set; }
    }
}