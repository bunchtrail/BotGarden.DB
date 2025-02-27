﻿namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "staff" или "client"
        
        // Дополнительные поля
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
