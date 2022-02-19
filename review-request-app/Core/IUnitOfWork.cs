using review_request_app.Core.Repositories;
using System;

namespace review_request_app.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        int Complete();
    }
}
