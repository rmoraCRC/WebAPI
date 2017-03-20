using System.Web.Script.Serialization;
using System;

namespace SecurityAppApi.Helpers
{
    public static class JsonHelper
    {
        #region Public extension methods.
        /// <summary>
        /// Extened method of object class, Converts an object to a json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }
}