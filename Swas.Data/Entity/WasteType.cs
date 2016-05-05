namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class WasteType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal LessQuantity { get; set; }
        public decimal FromQuantity { get; set; }
        public decimal EndQuantity { get; set; }
        public decimal MoreQuantity { get; set; }


        public decimal MunicipalityLessQuantityPrice { get; set; }
        public decimal MunicipalityIntervalQuantityPrice { get; set; }
        public decimal MunicipalityMoreQuantityPrice { get; set; }

        public decimal LegalPersonLessQuantityPrice { get; set; }
        public decimal LegalPersonIntervalQuantityPrice { get; set; }
        public decimal LegalPersonMoreQuantityPrice { get; set; }

        public decimal PhysicalPersonLessQuantityPrice { get; set; }
        public decimal PhysicalPersonIntervalQuantityPrice { get; set; }
        public decimal PhysicalPersonMoreQuantityPrice { get; set; }

        public decimal Coeficient { get; set; }

        public virtual ICollection<SolidWasteActDetail> SolidWasteActDetails { get; set; }
    }
}
