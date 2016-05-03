using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Business.Logic.Entity
{
    public class LandfillItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public List<RegionItem> RegionItemSource { get; set; }
    }
}
