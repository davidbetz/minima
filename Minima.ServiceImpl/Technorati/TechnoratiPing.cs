#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using CookComputing.XmlRpc;
//+
namespace Minima.Service.Technorati
{
    public static class TechnoratiNotifier
    {
        //- @Ping -//
        public static void Ping(String name, Uri uri)
        {
            ITechnoratiPing proxy = XmlRpcProxyGen.Create<ITechnoratiPing>();
            XmlRpcStruct call = new XmlRpcStruct();
            call.Add("name", name);
            call.Add("url", uri.AbsoluteUri);
            XmlRpcStruct response = proxy.Ping(call);
            //+
            if (response["flerror"].ToString() == "1")
            {
                throw new ApplicationException(response["message"].ToString());
            }
        }
    }
}