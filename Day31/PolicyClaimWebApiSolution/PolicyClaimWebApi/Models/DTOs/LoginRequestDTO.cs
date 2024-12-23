﻿using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models.DTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
