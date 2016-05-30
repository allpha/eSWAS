namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool UseEmailAsUserName { get; set; }
        public int MaxAttamptPassword { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime? LastLockedDate { get; set; }
        public DateTime? LastDisibledDate { get; set; }
        public int RoleId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserRegion> UserRegions { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
