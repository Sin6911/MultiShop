using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

/*
 * Dang ky PublicKey va Private Key o day
 * https://www.google.com/recaptcha/admin#whyrecaptcha
 */
public static class XCaptcha
{
    private const String PrivateKey = "6LfvWuQSAAAAAM-ZL9QA2_XJ2xlDBv0HQufCnXfb";
    private const String PublicKey = "6LfvWuQSAAAAAKStMfJdM_CbuV5lOBtkhhUFAXR6";

    private const String VerifyUrl = "http://www.google.com/recaptcha/api/verify";
    private const String Challenge = "recaptcha_challenge_field";
    private const String Response = "recaptcha_response_field";

    public static MvcHtmlString Captcha(this HtmlHelper helper)
    {
        var tag = new TagBuilder("script");
        tag.Attributes["src"] = "http://www.google.com/recaptcha/api/challenge?k=" + PublicKey;
        return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
    }

    public static bool IsValid
    {
        get
        {
            using (var web = new WebClient())
            {
                var data = new NameValueCollection();
                data["privatekey"] = PrivateKey;
                data["remoteip"] = HttpContext.Current.Request.UserHostAddress;
                data["challenge"] = HttpContext.Current.Request[Challenge];
                data["response"] = HttpContext.Current.Request[Response];

                var response = web.UploadValues(VerifyUrl, "POST", data);
                var text = Encoding.ASCII.GetString(response);

                return text.Trim().ToLower().StartsWith("true");
            }
        }
    }
}