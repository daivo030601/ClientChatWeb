﻿namespace CleanChat.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
