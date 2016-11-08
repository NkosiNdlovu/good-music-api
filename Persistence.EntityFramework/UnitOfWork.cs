using System;
using Domain.Repositories;

namespace Persistence.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArtistDbContext context;

        public IArtistRepository Artists { get; private set; }
        
        public UnitOfWork(ArtistDbContext context)
        {
            this.context = context;
            Artists = new ArtistRepository(context);
        }
        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //this.Dispose();
            //this.context.Dispose();
        }
    }
}
