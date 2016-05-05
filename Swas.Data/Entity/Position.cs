namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ReceiverPosition> ReceiverPositions { get; set; }
        public virtual ICollection<SolidWasteAct> SolidWasteActs { get; set; }
    }
}
