using System;
using System.ServiceModel;
using Minima.Service;
using System.ServiceModel.Web;
//+
namespace Minima.Web.Service
{
    [ServiceContract(Namespace = Information.NamespaceRoot)]
    public interface ICommentService
    {
        //- InitCaptchaMath -//
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        CaptchMathInformation InitCaptchaMath();

        //- AuthorizeComment -//
        [OperationContract]
        [WebGet(UriTemplate = "AuthorizeComment/{commentGuid}")]
        String AuthorizeComment(String commentGuid);

        //- PostNewComment -//
        [OperationContract]
        [WebInvoke(BodyStyle=WebMessageBodyStyle.Wrapped,
            ResponseFormat=WebMessageFormat.Json)]
        Int32 PostNewComment(Int32 captchaValue, String blogEntryGuid, String author, String email, String website, String text);
    }
}