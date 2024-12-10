using System.ComponentModel.DataAnnotations;

namespace TastyOrders.Web.ViewModels.MenuItem
{
    using static Common.EntityValidationConstants.MenuItem;
    public class EditMenuItemViewModel
    {
        public int Id { get; set; } 

        public int RestaurantId { get; set; }

        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MinLength(ImageUrlMinLength)]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;
    }
}
