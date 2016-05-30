namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string JobPosition { get; set; }
        public DateTime CreateDate { get; set; }
             

        public virtual User User { get; set; }
    }
}
