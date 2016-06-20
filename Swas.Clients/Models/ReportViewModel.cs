using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace Swas.Clients.Models
{
    public class SolidWasteActReportDetailReportViewModel
    {
        [Display(Name = "#")]
        public int Id { get; set; }
        [Display(Name = "მიღების თარიღი")]
        public DateTime ActDate { get; set; }
        [Display(Name = "გეოგრაფიული არეალი")]
        public string RegionName { get; set; }
        [Display(Name = "ნაგავსაყრელი")]
        public string LandfillName { get; set; }
        [Display(Name = "მიმღები")]
        public string ReceiverName { get; set; }
        [Display(Name = "შემომტანის ტიპი")]
        public string CustomerType { get; set; }
        [Display(Name = "შემომტანის საინდეფიკაციო კოდი")]
        public string CustomerCode { get; set; }
        [Display(Name = "შემომტანის დასახელება")]
        public string CustomerName { get; set; }
        [Display(Name = "მანქანის ნომერი")]
        public string CarNumber { get; set; }
        [Display(Name = "მყარი ნარჩენის ტიპი")]
        public string WasteTypeName { get; set; }
        [Display(Name = "ნარჩენის მოცულობა")]
        public decimal Quantity { get; set; }
        [Display(Name = "ერთეულის ღირებულება")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "ნარჩენის ღირებულება")]
        public decimal Price { get; set; }
    }

    public class SolidWasteActTotalSumReportViewModel
    {
        [Display(Name = "#")]
        public int Id { get; set; }
        [Display(Name = "წელი")]
        public int Year { get; set; }
        [Display(Name = "გეოგრაფიული არეალი")]
        public string RegionName { get; set; }
        [Display(Name = "ნარცენის სახეობა")]
        public string LandfillName { get; set; }
        [Display(Name = "ნაგავსაყრელი")]
        public string ReceiverName { get; set; }
        [Display(Name = "შემომტანი")]
        public string CustomerType { get; set; }
        [Display(Name = "შემომტანის საინდეტიფიკაციო კოდი")]
        public string CustomerCode { get; set; }
        [Display(Name = "შემომტანის დასახელება")]
        public string CustomerName { get; set; }
        [Display(Name = "მანქანის ნომერი")]
        public string CarNumber { get; set; }

        [Display(Name = "მოცულობა")]
        public decimal Quantity { get; set; }
        [Display(Name = "ღირებულება")]
        public decimal Price { get; set; }
    }


    public class SolidWasteActPaymentReportViewModel
    {
        [Display(Name = "#")]
        public int Id { get; set; }
        [Display(Name = "მიღების თარიღი")]
        public DateTime ActDate { get; set; }
        [Display(Name = "ნაგავსაყრელი")]
        public string LandfillName { get; set; }
        [Display(Name = "შემომტანის საინდეფიკაციო კოდი")]
        public string CustomerCode { get; set; }
        [Display(Name = "შემომტანის დასახელება")]
        public string CustomerName { get; set; }
        [Display(Name = "ნარჩენის რაოდენობა")]
        public decimal Quantity { get; set; }
        [Display(Name = "ღირებულება")]
        public decimal Price { get; set; }
        [Display(Name = "გადახდილი თანხა")]
        public decimal PaidAmount { get; set; }
        [Display(Name = "ნაშთი")]
        public decimal DebtAmount { get; set; }
    }


}