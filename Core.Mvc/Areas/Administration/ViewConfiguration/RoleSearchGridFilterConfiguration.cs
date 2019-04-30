﻿using System.Collections.Generic;
using Core.Model.Administration.Role;
using Core.Mvc.ViewConfiguration;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class RoleSearchGridFilterConfiguration : GridFilterConfiguration<RolePostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<RolePostModel>(o => o.RoleName, "角色名称"));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加"));
            buttons.Add(new StandardButton("编辑"));
        }
    }
}
