﻿using Core.Model;
using Core.Model.Administration.User;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserViewConfiguration : GridConfiguration<User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public UserViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<User>> gridColumns)
        {
            Url url = new Url(nameof(Administration), typeof(UserController), nameof(UserController.RowContextMenu));
            gridColumns.Add(new ContextMenuColumn<User>(o => o.Guid.ToString(), "操作", url));
            gridColumns.Add(new TextGridColumn<User>(o => o.LoginName, UserIndexResource.LoginName));
            gridColumns.Add(new TextGridColumn<User>(o => o.DisplayName, UserIndexResource.DisplayName));
            gridColumns.Add(new EnumGridColumn<User>(o => o.UserType, UserIndexResource.UserType));
            gridColumns.Add(new EnumGridColumn<User>(o => o.Status, UserIndexResource.Status));
            gridColumns.Add(new DateTimeGridColumn<User>(o => o.CreatedOn, UserIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<User>(o => o.CreatedByUserName, UserIndexResource.CreatedByUserName));
        }
    }
}