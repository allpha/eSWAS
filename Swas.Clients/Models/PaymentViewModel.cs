using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace Swas.Clients.Models
{
    public class PaymentViewModel
    {
        [Display(Name = "აქტის #")]
        public int ActId { get; set; }
        [Display(Name = "თარიღი")]
        public int ActDate { get; set; }
        [Display(Name = "ნაგავსაყრელი")]
        public string LandfillName { get; set; }
        [Display(Name = "შემომტანი")]
        public string CustomerName { get; set; }
        [Display(Name = "შემომტანის საინდეტიფიკაციო კოდი")]
        public string CustomerCode { get; set; }
        [Display(Name = "შემომტანის საინდეტიფიკაციო საკონტაქტო ინფორმაცია")]
        public string CustomerInfo { get; set; }
        [Display(Name = "რაოდენობა")]
        public decimal Quantity { get; set; }
        [Display(Name = "მთლიანი ღირებულება")]
        public decimal Price { get; set; }
        [Display(Name = "გადახდილი თანხა")]
        public decimal PaidAmount { get; set; }
        [Display(Name = "ნაშთი")]
        public decimal DebtAmount { get; set; }
    }
}