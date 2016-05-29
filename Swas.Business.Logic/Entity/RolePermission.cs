namespace Swas.Business.Logic.Entity
{
    using System;
    using System.Collections.Generic;

    public class RolePermissionItem
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionDescription { get; set; }
    }

}
