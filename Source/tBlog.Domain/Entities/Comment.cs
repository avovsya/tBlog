using System.ComponentModel.DataAnnotations;

namespace tBlog.Domain.Entities
{
    public class Comment : EntityWithTimestamps
    {
        [Required]
        public string Body { get; set; }

        [Required]
        public string Author { get; set; }

        public int EntryId { get; set; }

        public Entry Entry { get; set; }
    }
}
