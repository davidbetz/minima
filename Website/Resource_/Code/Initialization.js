Namespace.create('WCFClient.ICommentService');
//+
WCFClient.ICommentService.initCaptchaMath = function(onSuccess){
    Themelia.WCF.post({
        endpoint: '/Service_/Comment.svc/web',
        operation: 'InitCaptchaMath',
        message: { },
        onSuccess: onSuccess || (function( ){throw 'onSuccess is required for initCaptchaMath';})()
    });
};
WCFClient.ICommentService.postNewComment = function(captchaValue, blogEntryGuid, author, email, website, text, onSuccess){
    Themelia.WCF.post({
        endpoint: '/Service_/Comment.svc/web',
        operation: 'PostNewComment',
        message: {captchaValue: captchaValue, blogEntryGuid: blogEntryGuid, author: author, email: email, website: website, text: text},
        onSuccess: onSuccess || Prototype.emptyFunction
    });
};
//+
var Initialization = {
    init: function( ) {
        var btnSubmitComment = $('btnSubmitComment');
        if(btnSubmitComment) {
            Event.observe(btnSubmitComment, 'click', function( ) {
                var valid = true;
                if(txtCommentAuthorName.value.length < 1) {
                    rfvCommentAuthorName.style.display = 'inline';
                    valid = false;
                }
                
                if(txtCommentAuthorEmail.value.length < 1 && txtCommentAuthorEmail.value.indexOf('@') > -1 && txtCommentAuthorEmail.value.indexOf('.') > -1) {
                    cvCommentAuthorEmail.style.display = 'inline';
                    valid = false;
                }
                
                if(txtCommentText.value.length < 1) {
                    rfvCommentText.style.display = 'inline';
                    valid = false;
                }
                
                if($F('txtMath').length == 0){
                    valid = false;
                }
                
                if(valid==true) {
                    WCFClient.ICommentService.postNewComment($F('txtMath'), hfBlogEntryGuid.value, txtCommentAuthorName.value, txtCommentAuthorEmail.value, txtCommentWebsite.value, txtCommentText.value, function(r) {
                        switch(parseInt(r.PostNewCommentResult)) {
                            case 0:
                                $('commentInput').hide( );
                                $('commentInputCompleted').style.display = 'block';
                                //+
                                lblStatusMessage.update('<p>Comment saved.  All comments are moderated and may not show up for some time.</p>');
                                break;
                            case 1:
                                lblStatusMessage.update('<p>Err...  There may have been an error saving your comment.  The person who runs the site was sent a report describing this situation and s/he will quickly look into it.</p>');
                                break;
                            case 2:
                                lblStatusMessage.update('<p>Wrong math answer.</p>');
                                break;
                        }
                    });
                }
            });
        }
    }
};