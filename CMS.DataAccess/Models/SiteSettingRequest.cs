namespace CMS.DataAccess.Models
{
    public class SiteSettingRequest
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Group { get; set; }
    }
}