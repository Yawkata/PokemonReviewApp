namespace PokemonReviewApp.Dto
{
    public class PokemonResponse
    {
        public PokemonResponse()
        {
            this.Pokemons = new List<PokemonDto>();
        }

        public List<PokemonDto> Pokemons { get; set; }
        public string ServerMessage { get; set; }
    }
}
