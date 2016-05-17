using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Clients.Models
{
    public class LandfillViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "მიუთითეთ ნაგავსაყრელის დასახელება!")]
        [StringLength(255), Display(Name = "დასახელება")]
        public string Name { get; set; }
        
        [Display(Name= "რეგიონი")]
        public int RegionID { get; set; }

        [StringLength(255), Display(Name = "რეგიონი")]
        public string RegionName { get; set; }
    }
}
