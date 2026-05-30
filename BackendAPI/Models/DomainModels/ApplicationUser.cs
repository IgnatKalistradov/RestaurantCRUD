using Microsoft.AspNetCore.Identity;

namespace BackendAPI.Models.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
