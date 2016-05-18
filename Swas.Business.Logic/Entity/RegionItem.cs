using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Business.Logic.Entity
{
    public class RegionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RegionSearchItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LandfillItem> LandfillItemSource { get; set; }
    }
}
