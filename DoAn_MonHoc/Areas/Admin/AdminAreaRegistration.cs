﻿using System.Web.Mvc;

namespace DoAn_MonHoc.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "LoginAdmin", id = UrlParameter.Optional },
                new[] { "DoAn_MonHoc.Areas.Admin.Controllers" }
            );
        }
    }
}
