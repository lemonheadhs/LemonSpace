using LemonRebuildMvc.Framework.ActionInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.ModelBinding
{
    public class DefaultModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, string modelName, Type modelType)
        {
            throw new NotImplementedException();
        }
    }
}