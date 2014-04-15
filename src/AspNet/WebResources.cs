using System.Web.UI;
using Inthros.AspNet;

[assembly: WebResource(WebResourceNames.JsJQuery, "application/javascript")]
[assembly: WebResource(WebResourceNames.JsJQueryDebug, "application/javascript")]

[assembly: WebResource(WebResourceNames.CssFontAwesome, "text/css", PerformSubstitution = true)]
[assembly: WebResource(WebResourceNames.CssFontAwesomeDebug, "text/css", PerformSubstitution = true)]
[assembly: WebResource("fontawesome-webfont.eot", "font/eot")]
[assembly: WebResource("fontawesome-webfont.ttf", "font/ttf")]
[assembly: WebResource("fontawesome-webfont.woff", "font/woff")]
[assembly: WebResource("fontawesome-webfont.svg", "image/svg+xml")]

[assembly: WebResource(WebResourceNames.CssInthros, "text/css")]
[assembly: WebResource(WebResourceNames.JsInthros, "application/javascript")]

namespace Inthros.AspNet
{
    internal static class WebResourceNames
    {
        public const string JsJQuery = "jquery.js";

        public const string JsJQueryDebug = "jquery.debug.js";

        public const string JsInthros = "inthros.js";

        public const string CssFontAwesome = "font-awesome.min.css";

        public const string CssFontAwesomeDebug = "font-awesome.css";

        public const string CssInthros = "inthros.css";
    }
}
