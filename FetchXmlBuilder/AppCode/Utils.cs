using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Rappen.XTB.FetchXmlBuilder.AppCode
{
    public static class Utils
    {
        private static readonly NameValueCollection commonparams = new NameValueCollection { { "utm_source", "FetchXMLBuilder" }, { "utm_medium", "XrmToolBox" } };
        private static readonly NameValueCollection microsoftparams = new NameValueCollection { { "WT.mc_id", "DX-MVP-5002475" } };

        public static string ProcessURL(string url)
        {
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri _))
            {
                return url;
            }

            var uriBuilder = new UriBuilder(url);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (uriBuilder.Host.ToLowerInvariant().Contains("microsoft.com"))
            {
                microsoftparams.AllKeys.ToList().ForEach(k => queryString[k] = microsoftparams[k]);
                uriBuilder.Path = uriBuilder.Path.Replace("/en-us/", "/");
            }

            commonparams.AllKeys.ToList().ForEach(k => queryString[k] = commonparams[k]);

            uriBuilder.Query = queryString.ToString();

            return uriBuilder.Uri.ToString();
        }
    }
}