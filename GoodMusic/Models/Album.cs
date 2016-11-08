using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodMusic.Models
{
    public class Album
    {
        public string ReleaseId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Label { get; set; }
        public string NoOfTracks { get; set; }
        public List<OtherArtist> otherArtists { get; set; }
    }
}