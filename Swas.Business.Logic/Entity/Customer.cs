namespace Swas.Business.Logic.Entity
{
    using System.Collections.Generic;

    public class CustomerItem
    {
        public int Id { get; set; }
        public CustomerType Type { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContactInfo { get; set; }
    }


    public enum CustomerType
    {
        Municipal = 0,
        Juridical = 1,
        Personal = 2
    }

    public class CustomerSearchItem
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContactInfo { get; set; }
        public string RepresentativeName { get; set; }
        public string CarNumber { get; set; }
        public string CarModel { get; set; }
        public string DriverInfo { get; set; }
    }

    public class CustomerRootItem
    {
        public string TypeDescription { get; set; }
        public List<CustomerChildItem> ChildItemSource { get; set; }
    }

    public class CustomerChildItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
