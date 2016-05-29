using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace Swas.Clients.Models
{
    public class PermissionViewModel
    {
        public int Id { get; set; }

        [StringLength(255), Display(Name = "აღწერა")]
        public string Description { get; set; }
        [Display(Name = "დასახელება")]
        public string Name { get; set; }
    }
}