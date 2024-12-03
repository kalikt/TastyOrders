
using System.ComponentModel.DataAnnotations;

namespace TastyOrders.Web.ViewModels.Restaurant
{
    using static Common.EntityValidationConstants.Restaurant;
    public class EditRestaurantViewModel
    {
        public int Id { get; set; }

        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [MinLength(LocationMinLength)]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;

        [MinLength(ImageUrlMinLength)]
        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
