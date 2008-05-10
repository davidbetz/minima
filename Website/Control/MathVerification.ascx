<%@ Control Language="C#" AutoEventWireup="false" CodeFile="MathVerification.ascx.cs" Inherits="Controls_MathVerification" %>
<p>Math Problem: <span id="litMath"></span>(type the answer in the box)</p>
<div><input id="txtMath" /></div>
<script type="text/javascript">
document.observe('dom:loaded', function(evt) {
    Prominax.AspNet.registerObject('litMath');
    Prominax.AspNet.registerObject('txtMath');
    //+
    txtMath.value = '';
    //+
    WCFClient.ICommentService.initCaptchaMath(function(r) {
        var text = '#{A} + #{B} '.interpolate({
            A: r.InitCaptchaMathResult.A,
            B: r.InitCaptchaMathResult.B
        });
        litMath.update(text);
    });
}, false);
</script>