namespace Swas.Business.Logic.Entity
{
    using System;
    using System.Collections.Generic;

    public class RoleItem
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual List<RolePermissionItem> RolePermissions { get; set; }
    }

}
