﻿using System.Web;
using System.Web.Mvc;

namespace ListPlanner_Granny
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
