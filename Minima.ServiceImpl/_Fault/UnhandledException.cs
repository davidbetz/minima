#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
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