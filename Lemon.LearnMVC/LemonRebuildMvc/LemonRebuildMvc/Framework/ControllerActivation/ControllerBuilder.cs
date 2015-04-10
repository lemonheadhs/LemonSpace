using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.ControllerActivation
{
    public class ControllerBuilder
    {        
        private Func<IControllerFactory> factoryThunk;
        public static ControllerBuilder Current { get; private set; }
        public HashSet<string> DefaultNamespaces { get; private set; }

        static ControllerBuilder()
        {
            Current = new ControllerBuilder();
        }
        private ControllerBuilder() 
        {
            DefaultNamespaces = new HashSet<string>();
        }

        public IControllerFactory GetControllerFactory()
        {
            return factoryThunk();
        }
        public void SetControllerFactory(IControllerFactory controllerFactory)
        {
            factoryThunk = () => controllerFactory;
        }

    }
}