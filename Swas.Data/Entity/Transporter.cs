namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class Transporter
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string CarModel { get; set; }
        public string DriverInfo { get; set; }

        public virtual ICollection<SolidWasteAct> SolidWasteActs { get; set; }
        public virtual ICollection<CustomerRepresentative> CustomerRepresentatives { get; set; }
    }
}
