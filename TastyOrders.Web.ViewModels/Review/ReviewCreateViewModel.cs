using System.ComponentModel.DataAnnotations;

namespace TastyOrders.Web.ViewModels.Review
{
    using static Common.EntityValidationConstants.Review;
    public class ReviewCreateViewModel
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;

        [Required]
        [Range(RatingMinValue, RatingMaxValue)]
        public int Rating { get; set; }

        [Required]
        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = string.Empty;
    }
}
