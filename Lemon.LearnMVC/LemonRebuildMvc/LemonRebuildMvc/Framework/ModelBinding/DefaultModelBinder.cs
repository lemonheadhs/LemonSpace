using LemonRebuildMvc.Framework.ActionInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LemonRebuildMvc.Framework.ModelBinding
{
    public class DefaultModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, string modelName, Type modelType)
        {
            if (modelType.IsValueType || typeof(string) == modelType)
            {
                object instance;
                if(GetValueTypeInstance(controllerContext, modelName, modelType, out instance))
                {
                    return instance;
                }
                return Activator.CreateInstance(modelType);
            }

            object modelInstance = Activator.CreateInstance(modelType);
            foreach (PropertyInfo property in modelType.GetProperties())
            {
                if (!property.CanWrite || !property.PropertyType.IsValueType || property.PropertyType != typeof(string))
                {
                    continue;
                }
                object propertyValue;
                if (GetValueTypeInstance(controllerContext, property.Name, property.PropertyType, out propertyValue))
                {
                    property.SetValue(modelInstance, propertyValue);
                }
            }
            return modelInstance;

        }

        private bool GetValueTypeInstance(ControllerContext context, string modelName, Type modelType, out object value)
        {
            //数据来源1：HttpContext.Current.Request.Form
            var form = HttpContext.Current.Request.Form;
            string key;
            if(null != form)
            {
                key = form.AllKeys.FirstOrDefault(s => string.Compare(modelName, s, true) == 0);
                if(key != null)
                {
                    value = Convert.ChangeType(form[key], modelType);
                    return true;
                }
            }

            //数据来源2：HttpContext.Current.Request.QueryString
            var queryString = HttpContext.Current.Request.QueryString;
            if(null != queryString)
            {
                key = queryString.AllKeys.FirstOrDefault(s => string.Compare(modelName, s, true) == 0);
                if(key != null)
                {
                    value = Convert.ChangeType(queryString[key], modelType);
                    return true;
                }
            }

            //数据来源3：RequestContext.RouteData.Values
            var routeDataValues = context.RequestContext.RouteData.Values;
            if(routeDataValues.ContainsKey(modelName))
            {
                value = Convert.ChangeType(routeDataValues[modelName], modelType);
                return true;
            }

            //数据来源4：RequestContext.RouteData.DataTokens
            var routeDataTokens = context.RequestContext.RouteData.DataTokens;
            if(routeDataTokens.ContainsKey(modelName))
            {
                value = Convert.ChangeType(routeDataTokens[modelName], modelType);
                return true;
            }

            value = null;
            return false;
        }
    }
}