namespace PokemonReviewApp.Dto
{
    public class ReviewerResponse
    {
        public ReviewerResponse()
        {
            this.Reviewers = new List<ReviewerDto>();
        }

        public List<ReviewerDto> Reviewers { get; set; }
        public string ServerMessage { get; set; }
    }
}
