using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace tBlog.Domain.Entities
{
    public class Setting : EntityWithTimestamps
    {
        [Required]
        public string Name { get; set; }

        [MaxLength]
        [Column(TypeName = "ntext")]
        public string Value { get; set; }
    }
}
