﻿namespace PokemonReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Gym { get; set; }
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
    }
}
