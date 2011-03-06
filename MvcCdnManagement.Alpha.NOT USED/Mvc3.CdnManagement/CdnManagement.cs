using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace CdnManagement
{
    public static class CdnManagement
    {

        public static string CdnUrl { get; set; }
        static CdnManagement()
        {
            CdnUrl = ConfigurationManager.AppSettings["CdnUrl"].TrimEnd('/');
        }

        public enum ApplicationStyleSheet
        {
            [FileName("/Content/2010.3.1318/telerik.vista.min.css")]
            Telerik_Vista
        }
        public enum ApplicationScript
        {
            [FileName("/Scripts/MicrosoftMvcValidation.js")]
            MicrosoftMvcValidation
        }
        [AttributeUsage(AttributeTargets.Field)]
        public class FileNameAttribute : Attribute
        {
            public string FileName { get; set; }
            public FileNameAttribute(string name)
            {
                this.FileName = name;
            }
        }
        public static string ToFileName(this ApplicationStyleSheet val)
        {
            var attributes = (FileNameAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(FileNameAttribute), false);
            return attributes.Length > 0 ? attributes[0].FileName : string.Empty;
        }
        public static string ToFileName(this ApplicationScript val)
        {
            var attributes = (FileNameAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(FileNameAttribute), false);
            return attributes.Length > 0 ? attributes[0].FileName : string.Empty;
        }
        public static void StyleLink<T>(this HtmlHelper<T> htmlHelper, ApplicationStyleSheet fileName)
        {
            string url = CdnUrl + "/" + fileName.ToFileName().TrimStart('/');
            TagBuilder style = new TagBuilder("link");
            style.MergeAttribute("href", url);
            style.MergeAttribute("type", "text/css");
            style.MergeAttribute("rel", "stylesheet");
            htmlHelper.ViewContext.Writer.WriteLine(style.ToString());
        }
        public static void StyleLink<T>(this HtmlHelper<T> htmlHelper, string fileName)
        {
            string url = CdnUrl + "/" + fileName.TrimStart('/');
            TagBuilder style = new TagBuilder("link");
            style.MergeAttribute("href", url);
            style.MergeAttribute("type", "text/css");
            style.MergeAttribute("rel", "stylesheet");
            htmlHelper.ViewContext.Writer.WriteLine(style.ToString());
        }
        public static void ScriptLink<T>(this HtmlHelper<T> htmlHelper, ApplicationScript fileName)
        {
            string url = CdnUrl + "/" + fileName.ToFileName().TrimStart('/');
            TagBuilder script = new TagBuilder("script");
            script.MergeAttribute("src", url);
            script.MergeAttribute("type", "text/javascript");
            htmlHelper.ViewContext.Writer.WriteLine(script.ToString());
        }
        public static void ScriptLink<T>(this HtmlHelper<T> htmlHelper, string fileName)
        {
            string url = CdnUrl + "/" + fileName.TrimStart('/');
            TagBuilder script = new TagBuilder("script");
            script.MergeAttribute("src", url);
            script.MergeAttribute("type", "text/javascript");
            htmlHelper.ViewContext.Writer.WriteLine(script.ToString());
        }
    }
}