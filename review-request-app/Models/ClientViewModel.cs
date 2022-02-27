using Microsoft.AspNetCore.Http;
using review_request_app.Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace review_request_app.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Required]
        public string BusinessName { get; set; }

        public IFormFile LogoFile { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string GoogleReviewLink { get; set; }

        [Required]
        public string FacebookReviewLink { get; set; }
        public byte[] Logo { get; set; }

        public static implicit operator ClientViewModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                BusinessName = client.BusinessName,
                Logo = client.Logo,
                Description = client.Description,
                FacebookReviewLink = client.FacebookReviewLink,
                GoogleReviewLink = client.GoogleReviewLink
            };
        }

        public static implicit operator Client(ClientViewModel client)
        {
            return new Client
            {
                Id = client.Id,
                BusinessName = client.BusinessName,
                Logo = client.Logo,
                Description = client.Description,
                FacebookReviewLink = client.FacebookReviewLink,
                GoogleReviewLink = client.GoogleReviewLink
            };
        }
    }
}
