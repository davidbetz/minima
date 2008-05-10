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

        //- @Ctor -//
        public UnhandledException(string message) : base(message) { }

        //- @Ctor -//
        public UnhandledException(string message, Exception inner) : base(message, inner) { }

        //- @Ctor -//
        protected UnhandledException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}