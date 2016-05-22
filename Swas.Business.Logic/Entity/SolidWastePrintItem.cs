using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Business.Logic.Entity
{
    public class SolidWasteActPrintItem
    {
        public int Id { get; set; }
        public DateTime ActDate { get; set; }
        public string LandfillName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverLastName { get; set; }
        public string PositionName { get; set; }

        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerContactInfo { get; set; }
        public string RepresentativeName { get; set; }
        public string TransporterCarNumber { get; set; }
        public string TransporterCarModel { get; set; }
        public string TransporterDriverInfo { get; set; }

        public string Remark { get; set; }

        public decimal TotalAmount { get; set; }
        public List<SolidWasteActDetailPrintItem> DetailItemSource { get; set; }
    }



    public class SolidWasteActDetailPrintItem
    {
        public string WasteTypeName { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}
