/* based on the work by juval lowy in his book programming wcf from oreilly press*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
//+
namespace Minima.Service
{
    internal static class FaultCreator
    {
        //- ~Throw -//
        internal static void CreateFault(Type type, Exception error, MessageVersion version, ref Message fault)
        {
            if (error.GetType().IsGenericType && error is FaultException)
            {
                return;
            }
            if (IsExceptionInContract(type, error))
            {
                CreateFaultMessage(type, error, version, ref fault);
                //+
            }
            else
            {
                CreateFaultMessage(type, new UnhandledException(error.Message), version, ref fault);
            }
        }

        //- $CreateFaultMessage -//
        private static void CreateFaultMessage(Type type, Exception error, MessageVersion version, ref Message fault)
        {
            try
            {
                Type faultClosedType = typeof(FaultException<>).MakeGenericType(error.GetType());
                Exception exception = (Exception)Activator.CreateInstance(error.GetType(), error.Message);
                FaultException faultException = (FaultException)Activator.CreateInstance(faultClosedType, exception, error.Message);
                fault = Message.CreateMessage(version, faultException.CreateMessageFault(), faultException.Action);
            }
            catch
            {
            }
        }

        //- $IsExceptionInContract -//
        private static Boolean IsExceptionInContract(Type serviceType, Exception error)
        {
            List<FaultContractAttribute> faultAttributes = new List<FaultContractAttribute>();
            Type[] interfaces = serviceType.GetInterfaces();
            //+ get method name
            const String WCFPrefix = "SyncInvoke";
            int start = error.StackTrace.IndexOf(WCFPrefix);
            String trimedTillMethod = error.StackTrace.Substring(start + WCFPrefix.Length);
            String[] parts = trimedTillMethod.Split('(');
            String serviceMethod = parts[0];
            //+ find attribute
            FaultContractAttribute[] attributes;
            foreach (Type interfaceType in interfaces)
            {
                MethodInfo[] methods = interfaceType.GetMethods();
                foreach (MethodInfo methodInfo in methods)
                {
                    if (methodInfo.Name == serviceMethod)//Does not deal with overlaoded methods 
                    //or same method name on different contracts implemented explicitly 
                    {

                        attributes = (FaultContractAttribute[])methodInfo.GetCustomAttributes(typeof(FaultContractAttribute), false);
                        faultAttributes.AddRange(attributes);
                        //+
                        return faultAttributes.Any(p => p.DetailType == error.GetType());
                    }
                }
            }
            //+
            return false;
        }

    }
}