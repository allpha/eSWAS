namespace Swas.Business.Logic.Entity
{
    using System;
    using System.Collections.Generic;

    public class SolidWasteActReportDetailReportItem
    {
        public int Id { get; set; }
        public DateTime ActDate { get; set; }
        public string RegionName { get; set; }
        public string LandfillName { get; set; }
        public string ReceiverName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WasteTypeName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public int CustomerType { get; set; }
    }


    public class SolidWasteActTotalSumReportItem
    {
        public int ActDate { get; set; }
        public string RegionName { get; set; }
        public string LandfillName { get; set; }
        public string ReceiverName { get; set; }
        public int CustomerType { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WasteTypeName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }


    public class ReportItem
    {
        public object ReportData { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalQuantity { get; set; }
    }

}
