namespace CMS.DataAccess.Core.Extension
{
    public class Constants
    {
        public static class Taget
        {
            //Load in a new window
            public const string Blank = "_blank";

            //Load in the same frame as it was clicked
            public const string Self = "_self";

            //Load in the parent frameset
            public const string Parent = "_parent";

            //Load in the full body of the window
            public const string Top = "_top";

            //Load in a named frame
            public const string Framename = "framename";

        }

        public static class CultureCode
        {
            public const string English = "en-US";
            
            public const string Vietnamese = "vi-VN";
        }

        public static class Validation
        {
            public const string Date = "Vui lòng nhập một ngày hợp lệ.";
            public const string Digits = "Vui lòng nhập vào kiểu số.";
            public const string Email = "Vui lòng nhập vào một địa chỉ email hợp lệ.";
            public const string EqualTo = "Vui lòng nhập {0} cùng một lần nữa.";
            public const string MaxLength = "Vui lòng không nhập quá {1} ký tự.";
            public const string MinLength = "Vui lòng nhập ít nhất {0} ký tự.";
            public const string Number = "Vui lòng nhập số hợp lệ.";
            public const string PhoneNumber = "Xin vui lòng nhập số điện thoại hợp lệ.";
            public const string Range = "Vui lòng nhập giá trị giữa {0} và {1}.";
            public const string RangeLength = "Vui lòng nhập độ dài giữa {0} và {1} ký tự.";
            public const string RangeMax = "Vui lòng nhập một giá trị nhỏ hơn hoặc bằng {0}.";
            public const string RangeMin = "Vui lòng nhập một giá trị lớn hơn hoặc bằng {0}.";
            public const string Required = "Các trường bắt buộc không được bỏ trống.";
            public const string Url = "Vui lòng nhập một URL hợp lệ.";
            public const string LettersOnly = "Vui lòng chỉ nhập ký tự.";
            public const string PhoneNumberFormat = "{0:###-###-####}";
        }
    }
}
