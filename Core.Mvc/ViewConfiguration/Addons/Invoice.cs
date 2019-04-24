﻿using System.Collections.Generic;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Addons
{
    public class Invoice : IndexBase
    {
        public Invoice(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "Invoice";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                
                "/css/bootstrap-responsive.min.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               
               "/js/bootstrap.min.js",
               "/js/jquery.ui.custom.js",
               "/js/matrix.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Invoice");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}