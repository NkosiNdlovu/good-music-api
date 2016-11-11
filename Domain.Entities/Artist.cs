using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Artist
    {
        public Guid Artist_Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Alias> Aliases { get; set; }
        public string Country { get; set; }
    }
}
