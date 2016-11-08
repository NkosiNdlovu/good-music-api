using System.Collections.Generic;

namespace GoodMusic.Models
{
    public class ArtistSearchApiModel
    {
        public List<ArtistApiModel> results { get; set; }
        public int numberOfSearchResults { get; set; }
        public string page { get; set; }
        public string pageSize { get; set; }
        public string numberOfPages { get; set; }
    }
}