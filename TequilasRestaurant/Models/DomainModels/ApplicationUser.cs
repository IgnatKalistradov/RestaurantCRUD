using Microsoft.AspNetCore.Identity;

namespace TequilasRestaurant.Models.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
