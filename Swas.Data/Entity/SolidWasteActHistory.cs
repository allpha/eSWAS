namespace Swas.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class SolidWasteActHistory
    {
        public int Id { get; set; }
        public int SolidWasteActId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual SolidWasteAct SolidWasteAct { get; set; }
    }
}
