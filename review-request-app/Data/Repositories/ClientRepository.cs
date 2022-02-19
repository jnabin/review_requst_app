using review_request_app.Core.Domain;
using review_request_app.Core.Repositories;
using System.Collections.Generic;

namespace review_request_app.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context)
           : base(context)
        {
        }
        public ApplicationDbContext PlutoContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
