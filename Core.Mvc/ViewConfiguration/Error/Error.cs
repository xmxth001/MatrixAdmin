﻿using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Core.Web.Sidebar;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Mvc.ViewConfiguration.Error
{
    public class Error : IndexBase
    {
        private readonly int errorNumber;

        public Error(IHostingEnvironment hostingEnvironment, int errorNumber) : base(hostingEnvironment)
        {
            this.errorNumber = errorNumber;
        }

        protected override string Title
        {
            get
            {
                return "Matrix Admin";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/font-awesome/css/font-awesome.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
            };
        }


        protected override string FileName
        {
            get
            {
                return "Error";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.min.js",
               "/js/jquery.ui.custom.js",
               "/js/bootstrap.min.js",
            };
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <returns></returns>
        public override string Render()
        {
            string html = base.Render().Replace("{{number}}", this.errorNumber.ToString());
            return html;
        }


        protected override string BreadCrumb()
        {
            BreadCrumb breadCrumb = new BreadCrumb("Error " + this.errorNumber);
            breadCrumb.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            breadCrumb.AddAnchor(new Anchor("/Redirect/error"+ this.errorNumber, "Error", "Go to Error", "icon-info-sign", "tip-bottom"));
            return breadCrumb.Render();
        }
    }
}