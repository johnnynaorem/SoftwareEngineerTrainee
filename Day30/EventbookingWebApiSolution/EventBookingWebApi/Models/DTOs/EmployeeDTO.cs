﻿namespace EventBookingWebApi.Models.DTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
