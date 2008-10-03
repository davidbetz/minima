#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
using CookComputing.XmlRpc;
//+
namespace Minima.Service.Technorati
{
    [XmlRpcUrl("http://rpc.technorati.com/rpc/ping")]
    public interface ITechnoratiPing : IXmlRpcProxy
    {
        [XmlRpcMethod("weblogUpdates.ping")]
        XmlRpcStruct Ping(XmlRpcStruct call);
    }
}