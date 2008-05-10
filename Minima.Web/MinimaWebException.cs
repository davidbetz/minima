using System;
//+
using General.ExceptionHandling;
//+
namespace Minima
{
    [Serializable]
    public class MinimaWebException : Exception
    {
        public MinimaWebException()
        {
            ExceptionManager.Report(this.GetType().ToString(), this);
        }
        public MinimaWebException(string message)
            : base(message)
        {
            ExceptionManager.Report(this.GetType().ToString(), this);
        }
        public MinimaWebException(string message, Exception inner)
            : base(message, inner)
        {
            ExceptionManager.Report(this.GetType().ToString(), this);
        }
        protected MinimaWebException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            ExceptionManager.Report(this.GetType().ToString(), this);
        }
    }
}