﻿using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Role
    {
        public Role()
        {
            this.RolePermissionMapping = new HashSet<RolePermissionMapping>();
            this.UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string ModifiedByUserName { get; set; }
        public bool IsSuperAdministrator { get; set; }
        public bool IsBuiltin { get; set; }
        public Guid CreatedByUserGuid { get; set; }
        public Guid? ModifiedByUserGuid { get; set; }
        public bool IsEnable { get; set; }
        public bool Status { get; set; }
        public int Id { get; set; }

        public virtual ICollection<RolePermissionMapping> RolePermissionMapping { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}