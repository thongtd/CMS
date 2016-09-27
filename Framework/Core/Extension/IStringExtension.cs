using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Core.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// Convert name to slug
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string NameToSlug(this string name)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            name = name.Replace("-", " ");
            var slug = Regex.Replace(name, "[^\\w\\s]", string.Empty).Replace(" ", "-").ToLower();
            string formD = slug.Normalize(NormalizationForm.FormD);
            slug = regex.Replace(formD, string.Empty).Replace("đ", "d");
            while (slug.IndexOf("--", StringComparison.Ordinal) > 0)
            {
                slug = slug.Replace("--", "-");
            }
            return slug;
        }
        
        /// <summary>
        /// Return content after substring by len
        /// </summary>
        /// <param name="content"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string SubContent(this string content, int len)
        {
            if (content != null)
            {
                var temContent = content;
                if (temContent.Length >= len)
                {
                    var subContent = temContent.Substring(0, len - 3);
                    var valuearray = subContent.Split(' ');
                    var result = string.Empty;
                    for (var i = 0; i < valuearray.Length - 1; i++)
                    {
                        result = result + " " + valuearray[i];
                    }
                    return result + "...";
                }
                return temContent;
            }
            return string.Empty;
        }

        /// <summary>
        /// Create level for a category
        /// </summary>
        /// <param name="order"></param>
        /// <param name="parentLevel"></param>
        /// <returns></returns>
        public static string MakeLevel(int order, string parentLevel)
        {
            string level = order.ToString(CultureInfo.InvariantCulture);
            while (level.Length < 5)
            {
                level = "0" + level;
            }
            if (parentLevel.Length != 0)
                level = parentLevel + level;
            return level;
        }
        
        /// <summary>
        /// Return Vietnamese name day of week
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(this DateTime dateTime)
        {
            var dayOfWeek = dateTime.DayOfWeek;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Thứ Hai";
                case DayOfWeek.Tuesday:
                    return "Thứ Ba";
                case DayOfWeek.Wednesday:
                    return "Thứ Tư";
                case DayOfWeek.Thursday:
                    return "Thứ Năm";
                case DayOfWeek.Friday:
                    return "Thứ Sáu";
                case DayOfWeek.Saturday:
                    return "Thứ Bảy";
                case DayOfWeek.Sunday:
                    return "Chủ Nhật";
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// Return string after MD5 encrypt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string str)
        {
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //StringBuilder sbHash = new StringBuilder();
            //foreach (byte b in bHash)
            //{
            //    sbHash.Append($"{b:x2}");
            //}
            //return sbHash.ToString();
            throw new NotImplementedException();
        }
    }
}
