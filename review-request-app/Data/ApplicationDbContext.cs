using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using review_request_app.Core.Domain;
using review_request_app.Data.EntityConfigurations;
using review_request_app.Models;

namespace review_request_app.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new ClientConfiguration().Configure(builder.Entity<Client>());
            base.OnModelCreating(builder);
        }

    }
}
