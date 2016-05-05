namespace Swas.Data.Entity
{
    using System.Collections.Generic;

    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Landfill> Landfills { get; set; }
    }

}
