namespace Swas.Business.Logic.Entity
{
    using System.Collections.Generic;

    public class TransporterItem
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string CarModel { get; set; }
        public string DriverInfo { get; set; }
    }

    public class TransporterSearchItem
    {
        public string Description { get; set; }
        public string CarNumber { get; set; }
        public string CarModel { get; set; }
        public string DriverInfo { get; set; }
    }

}
