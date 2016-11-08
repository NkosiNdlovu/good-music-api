using System;

namespace Domain.Entities
{
    public class Artist
    {
        public Guid Artist_Id { get; set; }
        public string Name { get; set; }
        public string Aliases { get; set; }
        public string Country { get; set; }
    }
}
