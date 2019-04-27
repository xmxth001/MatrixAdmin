﻿using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.ViewConfiguration.Administration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers.Administration
{
    [Area(nameof(Administration))]
    public class UserController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public UserController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.Index));
            var model = AsyncRequest.GetAsync<IList<User>>(url).Result;
            UserIndex table = new UserIndex(HostingEnvironment, model);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(UserPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.Search));
            ResponseModel response = AsyncRequest.PostAsync<IList<User>, UserPostModel>(url, model).Result;
            UserViewConfiguration configuration = new UserViewConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}