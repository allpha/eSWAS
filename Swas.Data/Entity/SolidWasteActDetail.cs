namespace Swas.Data.Entity
{
    public class SolidWasteActDetail
    {
        public int Id { get; set; }
        public int SolidWasteActId { get; set; }
        public int WasteTypeId { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }

        public virtual SolidWasteAct SolidWasteAct { get; set; }
        public virtual WasteType WasteType { get; set; }
    }
}
