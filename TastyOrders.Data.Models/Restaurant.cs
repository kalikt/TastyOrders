using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyOrders.Data.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;

        public string? ImageUrl { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
