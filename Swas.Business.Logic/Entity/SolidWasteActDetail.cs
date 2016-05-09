namespace Swas.Business.Logic.Entity
{
    public class SolidWasteActDetailItem
    {
        public int Id { get; set; }
        public int WasteTypeId { get; set; }
        public string WasteTypeName { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}
