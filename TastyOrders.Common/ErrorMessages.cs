namespace TastyOrders.Common
{
    public static class ErrorMessages
    {
        public static class Admin
        {
            public const string FailedToAssignRoleMessage = "Failed to assign role.";
            public const string FailedToRemoveRoleMessage = "Failed to remove role.";
        }

        public static class MenuItemManagement
        {
            public const string RestaurantNotFound = "Restaurant not found.";
            public const string RequiredFieldsMessage = "All fields are required, and price must be greater than 0.";
            public const string AddedItem = "Menu item added successfully.";
            public const string NotFoundItemMessage = "Menu item not found.";
            public const string SuccessfullyRemovedMessage = "Menu item removed successfully.";
            public const string FailedToEditMessage = "Failed to update the menu item. Please try again.";
            public const string SuccessfullyEditedMessage = "Menu item updated successfully.";
        }

        public static class RestaurantManagement
        {
            public const string RestaurantNotFoundMessage = "Restaurant not found.";
            public const string RestaurantSuccessfullyRemovedMessage = "Restaurant has been removed successfully.";
        }

        public static class ReviewManagement
        {
            public const string ReviewNotFoundMessage = "Review not found.";
            public const string ReviewDeletedSuccesfullyMessage = "Review deleted successfully.";
        }

        public static class Cart
        {
            public const string LoggedInMessage = "You must be logged in to add items to the cart.";
            public const string AddItemErrorMessage = "Unable to add item to cart. Ensure all items are from the same location.";
            public const string AddItemSuccessMessage = "Item added to cart!";
        }
    }
}
