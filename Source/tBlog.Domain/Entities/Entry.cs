using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tBlog.Domain.Entities
{
    public class Entry : EntityWithTimestamps
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        [MaxLength]
        [Column(TypeName="ntext")]
        public string Body { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string EntryMetaKeywords { get; set; }
        public string EntryMetaDescription { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
