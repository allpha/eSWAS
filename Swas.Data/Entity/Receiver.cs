namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class Receiver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ReceiverPosition> ReceiverPositions { get; set; }
        public virtual ICollection<SolidWasteAct> SolidWasteActs { get; set; }
    }
}
