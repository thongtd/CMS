using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class LoginModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = Constans)]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập vào mật khẩu")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Độ dài của mật khẩu phải lớn hơn 5 ký tự")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
