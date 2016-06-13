using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Swas.Clients.Models
{
    public class EditViewModel
    {
        public int Id { get; set; }
    }

    public class HistoryViewModel
    {
        public string Content { get; set; }
    }

}