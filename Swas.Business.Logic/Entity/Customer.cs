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
}
