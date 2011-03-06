using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Configuration;

namespace CdnManagement
{
    public static class Factory
    {
        private static string StyleInternal(this HtmlHelper htmlHelper, string appSettingKey)
        {
            string url = ConfigurationManager.AppSettings[appSettingKey];
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(string.Format("CdnManagement Error: Key - {0} is not found.", appSettingKey));
            }
            TagBuilder tag = new TagBuilder("link");
            tag.MergeAttribute("type", "text/css");
            tag.MergeAttribute("rel", "stylesheet");
            tag.MergeAttribute("href", url);
            return tag.ToString(TagRenderMode.Normal);
        }
        private static string ScriptInternal(this HtmlHelper htmlHelper, string appSettingKey)
        {
            string url = ConfigurationManager.AppSettings[appSettingKey];
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(string.Format("CdnManagement Error: Key - {0} is not found.", appSettingKey));
            }
            TagBuilder tag = new TagBuilder("script");
            tag.MergeAttribute("type", "text/javascript");
            tag.MergeAttribute("src", url);
            return tag.ToString(TagRenderMode.Normal);
        }
        private static string ResourceInternal(this HtmlHelper htmlHelper, string appSettingKey)
        {
            string url = ConfigurationManager.AppSettings[appSettingKey];
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(string.Format("CdnManagement Error: Key - {0} is not found.", appSettingKey));
            }
            if (url.ToLowerInvariant().EndsWith(".js"))
            {
                return ScriptInternal(htmlHelper, appSettingKey);
            }
            else if (url.ToLowerInvariant().EndsWith(".css"))
            {
                return StyleInternal(htmlHelper, appSettingKey);
            }
            throw new Exception(string.Format("CdnManagement Error: Resource url - {0} does not ends with \".js\" or \".css\"", url));
        }
        private static string HasValidRoutes(HtmlHelper htmlHelper)
        {
            string controller = htmlHelper.ViewContext.RouteData.Values["controller"] as string;
            string action = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                throw new Exception("CdnManagement Error: This feature is only supported for routes having controller and action attributes in their routes.");
            }
            return controller + "." + action;
        }

        public static MvcHtmlString Style(this HtmlHelper htmlHelper, string appSettingKey)
        {
            return MvcHtmlString.Create(StyleInternal(htmlHelper,appSettingKey));
        }
        public static MvcHtmlString Script(this HtmlHelper htmlHelper, string appSettingKey)
        {

            return MvcHtmlString.Create(ScriptInternal(htmlHelper, appSettingKey));
        }
        public static MvcHtmlString Resource(this HtmlHelper htmlHelper, string appSettingKey)
        {
            return MvcHtmlString.Create(ResourceInternal(htmlHelper, appSettingKey));
        }
        public static MvcHtmlString LoadResources(this HtmlHelper htmlHelper)
        {
            string keyprefix = HasValidRoutes(htmlHelper);
            List<string> keys = (from k in ConfigurationManager.AppSettings.AllKeys
                                 where k.ToLowerInvariant().StartsWith(keyprefix)
                                 select k).ToList<string>();

            StringBuilder sb = new StringBuilder();
            foreach (string key in keys)
            {
                sb.AppendLine(ResourceInternal(htmlHelper,key));
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        public static MvcHtmlString LoadMasterResources(this HtmlHelper htmlHelper, string masterName)
        {
            HasValidRoutes(htmlHelper);
            string keyprefix ="master"+"."+ masterName + ".";
            List<string> keys = (from k in ConfigurationManager.AppSettings.AllKeys
                                 where k.ToLowerInvariant().StartsWith(keyprefix)
                                 select k).ToList<string>();

            StringBuilder sb = new StringBuilder();
            foreach (string key in keys)
            {
                sb.AppendLine(ResourceInternal(htmlHelper, key));
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
