namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class SolidWasteAct
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

        public bool HasHistory { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Transporter Transporter { get; set; }
        public virtual Landfill Landfill { get; set; }
        public virtual Receiver Receiver { get; set; }
        public virtual Position Position { get; set; }
        public virtual Representative Representative { get; set; }
        public virtual ICollection<SolidWasteActDetail> SolidWasteActDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<SolidWasteActHistory> SolidWasteActHistories { get; set; }
    }
}
