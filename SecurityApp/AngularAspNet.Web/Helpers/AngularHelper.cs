using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using HtmlTags;


namespace SecurityApp.Web.Helpers
{
    public static class AngularHelper
    {
        public static AngularHelper<TModel> Angular<TModel>(this HtmlHelper<TModel> helper)
        {
            return new AngularHelper<TModel>(helper);
        }
    }

    public class AngularHelper<TModel>
    {
        private readonly HtmlHelper<TModel> _htmlHelper;

        public AngularHelper(HtmlHelper<TModel> helper)
        {
            _htmlHelper = helper;
        }

        public AngularModelHelper<TModel> ModelFor(string expressionPrefix)
        {
            return new AngularModelHelper<TModel>(_htmlHelper, expressionPrefix);
        }

        public HtmlTag FormForModelInputGroup(string expressionPrefix)
        {
            var modelHelper = ModelFor(expressionPrefix);

            var formGroupForMethodGeneric = typeof(AngularModelHelper<TModel>)
                .GetMethod("FormInputGroupFor");


            var propertiesModel = typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var formTagCreate = new List<KeyValuePair<string, HtmlTag>>();


            foreach (var propertyInfo in propertiesModel)
            {
                var currentTypeTag = HtmlTypeTag.HtmlTagType.Text.ToString();

                if (propertyInfo.GetCustomAttributes().OfType<HiddenInputAttribute>().Any())
                    currentTypeTag = HtmlTypeTag.HtmlTagType.Hidden.ToString();

                var formGroupForProp = formGroupForMethodGeneric
                    .MakeGenericMethod(propertyInfo.PropertyType);

                var propertyLambda = MakeLambda(propertyInfo);


                var formGroupTag = (HtmlTag)formGroupForProp.Invoke(modelHelper,
                    new[] { propertyLambda });

                var currentField = new KeyValuePair<string, HtmlTag>(currentTypeTag, formGroupTag);

                formTagCreate.Add(currentField);

            }

            return CreateFormTag(formTagCreate);
        }

        private HtmlTag CreateFormTag(List<KeyValuePair<string, HtmlTag>> formTagCreate)
        {
            var contentFormTag = new HtmlTag("div").NoTag();
            var contentFormTagLeft = new HtmlTag("div").AddClass("leftform");
            var contentFormTagRight = new HtmlTag("div").AddClass("rightform");

            contentFormTagLeft.Append(formTagCreate.Where(x => x.Key.Equals(HtmlTypeTag.HtmlTagType.Hidden.ToString())).Select(x => x.Value));

            var formTagToAdd = formTagCreate.RemoveAll(x => x.Key.Equals(HtmlTypeTag.HtmlTagType.Hidden.ToString()));

            var quantityForLeftContainer = formTagCreate.Count() / 2;

            for (int indexLeft = 0; indexLeft < quantityForLeftContainer; indexLeft++)
            {
                contentFormTagLeft.Append(formTagCreate[indexLeft].Value);
            }

            var quantityForRightContainer = (quantityForLeftContainer*2);

            for (int indexRight = quantityForLeftContainer; indexRight < quantityForRightContainer; indexRight++)
            {
                contentFormTagRight.Append(formTagCreate[indexRight].Value);
            }

            return contentFormTag.Append(contentFormTagLeft).Append(contentFormTagRight);

        }

        private object MakeLambda(PropertyInfo prop)
        {
            var parameter = Expression.Parameter(typeof(TModel), "x");
            var property = Expression.Property(parameter, prop);
            var funcType = typeof(Func<,>).MakeGenericType(typeof(TModel), prop.PropertyType);

            return Expression.Lambda(funcType, property, parameter);
        }
    }
}