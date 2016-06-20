using Swas.Business.Logic.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Clients.Models
{
    public class SolidWasteActInfoViewModel
    {
        [Display(Name = "#")]
        public int Id { get; set; }
        [Display(Name = "შემოტანის თარიღი")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ActDate { get; set; }
        [Display(Name = "ნაგავსაყრელი")]
        public string LandfillName { get; set; }
        [Display(Name = "მიმღები")]
        public string Receiver { get; set; }
        [Display(Name = "შემომტანი")]
        public string Customer { get; set; }
        [Display(Name = "რაოდენობა")]
        public decimal Quantity { get; set; }
        [Display(Name = "ღირებულება")]
        public decimal Price { get; set; }
        [Display(Name = "მანქანის ნომერი")]
        public string CarNumber { get; set; }

    }


    public class SolidWasteActViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "მიუთითეთ თარიღი!")]
        [Display(Name = "თარიღი")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ActDate { get; set; }
        [Display(Name = "ნაგავსაყრელი")]
        public int LandfillId { get; set; }
        //public int ReceiverId { get; set; }
        //public int PositionId { get; set; }
        //public int CustomerId { get; set; }
        //public int TransporterId { get; set; }
        //public int RepresentativeId { get; set; }

        [Required(ErrorMessage = "მიუთითეთ მიმღების სახელი!")]
        [StringLength(255), Display(Name = "სახელი")]
        public string ReceiverName { get; set; }
        [Required(ErrorMessage = "მიუთითეთ მიმღების გვარი!")]
        [StringLength(255), Display(Name = "გვარი")]
        public string ReceiverLastName { get; set; }
        [Required(ErrorMessage = "მიუთითეთ მიმღების სახელი!")]
        [StringLength(255), Display(Name = "თანამდებობა")]
        public string PositionName { get; set; }

        [StringLength(255), Display(Name = "ტიპი")]
        public int Type { get; set; }
        [Required(ErrorMessage = "მიუთითეთ მიმღების სახელი!")]
        [StringLength(255), Display(Name = "დასახელება")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "მიუთითეთ საინდედიფიკაციო კოდი!")]
        [StringLength(255), Display(Name = "საინდედიფიკაციო კოდი")]
        public string CustomerCode { get; set; }
        [Required(ErrorMessage = "მიუთითეთ საკონტაქტო ინფორმაცია!")]
        [StringLength(200), Display(Name = "საკონტაქტო ინფორმაცია")]
        public string CustomerContactInfo { get; set; }
        [Required(ErrorMessage = "მიუთითეთ წარმომადგენელი!")]
        [StringLength(200), Display(Name = "წარმომადგენელი")]
        public string RepresentativeName { get; set; }
        [Required(ErrorMessage = "მიუთითეთ ავტომობილის მარკა!")]
        [StringLength(200), Display(Name = "ავტომობილის მარკა")]
        public string TransporterCarNumber { get; set; }
        [Required(ErrorMessage = "მიუთითეთ ავტომობილის მოდელი")]
        [StringLength(200), Display(Name = "ავტომობილის მოდელი")]
        public string TransporterCarModel { get; set; }
        [Required(ErrorMessage = "მიუთითეთ მძღოლი!")]
        [StringLength(200), Display(Name = "მძღოლი")]
        public string TransporterDriverInfo { get; set; }

        [StringLength(1000), Display(Name = "შენიშვნა")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        public int WasteTypeId { get; set; }
        public decimal Quantity { get; set; }

        public List<SolidWasteActDetailViewModel> SolidWasteActDetails { get; set; }

        public List<ReceiverPositionSearchItem> RecieverItemSource { get; set; }
        public List<CustomerSearchItem> CustomerItemSource { get; set; }
        public List<TransporterSearchItem> TransporterItemSource { get; set; }
    }

    
}
