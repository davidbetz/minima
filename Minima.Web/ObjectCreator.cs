using System;
//+
namespace Minima.Activation
{
    public static class ObjectCreator
    {
        //- @Create -//
        public static Object Create<T>(Type type, params Object[] paramterArray)
        {
            return Activator.CreateInstance(type, paramterArray);
        }

        //- @CreateAs -//
        public static T CreateAs<T>(Type type, params Object[] paramterArray) where T : class
        {
            return Activator.CreateInstance(type, paramterArray) as T;
        }
    }
}