﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Login
{
    public class Login : IndexBase
    {
        public Login(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/font-awesome/css/font-awesome.css",
                "/css/matrix-login.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "login";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.min.js",
               "/js/matrix.login.js"
            };
        }
    }
}