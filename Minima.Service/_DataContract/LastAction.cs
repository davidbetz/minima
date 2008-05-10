using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
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