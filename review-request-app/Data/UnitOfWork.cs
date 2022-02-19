using review_request_app.Core;
using review_request_app.Core.Repositories;
using review_request_app.Data.Repositories;

namespace review_request_app.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Clients = new ClientRepository(_context); 
        }

        public IClientRepository Clients { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
