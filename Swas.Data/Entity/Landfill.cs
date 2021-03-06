﻿namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class Landfill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<SolidWasteAct> SolidWasteActs { get; set; }
    }
}
