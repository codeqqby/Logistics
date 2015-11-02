using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logistics.Models
{
    public class UserModel
    {
        //[Required(ErrorMessage = "")]
        [Display(Name = "用户名:", Description = "4-20个字符")]
        //[StringLength(20, MinimumLength = 4, ErrorMessage = "")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "")]
        [Display(Name = "密  码:", Description = "6-20个字符")]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }

        [Display(Name = "验证码:", Description = "请输入图片中的验证码")]
        //[Required(ErrorMessage = "")]//×
        //[StringLength(6, MinimumLength = 6, ErrorMessage = "")]
        public string Code { get; set; }
    }
}