namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class Agreement
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
