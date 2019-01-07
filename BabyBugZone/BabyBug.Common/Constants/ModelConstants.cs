using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.Constants
{
    public static class ModelConstants
    {
        public const string ERROR_MSG = "The {0} must be at least {2} and at max {1} characters long.";

        public const int CONTENT_LENGTH_MAX = 5000;
        public const int CONTENT_LENGTH_MIN = 100;
        public const int AUTHOR_NAME_MAX = 25;
        public const int AUTHOR_NAME_MIN = 5;
        public const int HEADER_LENGTH_MIN = 5;
        public const int HEADER_LENGTH_MAX = 30;
        public const int CATEGORY_LENGTH_MIN = 5;
        public const int CATEGORY_LENGTH_MAX = 30;
        public const int SIZE_VALUES_MIN = 1;
        public const int SIZE_VALUES_MAX = 35;
        public const int TYPE_VALUE_MIN = 4;
        public const int TYPE_VALUE_MAX = 35;
        public const int PHONE_VALUE_MIN = 6;
        public const int PHONE_VALUE_MAX = 13;
        public const int USER_NAME_LENGTH_MIN = 3;
        public const int USER_NAME_LENGTH_MAX = 17;
        public const int DESTINATION_NAME_LENGTH_MIN = 5;
        public const int DESTINATION_NAME_LENGTH_MAX = 30;
        public const int PRODUCT_NAME_MAX = 25;
        public const int PRODUCT_NAME_MIN = 3;
        public const int PRODUCT_DESCRIPTION_MAX = 200;
        public const int PRODUCT_DESCRIPTION_MIN = 10;

        public const string CATEGORY_NAME_RGX_ERROR = "Invalid Category name.";
        public const string BLOG_AUTHOR_RGX_ERROR = "Invalid Author name.";
        public const string BLOG_HEADER_RGX_ERROR = "Invalid Title name.";
        public const string SIZE_VALUE_RGX_ERROR = "Invalid Type name.";
        public const string TYPE_VALUE_RGX_ERROR = "Invalid Type name.";
        public const string PHONE_VALUE_RGX_ERROR = "Invalid Phone number.";
        public const string USER_NAME_RGX_ERROR = "Invalid name.";
        public const string CITY_NAME_RGX_ERROR = "Invalid City name.";
        public const string ADDRESS_NAME_RGX_ERROR = "Invalid Address.";
        public const string PRODUCT_NAME_RGX_ERROR = "Invalid Product name.";

        public const string ALPHABETS_RGX = @"^[A-z\s\-]+$";
        public const string NUMERICS_RGX = @"^[0-9]+$";
        public const string ALPHA_NUMERIC_RGX = @"^[a-zA-Z0-9\,\;\.\s\-\(\)\?\!]+$";
    }
}
