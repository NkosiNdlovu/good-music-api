using System;

namespace Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IArtistRepository Artists { get; }
        int Complete();
    }
}
