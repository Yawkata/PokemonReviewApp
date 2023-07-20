namespace PokemonReviewApp.Dto
{
    public class ReviewResponse
    {
        public ReviewResponse()
        {
            this.Reviews = new List<ReviewDto>();
        }

        public List<ReviewDto> Reviews { get; set; }
        public string ServerMessage { get; set; }
    }
}
