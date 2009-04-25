#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
//+
namespace Minima.Service
{
    public static class ServerCertificateValidator
    {
        //- @Validate -//
        public static Boolean Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //+ used to soften WCF's excessively uptight security
            return true;
        }
    }
}