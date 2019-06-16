﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;
using Core.Model.Log;
using Core.Mvc.Areas.Log.ViewConfiguration;
using Core.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;
using Core.Api.Routes;

namespace Core.Mvc.Areas.Log.Controllers
{
    [Area(nameof(Log))]
    public class LogController : StandardController
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await HttpClientAsync.GetAsync<IList<LogModel>>(LogRoute.Index);
            LogIndex<LogModel, LogPostModel> table = new LogIndex<LogModel, LogPostModel>(model);

            return this.SearchGridConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> GridStateChange(LogPostModel model)
        {
            ResponseModel response = await HttpClientAsync.PostAsync<IList<LogModel>, LogPostModel>(LogRoute.Search, model);
            LogGridConfiguration<LogModel> configuration = new LogGridConfiguration<LogModel>(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Clear()
        {
            ResponseModel model = await HttpClientAsync.DeleteAsync(LogRoute.Clear);

            return this.Submit(model);
        }
    }
}