using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Globalization;
using Campervibe.Internal.UI.Helpers;

namespace Campervibe.Internal.UI.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static LabelHelper BeginLabelFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            TagBuilder tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.MergeAttributes(htmlAttributes, replaceExisting: true);
            htmlHelper.ViewContext.Writer.Write(tag.ToString(TagRenderMode.StartTag));
            return new LabelHelper(htmlHelper, labelText);
        }
    }
}