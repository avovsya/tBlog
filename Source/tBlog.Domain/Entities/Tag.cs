using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tBlog.Domain.Entities
{
    public class Tag : Entity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
