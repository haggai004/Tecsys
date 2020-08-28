using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Tecsys.Retail.MvcWeb.Views
{
    public static class HtmlHelpersExtension
    {
        public static MvcHtmlString LabelWithColonFor<TModel, TValue>(this HtmlHelper<TModel> helper,Expression<Func<TModel, TValue>> expression)
        {
            return helper.LabelFor(expression, string.Format("{0}:",helper.DisplayNameFor(expression)));
        }
    }
}