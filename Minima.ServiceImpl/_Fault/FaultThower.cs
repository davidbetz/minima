#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    public static class FaultThrower
    {
        //- ~Throw -//
        public static void Throw<T>(T exception) where T : Exception
        {
            throw new FaultException<T>(exception, exception.Message);
        }
    }
}