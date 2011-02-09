using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Text;

namespace Mvc3.CdnManagement
{
    /// <summary>
    /// Resource Type Script or Style or other
    /// </summary>
    public enum ResourceType
    { 
        /// <summary>
        /// Use for loading script
        /// </summary>
        Script,
        /// <summary>
        /// Use for loading style
        /// </summary>
        Style
    }
    public static class CdnFactory
    {
        /// <summary>
        /// Prefix of cdn url
        /// </summary>
        public static string CdnUrl { get; set; }
        static CdnFactory()
        {
            if (string.IsNullOrEmpty(CdnUrl))
            {
                CdnUrl = ConfigurationManager.AppSettings["CdnUrl"].TrimEnd('/');
            }
        }
        /// <summary>
        /// Relative path of resource, it uses key "CdnUrl"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="relativePath"></param>
        public static void StyleLink<T>(this HtmlHelper<T> htmlHelper, string relativePath)
        {
            CdnUrl = CdnUrl.TrimEnd('/');
            relativePath = relativePath.TrimStart('/');
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<link href=\"{0}/{1}\" rel=\"stylesheet\" type=\"text/css\"></link>", CdnUrl,relativePath);
            htmlHelper.ViewContext.Writer.WriteLine(sb.ToString());
        }
        /// <summary>
        /// Relative path of script, it uses key "CdnUrl"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="relativePath"></param>
        public static void ScriptLink<T>(this HtmlHelper<T> htmlHelper, string relativePath)
        {
            CdnUrl = CdnUrl.TrimEnd('/');
            relativePath = relativePath.TrimStart('/');
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<script src=\"{0}/{1}\" type=\"text/javascript\"></script>", CdnUrl, relativePath);
            htmlHelper.ViewContext.Writer.WriteLine(sb.ToString());
        }
        /// <summary>
        /// Relative resource, it uses key "CdnUrl"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="resourceType">script or style or other</param>
        /// <param name="configKey">configuration key value, configured in appsettings</param>
        public static void Resource<T>(this HtmlHelper<T> htmlHelper,ResourceType resourceType, string configKey)
        {
            var url = ConfigurationManager.AppSettings[configKey];
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("Mvc3.ContentManagement: Resource not found.");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                switch (resourceType)
                {
                    case ResourceType.Script:
                        sb.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>", url);
                        break;
                    case ResourceType.Style:
                        sb.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\"></link>", url);
                        break;
                }
                htmlHelper.ViewContext.Writer.WriteLine(sb.ToString());
            }
        }
    }
}