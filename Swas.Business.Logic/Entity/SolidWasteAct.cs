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

        public virtual CustomerItem Customer { get; set; }
        public virtual TransporterItem Transporter { get; set; }
        public virtual LandfillItem Landfill { get; set; }
        public virtual ReceiverItem Receiver { get; set; }
        public virtual PositionItem Position { get; set; }
        public virtual RepresentativeItem Representative { get; set; }
        public virtual List<SolidWasteActDetailItem> SolidWasteActDetails { get; set; }
    }
}
