namespace review_request_app.Core.Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string GoogleReviewLink { get; set; }
        public string FacebookReviewLink { get; set; }
        public byte[] Logo { get; set; }
    }
}
