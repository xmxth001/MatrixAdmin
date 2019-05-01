﻿using System.Text;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Addons;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Button;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Chart;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Error;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Form;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Grid;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Index;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Interface;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Login;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Widget;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Redirect.Controllers
{
    public class RedirectController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public RedirectController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
        public IActionResult Test()
        {
            File file = new File(HostingEnvironment, "a_test");
            return Content(file.Render(), "text/html");
        }

        public IActionResult Index()
        {
            Index index = new Index(this.HostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Index2()
        {
            Index2 file = new Index2(HostingEnvironment);
            return Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Login()
        {
            Login login = new Login(HostingEnvironment);
            return Content(login.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Widgets()
        {
            Widget widget = new Widget(HostingEnvironment);
            return Content(widget.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Buttons()
        {
            Button button = new Button(HostingEnvironment);
            return Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Calendar()
        {
            Calendar file = new Calendar(HostingEnvironment);
            return Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Charts()
        {
            Chart button = new Chart(HostingEnvironment);
            return Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Chat()
        {
            Chat button = new Chat(HostingEnvironment);
            return Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Dashboard()
        {
            File file = new File(HostingEnvironment, "dashboard");
            return Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Error(int number)
        {
            Error error = new Error(HostingEnvironment, number);
            return Content(error.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult FormCommon()
        {
            BasicForm basicForm = new BasicForm(HostingEnvironment);
            return Content(basicForm.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult FormValidation()
        {
            FormValidation basicForm = new FormValidation(HostingEnvironment);
            return Content(basicForm.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult FormWizard()
        {
            FormWizard basicForm = new FormWizard(HostingEnvironment);
            return Content(basicForm.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Gallery()
        {
            Gallery file = new Gallery(HostingEnvironment);
            return Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Grid()
        {
            Grid file = new Grid(HostingEnvironment);
            return Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Interface()
        {
            Interface @interface = new Interface(HostingEnvironment);
            return Content(@interface.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Invoice()
        {
            Invoice @interface = new Invoice(HostingEnvironment);
            return Content(@interface.Render(), "text/html", Encoding.UTF8);
        }
    }
}