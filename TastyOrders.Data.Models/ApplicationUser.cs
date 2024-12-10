using Microsoft.AspNetCore.Identity;

namespace TastyOrders.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public Cart? Cart { get; set; }
    }
}
