using System;
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