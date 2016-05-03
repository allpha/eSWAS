using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swas.Client.Models
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "მიუთითეთ რეგიონის დასახელება!")]
        [StringLength(255), Display(Name = "რეგიონის დასახელება")]
        public string Name { get; set; }
    }
}
