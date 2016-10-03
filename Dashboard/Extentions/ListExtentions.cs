using System.Collections.Generic;
using CMS.DataAccess.Core.Extension;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard.Extentions
{
    public class ListExtentions
    {
        public static List<SelectListItem> BinDdlLang()
        {
            var lang = new List<SelectListItem>
            {
                new SelectListItem {Value = Constants.CultureCode.Vietnamese, Text = Constants.CultureCode.VietnameseName},
                new SelectListItem {Value = Constants.CultureCode.English, Text = Constants.CultureCode.EnglishName}
            };

            return lang;
        }
    }
}
