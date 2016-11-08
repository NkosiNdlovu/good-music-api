using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Artist FindArtist(Guid Artist_Id);

        IEnumerable<Artist> FindArtistsThatStartWith(string searchCriteria);

        IEnumerable<Artist> GetPagedArtistsThatStartWith(string searchCriteria,int page, int pageSize);

    }
}
