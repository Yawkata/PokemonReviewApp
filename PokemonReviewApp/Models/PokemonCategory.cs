namespace PokemonReviewApp.Models
{
    public class PokemonCategory
    {
        public int PokemonId { get; set; }
        public int CategoryId { get; set; }
        public virtual Pokemon Pokemon { get; set; }
        public virtual Category Category { get; set; }
    }
}
