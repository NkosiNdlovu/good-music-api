using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.EntityFramework
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(ArtistDbContext context)
            : base(context)
        {

        }
        
        public Artist FindArtist(Guid Artist_Id)
        {
            return Context.Artists.Where(a => a.Artist_Id == Artist_Id).SingleOrDefault();
        }

        public IEnumerable<Artist> FindArtistsThatStartWith(string searchCriteria)
        {
            return Context.Artists.Where(a => a.Name.StartsWith(searchCriteria) || a.Aliases.Contains(","+ searchCriteria));
        }

        public IEnumerable<Artist> GetPagedArtistsThatStartWith(string searchCriteria, int page, int pageSize)
        {
            return Context.Artists.OrderBy(a => a.Name).Skip(pageSize * (page - 1)).Take(pageSize).Where(a => a.Name.StartsWith(searchCriteria) || a.Aliases.Contains("," + searchCriteria));
        }
    }
}
