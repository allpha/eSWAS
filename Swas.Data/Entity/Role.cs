namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;
    public class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
