using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Swas.Clients.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [StringLength(255), Display(Name = "დასახელება")]
        public string Description { get; set; }
        public List<PermissionViewModel> Periossions { get; set; }
    }
}