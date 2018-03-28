﻿using System.ComponentModel.DataAnnotations;


namespace Yunt.WebApi.Models
{
    public class LoginInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入登录密码")]
        public string Password { get; set; }
    }
}