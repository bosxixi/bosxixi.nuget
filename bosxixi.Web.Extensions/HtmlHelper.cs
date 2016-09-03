using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bosxixi.Web.HtmlHelpers
{
    public static class HtmlHelperExtension
    {
        public static string CutString(this HtmlHelper helper, string source, int maxLength)
        {
            if (maxLength < 3)
                throw new ArgumentException();

            if (String.IsNullOrEmpty(source))
                return string.Empty;

            if (source.Length > maxLength)
                return source.Substring(0, maxLength - 3) + "...";

            return source;
        }

        public static MvcHtmlString MakeButton(this HtmlHelper helper, string content, string btnType, string title)
        {
            if (String.IsNullOrEmpty(content))
            {
                return null;
            }
            if (String.IsNullOrEmpty(btnType))
            {
                throw new ArgumentNullException(nameof(btnType));
            }

            string str = $@"<button title='{title}' class='btn btn-{btnType}'>{content}</button>" +
                       @"</button>";
            return new MvcHtmlString(str);
        }

        //Submit Button Helper
        public static MvcHtmlString Alert(this HtmlHelper helper, TempDataDictionary
        tempDataDictionary)
        {
            var viewData = tempDataDictionary.Where(c => c.Key == "success" || c.Key == "warning" ||
                           c.Key == "info" || c.Key == "danger");
            string alertType = string.Empty;
            string alertContent = string.Empty;
            foreach (var item in viewData)
            {
                if (item.Value != null)
                {
                    alertType = item.Key;
                    alertContent = item.Value.ToString();
                    break;
                }
            }

            if (String.IsNullOrEmpty(alertType) || String.IsNullOrEmpty(alertContent))
            {
                return null;
            }
            string str = $@"<div class='alert alert-{alertType} alert-dismissable'>" +
                               $"{alertContent}" +
                               $"<button type='button' class='close' data-dismiss='alert' aria-hidden='true' class='btn-alert-x'>&times;</button>" +
                          @"</div>";
            return new MvcHtmlString(str);
        }

        //Submit Button Helper
        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string
        buttonText)
        {
            string str = "<input type=\"submit\" value=\"" + buttonText + "\" />";
            return new MvcHtmlString(str);
        }
        //Readonly Strongly-Typed TextBox Helper
        public static MvcHtmlString TextBoxFor<TModel, TValue>(this
        HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>>
        expression, bool isReadonly)
        {
            MvcHtmlString html = default(MvcHtmlString);

            if (isReadonly)
            {
                html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper,
                expression, new
                {
                    @class = "readOnly",
                    @readonly = "read-only"
                });
            }
            else
            {
                html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper,
                expression);
            }
            return html;
        }

    }
}
