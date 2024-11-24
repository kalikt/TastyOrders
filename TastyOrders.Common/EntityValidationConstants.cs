using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyOrders.Common
{
    public static class EntityValidationConstants
    {
        public static class ApplicationUser
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 50;
            public const int LastNameMinLength = 3;
            public const int LastNameMaxLength = 50;
        }

        public static class MenuItem
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
            public const int DescriptionMinLength = 3;
            public const int DescriptionMaxLength = 150;
            public const int ImageUrlMinLength = 8;
            public const int ImageUrlMaxLength = 2083;
        }

        public static class Restaurant
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 40;
            public const int LocationMinLength = 3;
            public const int LocationMaxLength = 30;
            public const int ImageUrlMinLength = 8;
            public const int ImageUrlMaxLength = 2083;
        }

        public static class Order
        {
            public const string OrderDateFormat = "yyyy-MM-dd HH:mm";
        }

        public static class Review 
        {
            public const int CommentMinLength = 5;
            public const int CommentMaxLength = 300;
            public const string CreatedAtDateFormat = "yyyy-MM-dd HH:mm";
        }

    }
}
