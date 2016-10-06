using System.Collections.Generic;
using System.Web.Mvc;
using CMS.DataAccess.Core.Extension;

namespace CMS.Dashboard.Extentions
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
