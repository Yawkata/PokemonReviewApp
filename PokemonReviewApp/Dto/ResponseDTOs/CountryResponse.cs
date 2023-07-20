namespace PokemonReviewApp.Dto
{
    public class CountryResponse
    {
        public CountryResponse()
        {
            this.Countries = new List<CountryDto>();
        }
 
        public List<CountryDto> Countries { get; set; }
        public string ServerMessage { get; set; }
    }
}
