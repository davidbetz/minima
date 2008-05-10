using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [Serializable]
    public class UnhandledException : Exception
    {
        //- @Ctor -//
        public UnhandledException() { }
        public UnhandledException(string message) : base(message) { }
        public UnhandledException(string message, Exception inner) : base(message, inner) { }
        protected UnhandledException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}