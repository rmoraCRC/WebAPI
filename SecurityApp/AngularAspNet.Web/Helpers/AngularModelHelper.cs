using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using SecurityApp.Web.Utilities;
using HtmlTags;
using Humanizer;

namespace SecurityApp.Web.Helpers
{
    public class AngularModelHelper<TModel>
    {
        protected readonly HtmlHelper Helper;
        private readonly string _expressionPrefix;

        public AngularModelHelper(HtmlHelper helper, string expressionPrefix)
        {
            Helper = helper;
            _expressionPrefix = expressionPrefix;
        }

        /// <summary>
        /// Converts an lambda expression into a camel-cased string, prefixed
        /// with the helper's configured prefix expression, ie:
        /// vm.model.parentProperty.childProperty
        /// </summary>
        public IHtmlString ExpressionFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var expressionText = ExpressionForInternal(property);
            return new MvcHtmlString(expressionText);
        }

        /// <summary>
        /// Converts a lambda expression into a camel-cased AngularJS binding expression, ie:
        /// {{vm.model.parentProperty.childProperty}} 
        /// </summary>
        public IHtmlString BindingFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            return MvcHtmlString.Create("{{" + ExpressionForInternal(property) + "}}");
        }

        /// <summary>
        /// Creates a div with an ng-repeat directive to enumerate the specified property,
        /// and returns a new helper you can use for strongly-typed bindings on the items
        /// in the enumerable property.
        /// </summary>
        public AngularNgRepeatHelper<TSubModel> Repeat<TSubModel>(
            Expression<Func<TModel, IEnumerable<TSubModel>>> property, string variableName)
        {
            var propertyExpression = ExpressionForInternal(property);
            return new AngularNgRepeatHelper<TSubModel>(
                Helper, variableName, propertyExpression);
        }

        private string ExpressionForInternal<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var camelCaseName = property.ToCamelCaseName();

            var expression = !string.IsNullOrEmpty(_expressionPrefix)
                ? _expressionPrefix + "." + camelCaseName
                : camelCaseName;

            return expression;
        }

        private ModelMetadata getObjectWithCustomAttributesMetadata<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var memberExpression = property.Body as MemberExpression;
            var metadata = ModelMetadata.FromLambdaExpression(property, new ViewDataDictionary<TModel>());

            if (memberExpression == null) return metadata;
            foreach (var customAttributes in memberExpression.Member.CustomAttributes)
            {
                var attributeTypeName = customAttributes.AttributeType.Name;
                if (customAttributes.NamedArguments != null)
                    foreach (var namedArgument in customAttributes.NamedArguments)
                    {
                        metadata.AdditionalValues[attributeTypeName + namedArgument.MemberName] = namedArgument.TypedValue.Value;
                    }
            }

            return metadata;
        }
        public HtmlTag FormInputGroupFor<TProp>(Expression<Func<TModel, TProp>> property)
        {

            var metadata = getObjectWithCustomAttributesMetadata(property);

            var isCheckBox = metadata.ModelType == typeof (Boolean);

            var fontAwesomeType = "fa fa-random";
            if (metadata.AdditionalValues.ContainsKey("FontAwesomeName"))
                fontAwesomeType = metadata.AdditionalValues["FontAwesomeName"].ToString();

            var fieldName = ExpressionHelper.GetExpressionText(property);

            var labelText = metadata.DisplayName ?? fieldName.Humanize(LetterCasing.Title);

            var angularFieldBinding = ExpressionForInternal(property);

            var containerTag= new HtmlTag("div").NoTag();

            var pLabelTag = new HtmlTag("p")
                .AppendHtml(labelText);

            if (metadata.IsRequired && !isCheckBox)
            {
                var spanLabelTag = new HtmlTag("span")
                    .AppendHtml("*");
                pLabelTag.Append(spanLabelTag);
            }

            containerTag.Append(pLabelTag);

            var formGroup = new HtmlTag("div");
            if (metadata.DataTypeName != "Hidden")
            {
                formGroup.AddClasses(isCheckBox ? "has-feedback" : "", "input-group");
                formGroup.Attr("form-group-validation", fieldName);
            }
            var spanInputGroupAddon = new HtmlTag("span")
                .AddClasses("input-group-addon", fontAwesomeType)
                .Attr("id", fieldName);

            var tagName = metadata.DataTypeName == "MultilineText"
                ? "textarea"
                : "input";

            var placeholder = metadata.Watermark ??
                              (labelText + "...");

            var input = new HtmlTag(tagName)
            .AddClass(metadata.ModelType == typeof(Boolean) ? "" : "form-control")
            .Attr("ng-model", angularFieldBinding)
            .Attr("name", fieldName)
            .Attr("type", "text")
            .Attr("aria-describedby", fieldName)
            .Attr("placeholder", placeholder);

            ApplyValidationToInput(input, metadata);

            if (metadata.DataTypeName == "Hidden")
                return formGroup
               .Append(input);

            if (!isCheckBox)
                return containerTag.Append(formGroup
                    .Append(spanInputGroupAddon)
                    .Append(input));

            var divSliderTag = new HtmlTag("div")
                .AddClass("slider round");

            var label = new HtmlTag("label")
                .AddClass("switch")
                .Append(input)
                .Append(divSliderTag);

            return containerTag.Append(formGroup
                .Append(label));
        }

        private void ApplyValidationToInput(HtmlTag input, ModelMetadata metadata)
        {
            if (metadata.IsRequired && metadata.DataTypeName != "Hidden" && metadata.ModelType != typeof(Boolean))
                input.Attr("required", "");

            if (metadata.DataTypeName == "EmailAddress")
                input.Attr("type", "email");

            if (metadata.DataTypeName == "PhoneNumber")
                input.Attr("pattern", @"[\ 0-9()-]+");

            if (metadata.DataTypeName == "Password")
                input.Attr("type", "password");

            if (metadata.DataTypeName == "Hidden")
                input.Attr("type", "hidden");

            if (metadata.ModelType == typeof(Boolean))
                input.Attr("type", "checkbox");

        }
    }
}