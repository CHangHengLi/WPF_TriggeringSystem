using System;
using System.ComponentModel.DataAnnotations;

namespace TriggerSystemDemo.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [MinLength(3, ErrorMessage = "用户名至少需要3个字符")]
        public string Username { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [MinLength(6, ErrorMessage = "密码至少需要6个字符")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "请输入有效的电子邮件地址")]
        public string Email { get; set; }

        public bool IsSubscribed { get; set; }

        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
} 