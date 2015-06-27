using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Globalization;
using CampervibeBooking.UI.Helpers;

namespace CampervibeBooking.UI.Extensions
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

        public static MvcHtmlString BootstrapDatePickerFor<TModel, TProperty>(
             this HtmlHelper<TModel> htmlHelper,
             Expression<Func<TModel, TProperty>> expression)
        {
            var now = DateTime.Now;
            var dateCompilation = expression.Compile();
            var date = dateCompilation(htmlHelper.ViewData.Model) as DateTime?;
            string fullPropertyName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = metadata.PropertyName;
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            string dayName = propertyName + ".Day";
            string monthName = propertyName + ".Month";
            string yearName = propertyName + ".Year";
            var dayOptionsList = new List<object>();
            var monthOptionsList = new List<object>();
            var yearOptionsList = new List<object>();

            for (int dayIndex = 1; dayIndex <= 31; dayIndex++)
            {
                dayOptionsList.Add(new { Text = dayIndex.ToString(), Value = dayIndex.ToString() });
            }

            for(int monthIndex = 1; monthIndex <= 12; monthIndex ++)
            {
                monthOptionsList.Add(new { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthIndex), Value = monthIndex.ToString() });
            }

            for(int yearIndex = 0; yearIndex < 10; yearIndex ++)
            {
                yearOptionsList.Add(new { Text = now.AddYears(yearIndex).Year.ToString(), Value = now.AddYears(yearIndex).Year.ToString() });
            }

            SelectList dayOptions = null;
            SelectList monthOptions = null;
            SelectList yearOptions = null;

            if (date.HasValue)
            {
                dayOptions = new SelectList(dayOptionsList, "Value", "Text", date.Value.Day.ToString()).AddDefaultOption();
                monthOptions = new SelectList(monthOptionsList, "Value", "Text", date.Value.Month.ToString()).AddDefaultOption();
                yearOptions = new SelectList(yearOptionsList, "Value", "Text", date.Value.Year.ToString()).AddDefaultOption();
            }
            else
            {
                dayOptions = new SelectList(dayOptionsList, "Value", "Text", null).AddDefaultOption();
                monthOptions = new SelectList(monthOptionsList, "Value", "Text", null).AddDefaultOption();
                yearOptions = new SelectList(yearOptionsList, "Value", "Text", null).AddDefaultOption();
            }

            var dayLabel = htmlHelper.Label(dayName, "Day", new { @class = "sr-only" });
            var monthLabel = htmlHelper.Label(monthName, "Month", new { @class = "sr-only" });
            var yearLabel = htmlHelper.Label(yearName, "Year", new { @class = "sr-only" });
            var rowDiv = new TagBuilder("div");
            var dayDiv = new TagBuilder("div");
            var monthDiv = new TagBuilder("div");
            var yearDiv = new TagBuilder("div");
            rowDiv.AddCssClass("row date-picker-container");
            dayDiv.AddCssClass("col-sm-3");
            monthDiv.AddCssClass("col-sm-5");
            yearDiv.AddCssClass("col-sm-4");
            var dayAttributes = new Dictionary<string, object>();
            var monthAttributes = new Dictionary<string, object>();
            var yearAttributes = new Dictionary<string, object>();
            dayAttributes["class"] = "form-control date-picker-component date-picker-day";
            monthAttributes["class"] = "form-control date-picker-component date-picker-month";
            yearAttributes["class"] = "form-control date-picker-component date-picker-year";
            var dayDropDown = htmlHelper.DropDownList(dayName, dayOptions, null, dayAttributes);
            var monthDropDown = htmlHelper.DropDownList(monthName, monthOptions, null, monthAttributes);
            var yearDropDown = htmlHelper.DropDownList(yearName, yearOptions, null, yearAttributes);
            dayDiv.InnerHtml = dayLabel.ToHtmlString() + dayDropDown.ToHtmlString();
            monthDiv.InnerHtml = monthLabel.ToHtmlString() + monthDropDown.ToHtmlString();
            yearDiv.InnerHtml = yearLabel.ToHtmlString() + yearDropDown.ToHtmlString();
            var validatorAttributes = new Dictionary<string, object>();
            validatorAttributes.Add("data-val", "true");
            validatorAttributes.Add("data-val-validdate", "The field " + labelText + " must be a valid date.");
            validatorAttributes.Add("class", "date-picker-validator");
            var validator = htmlHelper.Hidden(propertyName, date.HasValue ? date.GetValueOrDefault().ToString("yyyy-MM-dd") : null, validatorAttributes);
            rowDiv.InnerHtml = dayDiv.ToString() + monthDiv.ToString() + yearDiv.ToString() + validator.ToHtmlString();
            var fullHtmlString = MvcHtmlString.Create(rowDiv.ToString());
            return fullHtmlString;
        }

        public static MvcHtmlString TickBoxListFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> options,
            object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string propertyName = metadata.PropertyName;
            string fullPropertyName = html.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            string fullPropertyId = fullPropertyName.Replace("[", "_").Replace("]", "_").Replace(".", "_");
            var multiselectContainer = new TagBuilder("div");
            multiselectContainer.AddCssClass("row tick-box-list-container");
            var optionsList = options.ToList();

            foreach (var option in optionsList)
            {
                var checkboxId = fullPropertyId + "_Options_" + optionsList.IndexOf(option) + "_";
                var checkbox = new TagBuilder("input");
                checkbox.Attributes["type"] = "checkbox";
                checkbox.Attributes["id"] = checkboxId;
                checkbox.Attributes["value"] = option.Value;
                checkbox.Attributes["name"] = fullPropertyName;

                if (option.Selected)
                {
                    checkbox.Attributes["checked"] = "checked";
                }

                var optionLabel = new TagBuilder("label");
                optionLabel.Attributes["for"] = checkboxId;
                optionLabel.InnerHtml = checkbox.ToString() + option.Text;
                multiselectContainer.InnerHtml += optionLabel.ToString();
            }

            var validatorPropertyName = propertyName + ".Validator";
            var validatorAttributes = new Dictionary<string, object>();
            var select = html.ListBox(validatorPropertyName, options as MultiSelectList, validatorAttributes);
            multiselectContainer.InnerHtml += select.ToHtmlString();
            return MvcHtmlString.Create(multiselectContainer.ToString());
        }
    }
}