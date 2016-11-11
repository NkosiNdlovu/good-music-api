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
           
            return (from artists in Context.Artists
                      join alias in Context.Aliases on artists equals alias.Artist into X
                      from Y in X.DefaultIfEmpty()
                          where Y.Name.StartsWith(searchCriteria) || artists.Name.StartsWith(searchCriteria)
                      select artists).Distinct().ToList();
            
        }

        public IEnumerable<Artist> GetPagedArtistsThatStartWith(string searchCriteria, int page, int pageSize)
        {
            return (from artists in Context.Artists
                    join alias in Context.Aliases on artists equals alias.Artist into X
                    from Y in X.DefaultIfEmpty()
                    where Y.Name.StartsWith(searchCriteria) || artists.Name.StartsWith(searchCriteria)
                    select artists)
                        .Distinct()
                        .OrderBy(a => a.Name)
                        .Skip(pageSize * (page - 1)).Take(pageSize)
                        .ToList();
        }
    }
}
