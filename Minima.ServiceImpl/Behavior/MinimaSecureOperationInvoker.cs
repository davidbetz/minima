using System;
using System.Security;
using System.ServiceModel.Dispatcher;
//+
//+
namespace Minima.Service.Behavior
{
    public class MinimaSecureOperationInvoker : IOperationInvoker
    {
        //- $InnerOperationInvoker -//
        private IOperationInvoker InnerOperationInvoker { get; set; }

        //- @PermissionRequired -//
        public BlogPermission PermissionRequired { get; set; }

        //- @PermissionLevel -//
        public Char PermissionLevel { get; set; }

        //+
        //- @Ctor -//
        public MinimaSecureOperationInvoker(IOperationInvoker operationInvoker, Char permissionLevel, BlogPermission permissionRequired)
        {
            this.InnerOperationInvoker = operationInvoker;
            this.PermissionLevel = permissionLevel;
            this.PermissionRequired = permissionRequired;
        }

        //+
        //- @AllocateInputs -//
        public Object[] AllocateInputs()
        {
            return InnerOperationInvoker.AllocateInputs();
        }

        //- @Invoke -//
        public Object Invoke(Object instance, Object[] inputs, out Object[] outputs)
        {
            //+ authorization
            try
            {
                if (this.PermissionLevel == Minima.Service.PermissionLevel.Blog)
                {
                    String blogGuid = MinimaMessageHeaderHelper.GetBlogGuidFromMessageHeader();
                    SecurityValidator.ValidateBlogPermission(this.PermissionRequired, blogGuid);
                }
                else if (this.PermissionLevel == Minima.Service.PermissionLevel.System)
                {
                    SecurityValidator.ValidateSystemPermission(this.PermissionRequired);
                }
            }
            catch (SecurityException exception)
            {
                FaultThrower.Throw<SecurityException>(exception);
            }
            catch (ArgumentException exception)
            {
                FaultThrower.Throw<ArgumentException>(exception);
            }
            //+
            return InnerOperationInvoker.Invoke(instance, inputs, out outputs);
        }

        //- @InvokeBegin -//
        public IAsyncResult InvokeBegin(Object instance, Object[] inputs, AsyncCallback callback, Object state)
        {
            return InnerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        //- @InvokeEnd -//
        public Object InvokeEnd(Object instance, out Object[] outputs, IAsyncResult result)
        {
            return InnerOperationInvoker.InvokeEnd(instance, out outputs, result);
        }

        //- @IsSynchronous -//
        public bool IsSynchronous
        {
            get { return InnerOperationInvoker.IsSynchronous; }
        }
    }
}