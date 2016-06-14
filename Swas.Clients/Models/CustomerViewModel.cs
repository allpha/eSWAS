using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Swas.Business.Logic.Entity;

namespace Swas.Clients.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string TypeDescription { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContactInfo { get; set; }
    }
}