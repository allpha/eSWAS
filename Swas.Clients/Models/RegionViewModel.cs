using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace Swas.Clients.Models
{
    public class AgreementViewModel
    {
        public int Id { get; set; }

        [StringLength(255), Display(Name = "ხელშეკრულების ნომერი")]
        public string Code { get; set; }
        [Display(Name = "იურიდიული პირი")]
        public int CustomerId { get; set; }
        [Display(Name = "დაწყების თარიღი")]
        public DateTime StartDate { get; set; }
        [Display(Name = "დასრულების თარიღი")]
        public DateTime EndDate { get; set; }
    }
}