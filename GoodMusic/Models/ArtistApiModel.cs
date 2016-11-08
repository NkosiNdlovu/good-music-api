using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodMusic.Models
{
    public class ArtistApiModel
    {
        public string Artist_Id { get; set; }
        public string Name { get; set; }
        public List<string> Aliases { get; set; }
        public string Country { get; set; }
    }
}