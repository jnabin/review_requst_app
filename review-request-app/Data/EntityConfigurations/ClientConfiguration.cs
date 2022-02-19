using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using review_request_app.Core.Domain;

namespace review_request_app.Data.EntityConfigurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(5000);
            
            builder.Property(c => c.LogoPath)
                .IsRequired()
                .HasMaxLength(300);
            
            builder.Property(c => c.GoogleReviewLink)
                .IsRequired()
                .HasMaxLength(400);
            
            builder.Property(c => c.FacebookReviewLink)
                .IsRequired()
                .HasMaxLength(400);
            
            builder.Property(c => c.BusinessName)
                .IsRequired()
                .HasMaxLength(40);

            builder.HasKey(c => c.Id);
        }
    }
}
