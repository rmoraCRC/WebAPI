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

            formTagCreate.RemoveAll(x => x.Key.Equals(HtmlTypeTag.HtmlTagType.Hidden.ToString()));

            int index = 0;
            var tagContentDivide = from item in formTagCreate
                                   group item by index++ % 2 into part
                                   select part.ToList();

            var tagToGroup = tagContentDivide as IList<List<KeyValuePair<string, HtmlTag>>> ?? tagContentDivide.ToList();

            foreach (var groupLeft in tagToGroup[0])
            {
                contentFormTagLeft.Append(groupLeft.Value);
            }

            foreach (var groupRight in tagToGroup[1])
            {
                contentFormTagRight.Append(groupRight.Value);
            }



            var quantityToAddForLeftContainer = tagToGroup[0].Count() - tagToGroup[1].Count();
            if (quantityToAddForLeftContainer >= 1)
            {
                var divTagSpace = new HtmlTag("div")
                    .AddClass("divFormSpace");

                for (int i = 0; i < quantityToAddForLeftContainer; i++)
                {
                    contentFormTagRight.Append(divTagSpace);
                }
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