#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System.Web.UI;
//+
namespace Minima.Web.Controls
{
    public class MathCaptcha : CaptchaBase
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(@"
<p>Math Problem: <span id=""litMath""></span>(type the answer in the box)</p>
<div><input id=""txtMath"" /></div>
<script type=""text/javascript"">
document.observe('dom:loaded', function(evt) {
    Themelia.AspNet.registerObject('litMath');
    Themelia.AspNet.registerObject('txtMath');
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
</script>");
            //+
            base.Render(writer);
        }
    }
}