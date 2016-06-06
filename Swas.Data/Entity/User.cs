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
        public int RoleId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string JobPosition { get; set; }

        public DateTime CreateDate { get; set; }
        public bool ChangePassword { get; set; }

        public Guid? SeassionId { get; set; }
        public DateTime? LastActivityDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserRegion> UserRegions { get; set; }
    }
}
