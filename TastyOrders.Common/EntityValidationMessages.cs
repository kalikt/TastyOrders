
namespace TastyOrders.Common
{
    public static class EntityValidationMessages
    {
        public static class Admin
        {
            public const string EmailAndPasswordRequiredMessage = "Email and password are required.";
            public const string SuccessfullyAddedUserMessage = "User added successfully.";
            public const string UserNotFoundMessage = "User not found.";
            public const string FailedRemovalOfUserMessage = "Failed to remove user.";
        }

        public static class RestaurantManagement
        {
            public const string NameLocationRequired = "Name and location are required.";
            public const string NameRequired = "Name is required.";
            public const string LocationRequired = "Location is required.";
            public const string ImageUrlMessage = "Please provide a valid URL.";
        }
    }
}
