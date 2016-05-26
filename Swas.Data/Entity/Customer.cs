namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class Customer
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContactInfo { get; set; }

        public virtual ICollection<SolidWasteAct> SolidWasteActs { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
        public virtual ICollection<CustomerRepresentative> CustomerRepresentative { get; set; }
    }
}
