using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObject
{
    public class LoginViewModel
    {
        [DisplayName("Username"), Required(ErrorMessage = "{0} field cannot be left blank"), StringLength(25,
          ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Username { get; set; }
        [DisplayName("Password"), Required(ErrorMessage = "{0} field cannot be left blank"),DataType(DataType.Password), StringLength(25,
          ErrorMessage = "{0} max. {1} karakter olmalı.")] // alanı boş geçilemez
        public string Password { get; set; }
    }
}