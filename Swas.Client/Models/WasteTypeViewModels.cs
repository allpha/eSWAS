using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Client.Models
{
    public class WasteTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "მიუთითეთ მიუთითეთ მყარი ნარჩენის ტიპის დასახელება!")]
        [StringLength(255), Display(Name = "დასახელება")]
        public string Name { get; set; }

        [Display(Name = "<")]
        public decimal LessQuantity { get; set; }
        [Display(Name = "დან")]
        public decimal FromQuantity { get; set; }
        [Display(Name = "მდე")]
        public decimal EndQuantity { get; set; }
        [Display(Name = ">")]
        public decimal MoreQuantity { get; set; }
        [Display(Name = "თანხა ლარებში")]

        public decimal MunicipalityLessQuantityPrice { get; set; }
        public decimal MunicipalityIntervalQuantityPrice { get; set; }
        public decimal MunicipalityMoreQuantityPrice { get; set; }

        public decimal LegalPersonLessQuantityPrice { get; set; }
        public decimal LegalPersonIntervalQuantityPrice { get; set; }
        public decimal LegalPersonMoreQuantityPrice { get; set; }

        public decimal PhysicalPersonLessQuantityPrice { get; set; }
        public decimal PhysicalPersonIntervalQuantityPrice { get; set; }
        public decimal PhysicalPersonMoreQuantityPrice { get; set; }

        [Display(Name = "1მ კუბი ტონაში")]
        public decimal Coeficient { get; set; }

        [Display(Name = "რაოდენობა")]
        public string CountDescription { get; set; }
        [Display(Name = "ღირებულება მუნიციპალიტეტებისთვის")]
        public string MunicipalityDescription { get; set; }
        [Display(Name = "ღირებულება იურიდიული პირებისთვის")]
        public string LegalPersionDescription { get; set; }
        [Display(Name = "ღირებულება ფიზიკური პირებისთვის")]
        public string PhisicalPersionDestcription { get; set; }

    }
}
