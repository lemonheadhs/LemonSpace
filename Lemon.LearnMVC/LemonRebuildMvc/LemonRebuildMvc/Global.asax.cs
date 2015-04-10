﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using LemonRebuildMvc.Framework.URLMapping;
using LemonRebuildMvc.Framework.ControllerActivation;

namespace LemonRebuildMvc
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add("default", new Route { Url = "{controller}/{action}" });
            //ControllerBuilder.Current.SetControllerFactory(n
            ControllerBuilder.Current.DefaultNamespaces.Add("LemonRebuildMvc");
        }

    }
}