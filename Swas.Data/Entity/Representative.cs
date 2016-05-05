namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class Representative
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerRepresentative> CustomerRepresentatives { get; set; }
        public virtual ICollection<SolidWasteAct> SolidWasteActs { get; set; }
    }
}

