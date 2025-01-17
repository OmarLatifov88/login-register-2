﻿using System.ComponentModel.DataAnnotations;

namespace UserLoginRegister.Views.ViewModels;

public class ForgotPasswordViewModel
{
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
