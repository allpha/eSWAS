namespace Swas.Business.Logic.Entity
{
    using System;
    using System.Collections.Generic;

    public class AgreementItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

}
