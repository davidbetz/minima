using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    internal static class FaultThrower
    {
        //- $Throw -//
        internal static void Throw<T>(T exception) where T : Exception
        {
            throw new FaultException<T>(exception, exception.Message);
        }
    }
}