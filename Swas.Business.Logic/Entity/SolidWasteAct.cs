namespace Swas.Business.Logic.Entity
{
    using System;
    using System.Collections.Generic;

    public class SolidWasteActInfoItem
    {
        public int Id { get; set; }
        public DateTime ActDate { get; set; }
        public string LandfillName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverLastName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }


    public class SolidWasteActItem
    {
        public int Id { get; set; }
        public DateTime ActDate { get; set; }
        public int LandfillId { get; set; }
        public int ReceiverId { get; set; }
        public int PositionId { get; set; }
        public int CustomerId { get; set; }
        public int TransporterId { get; set; }
        public int RepresentativeId { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }

        public CustomerItem Customer { get; set; }
        public TransporterItem Transporter { get; set; }
        public LandfillItem Landfill { get; set; }
        public ReceiverItem Receiver { get; set; }
        public PositionItem Position { get; set; }
        public RepresentativeItem Representative { get; set; }
        public List<SolidWasteActDetailItem> SolidWasteActDetails { get; set; }
    }

    public class SolidWasteActHelperDataItem
    {
        public List<LandfillItem> LandfillItemSource { get; set; }
        public List<WasteTypeSmartItem> WasteTypeItemSource { get; set; }
        public SolidWasteActItem EditorItem { get; set; }
        
        public List<ReceiverPositionSearchItem> RecieverItemSource { get; set; }

        public List<CustomerSearchItem> CustomerItemSource { get; set; }

        public List<TransporterSearchItem> TransporterItemSource { get; set; }
    }
}
