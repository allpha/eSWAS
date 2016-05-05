namespace Swas.Data.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReceiverPosition
    {
        public int Id { get; set; }
        [Index("IX_ReceiverPosition_ReceiverId_PositionId", 1)]
        public int ReceiverId { get; set; }
        [Index("IX_ReceiverPosition_ReceiverId_PositionId", 2)]
        public int PositionId { get; set; }

        public virtual Receiver Receiver { get; set; }
        public virtual Position Position { get; set; }
    }
}
