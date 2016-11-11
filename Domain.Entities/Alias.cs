using System;

namespace Domain.Entities
{
    public class Alias
    {

        public Guid Alias_Id { get; set; }
        public Guid Artist_Id { get; set; }
        public string Name { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
