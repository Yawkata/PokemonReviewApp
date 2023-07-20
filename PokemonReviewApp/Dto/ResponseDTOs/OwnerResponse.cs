namespace PokemonReviewApp.Dto
{
    public class OwnerResponse
    {
        public OwnerResponse()
        {
            this.Owners = new List<OwnerDto>();
        }

        public List<OwnerDto> Owners { get; set; }
        public string ServerMessage { get; set; }
    }
}
