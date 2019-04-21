﻿using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Api.Extensions.Queryable;
using Core.Api.Models.Response;
using Core.Api.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Core.Model.Entity;
using Core.Model.Enums;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class RoleController : ControllerBase
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public RoleController(Context dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this._dbContext)
            {
                IQueryable<Role> query = this._dbContext.Role.AsQueryable();
                var list = query.ToList();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(RoleRequestPayload model)
        {
            ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
            using (this._dbContext)
            {
                IQueryable<Role> query = this._dbContext.Role.AsQueryable();
                //query.AddStringContainsFilter(x => x.Name.Contains(model.KeyWord.Trim()) || x.Id.Contains(model.KeyWord.Trim()));
                query = query.AddBooleanFilter(model.IsEnable, nameof(Role.IsEnable));
                query = query.AddBooleanFilter(model.Status, nameof(Role.Status));
                query = query.Paged(model.CurrentPage, model.PageSize);
                List<Role> list = query.ToList();
                int totalCount = query.Count();
                IEnumerable<RoleJsonModel> data = list.Select(_mapper.Map<Role, RoleJsonModel>);

                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="model">角色视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(RoleCreateViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.Name.Trim().Length <= 0)
            {
                response.SetFailed("请输入角色名称");
                return Ok(response);
            }
            using (this._dbContext)
            {
                if (this._dbContext.Role.Count(x => x.Name == model.Name) > 0)
                {
                    response.SetFailed("角色已存在");
                    return Ok(response);
                }
                Role entity = _mapper.Map<RoleCreateViewModel, Role>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Id = RandomHelper.GetRandomizer(8, true, false, true, true);
                entity.IsSuperAdministrator = false;
                entity.IsBuiltin = false;
                entity.CreatedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.CreatedByUserName = AuthContextService.CurrentUser.DisplayName;
                this._dbContext.Role.Add(entity);
                this._dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="code">角色惟一编码</param>
        /// <returns></returns>
        [HttpGet("{code}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(string code)
        {
            using (this._dbContext)
            {
                Role entity = this._dbContext.Role.FirstOrDefault(x => x.Id == code);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Role, RoleCreateViewModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的角色信息
        /// </summary>
        /// <param name="model">角色视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(RoleCreateViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this._dbContext)
            {
                if (this._dbContext.Role.Count(x => x.Name == model.Name && x.Id != model.Code) > 0)
                {
                    response.SetFailed("角色已存在");
                    return Ok(response);
                }

                Role entity = this._dbContext.Role.FirstOrDefault(x => x.Id == model.Code);

                if (entity.IsSuperAdministrator && !AuthContextService.IsSupperAdministrator)
                {
                    response.SetFailed("没有足够的权限");
                    return Ok(response);
                }

                entity.Name = model.Name;
                entity.IsEnable = model.IsEnable.Value;
                entity.ModifiedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
                entity.ModifiedOn = DateTime.Now;
                entity.Status = model.Status.Value;
                entity.Description = model.Description;
                this._dbContext.SaveChanges();
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            ResponseModel response = UpdateIsEnable(true, ids);
            return Ok(response);
        }

        /// <summary>
        /// 恢复角色
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(string ids)
        {
            ResponseModel response = UpdateIsEnable(false, ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = UpdateIsEnable(true, ids);
                    break;
                case "recover":
                    response = UpdateIsEnable(false, ids);
                    break;
                case "forbidden":
                    response = UpdateStatus(StatusEnum.Forbidden, ids);
                    break;
                case "normal":
                    response = UpdateStatus(StatusEnum.Normal, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 为指定角色分配权限
        /// </summary>
        /// <param name="payload">角色分配权限的请求载体类</param>
        /// <returns></returns>
        [HttpPost("/api/v1/rbac/role/assign_permission")]
        public IActionResult AssignPermission(RoleAssignPermissionPayload payload)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this._dbContext)
            {
                Role role = this._dbContext.Role.FirstOrDefault(x => x.Id == payload.RoleCode);
                if (role == null)
                {
                    response.SetFailed("角色不存在");
                    return Ok(response);
                }
                // 如果是超级管理员，则不写入到角色-权限映射表(在读取时跳过角色权限映射，直接读取系统所有的权限)
                if (role.IsSuperAdministrator)
                {
                    response.SetSuccess();
                    return Ok(response);
                }
                //先删除当前角色原来已分配的权限
                this._dbContext.Database.ExecuteSqlCommand("DELETE FROM DncRolePermissionMapping WHERE RoleCode={0}", payload.RoleCode);
                if (payload.Permissions != null || payload.Permissions.Count > 0)
                {
                    IEnumerable<RolePermissionMapping> permissions = payload.Permissions.Select(x => new RolePermissionMapping
                    {
                        CreatedOn = DateTime.Now,
                        PermissionCode = x.Trim(),
                        RoleCode = payload.RoleCode.Trim()
                    });
                    this._dbContext.RolePermissionMapping.AddRange(permissions);
                    this._dbContext.SaveChanges();
                }

            }
            return Ok(response);
        }

        /// <summary>
        /// 获取指定用户的角色列表
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/role/find_list_by_user_guid/{guid}")]
        public IActionResult FindListByUserGuid(Guid guid)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this._dbContext)
            {
                //有N+1次查询的性能问题
                //var query = this._dbContext.DncUser
                //    .Include(r => r.UserRoles)
                //    .ThenInclude(x => x.DncRole)
                //    .Where(x => x.Guid == guid);
                //var roles = query.FirstOrDefault().UserRoles.Select(x => new
                //{
                //    x.DncRole.Code,
                //    x.DncRole.Name
                //});
                string sql = @"SELECT R.* FROM DncUserRoleMapping AS URM
INNER JOIN DncRole AS R ON R.Code=URM.RoleCode
WHERE URM.UserGuid={0}";
                List<Role> query = this._dbContext.Role.FromSql(sql, guid).ToList();
                List<string> assignedRoles = query.ToList().Select(x => x.Id).ToList();
                var roles = this._dbContext.Role.Where(x => !x.IsEnable && x.Status).ToList().Select(x => new { label = x.Name, key = x.Id });
                response.SetData(new { roles, assignedRoles });
                return Ok(response);
            }
        }

        /// <summary>
        /// 查询所有角色列表(只包含主要的字段信息:name,code)
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/role/find_simple_list")]
        public IActionResult FindSimpleList()
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this._dbContext)
            {
                var roles = this._dbContext.Role.Where(x => !x.IsEnable && x.Status).Select(x => new { x.Name, x.Id }).ToList();
                response.SetData(roles);
            }
            return Ok(response);
        }

        #region 私有方法

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsEnable(bool isEnable, string ids)
        {
            using (this._dbContext)
            {
                string sql = @"UPDATE Role SET IsEnable = @IsEnable WHERE Id IN @Ids";
                this._dbContext.Dapper.Execute(sql, new { IsEnable = isEnable, Ids = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="status">角色状态</param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(StatusEnum status, string ids)
        {
            using (this._dbContext)
            {
                string sql = @"UPDATE Role SET Status = @Status WHERE Id IN @Ids";
                this._dbContext.Dapper.Execute(sql, new { Status = status, Ids = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
        #endregion
    }
}