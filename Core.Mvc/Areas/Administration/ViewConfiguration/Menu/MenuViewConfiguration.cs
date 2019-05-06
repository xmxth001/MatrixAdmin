﻿using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class MenuViewConfiguration : GridConfiguration<Model.Administration.Menu.Menu>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewConfiguration"/> class.
        /// 构造函数
        /// </summary>
        public MenuViewConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Model.Administration.Menu.Menu>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Model.Administration.Menu.Menu>(o => o.Name, MenuIndexResource.Name));
            gridColumns.Add(new TextGridColumn<Model.Administration.Menu.Menu>(o => o.Url, MenuIndexResource.Url));
            gridColumns.Add(new TextGridColumn<Model.Administration.Menu.Menu>(o => o.Alias, MenuIndexResource.Alias));
            gridColumns.Add(new TextGridColumn<Model.Administration.Menu.Menu>(o => o.ParentName, MenuIndexResource.ParentName));
            gridColumns.Add(new IntegerGridColumn<Model.Administration.Menu.Menu>(o => o.Sort, MenuIndexResource.Sort));
            gridColumns.Add(new BooleanGridColumn<Model.Administration.Menu.Menu>(o => o.Status, MenuIndexResource.Status));
            gridColumns.Add(new EnumGridColumn<Model.Administration.Menu.Menu>(o => o.IsDefaultRouter, MenuIndexResource.IsDefaultRouter));
            gridColumns.Add(new DateTimeGridColumn<Model.Administration.Menu.Menu>(o => o.CreatedOn, MenuIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Model.Administration.Menu.Menu>(o => o.CreatedByUserName, MenuIndexResource.CreatedByUserName));
        }
    }
}
