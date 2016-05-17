using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Clients.Models
{
    public class SolidWasteActDetailViewModel
    {
        public int Id { get; set; }
        public int WasteTypeId { get; set; }
        public string WasteTypeName{ get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }

}
