#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    public static class MessageHeaderHelper<T> where T : class
    {
        //- @AddMessageHeader -//
        public static void AddOutgoingMessageHeader(T messageHeader, String name, String ns)
        {
            MessageHeader<T> header = new MessageHeader<T>(messageHeader);
            OperationContext.Current.OutgoingMessageHeaders.Add(header.GetUntypedHeader(name, ns));
        }

        //- @GetMinimaMessageHeaderContent -//
        public static T GetAddOutgoingMessageHeader(String name, String ns)
        {
            OperationContext context = OperationContext.Current;
            if (context == null)
            {
                return null;
            }
            //+
            return context.IncomingMessageHeaders.GetHeader<T>(name, ns);
        }
    }
}