using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Swas.Clients.Models
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "მიუთითეთ რეგიონის დასახელება!")]
        [StringLength(255), Display(Name = "დასახელება")]
        public string Name { get; set; }
    }
}