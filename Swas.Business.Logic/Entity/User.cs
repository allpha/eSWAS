namespace Swas.Business.Logic.Entity
{
    using System;
    using System.Collections.Generic;

    public class UserItem
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
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string JobPosition { get; set; }

        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public bool ChangePassword { get; set; }
        public virtual ICollection<int> Regions { get; set; }
    }

    public class UserInfo
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<string> Permissions { get; set; }
    }
}
