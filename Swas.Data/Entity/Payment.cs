namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class Payment
    {
        public int Id { get; set; }
        public DateTime PayDate { get; set; }
        public int SolidWasteActId { get; set; }
        public decimal Amount { get; set; }

        public virtual SolidWasteAct SolidWasteAct { get; set; }
    }
}
