namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class UserRegion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RegionId { get; set; }

        public virtual User User { get; set; }
        public virtual Region Region { get; set; }
    }
}
