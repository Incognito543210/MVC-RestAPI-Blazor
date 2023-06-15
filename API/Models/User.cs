﻿namespace API.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Opinion> Opinions { get; set; }
    }
}
